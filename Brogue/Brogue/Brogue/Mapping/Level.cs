using Brogue.Engine;
using Brogue.EnviromentObjects.Interactive;
using Brogue.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Mapping
{
    public class Level
    {
        Tile[,] tiles;
        public GridBoundList<IEnvironmentObject> Environment { get; private set; }
        //List<Tuple<IEnvironmentObject, IntVec>> environment;

        public GridBoundList<Iinteractable> InteractableEnvironment { get; private set; }
        //public GridBoundList<IRenderableIEnvironmentObject> DroppedItems { get; private set; }

        public GridBoundList<ILightSource> LightSources { get; private set; }

        public GridBoundList<Item> DroppedItems { get; private set; }
        //List<Item> droppedItems;
        public GridBoundList<GameCharacter> CharacterEntities { get; private set; }
        //List<Tuple<GameCharacter, IntVec>> characterEntities;
        bool[,] cachedSolid;
        bool needToCache;
        IntVec startPoint;

        static Random statRand = new Random();

        public Level(IntVec startPoint, Tile[,] tiles, GridBoundList<IEnvironmentObject> environment, GridBoundList<Iinteractable> interact, GridBoundList<ILightSource> light, GridBoundList<GameCharacter> characterEntities)
        {
            this.tiles = tiles;
            this.startPoint = startPoint;
            this.Environment = environment;
            this.InteractableEnvironment = interact;
            this.CharacterEntities = characterEntities;
            this.LightSources = light;
            this.DroppedItems = new GridBoundList<Item>();
            calculateTiles();
            needToCache = true;
            cachedSolid = new bool[tiles.GetLength(0), tiles.GetLength(1)];

            a = findRandomOpenPosition();
            b = findRandomOpenPosition();
            path = AStar.getPathBetween(this, a, b);
            moveset = AStar.getPossiblePositionsFrom(this, a, 15);

            for (int i = 0; i < 28; i++)
            {
                Item item = Item.randomItem(10, 10);

                //Engine.Engine.Log(string.Format("Item Generated: {0}", item.Name));

                DroppedItems.Add(item, findRandomOpenPosition());
            }
        }

        public IntVec GetStartPoint()
        {
            return startPoint;
        }

        private void calculateTiles()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    int right = checkTileSolid(x + 1, y) ? 1 : 0;
                    int left = checkTileSolid(x - 1, y) ? 1 : 0;
                    int up = checkTileSolid(x, y - 1) ? 1 : 0;
                    int down = checkTileSolid(x, y + 1) ? 1 : 0;

                    tiles[x,y].solidNeighbors = right | (up << 1) | (left << 2) | (down << 3);
                }
            }
        }

        public IEnumerable<GameCharacter> GetCharactersIsFriendly(bool isFriendly)
        {
            foreach (GameCharacter character in CharacterEntities.Entities())
            {
                if (character.isFriendly == isFriendly)
                    yield return character;
            }
        }

        public IntVec findRandomOpenPosition()
        {
            cache();

            IntVec result = null;
            do
            {
                result = new IntVec(statRand.Next(cachedSolid.GetLength(0)), statRand.Next(cachedSolid.GetLength(1)));
            }
            while(cachedSolid[result.X,result.Y]);

            return result;
        }

        public IntVec findRandomOpenPosition( IntVec position, int range )
        {
            cache();

            IntVec result = null;
            do
            {
                IntVec offset;
                do
                {
                    offset = new IntVec(statRand.Next(-range, range), statRand.Next(-range, range));
                }
                while (Math.Abs(offset.X) + Math.Abs(offset.Y) > range);

                result = position + offset;
            }
            while (cachedSolid[result.X, result.Y]);

            return result;
        }

        public bool[,] getSolid()
        {
            cache();
            bool[,] result = new bool[cachedSolid.GetLength(0), cachedSolid.GetLength(1)];

            for (int x = 0; x < cachedSolid.GetLength(0); x++)
            {
                for (int y = 0; y < cachedSolid.GetLength(1); y++)
                {
                    result[x, y] = cachedSolid[x, y];
                }
            }

            return result;
        }

        public int[,] getIntSolid()
        {
            cache();

            int[,] result = new int[cachedSolid.GetLength(0), cachedSolid.GetLength(1)];

            for (int x = 0; x < result.GetLength(0); x++)
            {
                for (int y = 0; y < result.GetLength(1); y++)
                {
                    result[x, y] = (cachedSolid[x, y]) ? int.MaxValue : int.MinValue;
                }
            }

            return result;
        }

        public int GetWidth()
        {
            return tiles.GetLength(0);
        }

        public int GetHeight()
        {
            return tiles.GetLength(1);
        }

        private void cache()
        {
            if (needToCache)
            {
                for (int x = 0; x < tiles.GetLength(0); x++)
                {
                    for (int y = 0; y < tiles.GetLength(1); y++)
                    {
                        cachedSolid[x, y] = tiles[x, y].isSolid;
                    }
                }

                foreach (IntVec position in CharacterEntities.Positions() )
                {
                    cachedSolid[position.X, position.Y] = true;
                }

                foreach ( Tuple<IEnvironmentObject,IntVec> enviro in Environment.Tuples() )
                {
                    cachedSolid[enviro.Item2.X, enviro.Item2.Y] |= enviro.Item1.IsSolid();
                }

                foreach (Tuple<Iinteractable, IntVec> enviro in InteractableEnvironment.Tuples())
                {
                    cachedSolid[enviro.Item2.X, enviro.Item2.Y] |= enviro.Item1.IsSolid();
                }

                foreach (Tuple<ILightSource, IntVec> enviro in LightSources.Tuples())
                {
                    //cachedSolid[enviro.Item2.X, enviro.Item2.Y] = true;
                }
                needToCache = false;
            }
        }

        public bool isSolid(int x, int y)
        {
            cache();
            return cachedSolid[x, y];
        }

        public bool isSolid(IntVec position)
        {
            return isSolid(position.X, position.Y);
        }

        public bool Move(GameCharacter character, IntVec position, bool absolute = false)
        {
            bool moveWasMade = false;
            if (absolute && !isSolid( position ) )
                moveWasMade = CharacterEntities.SetPosition(character, position);
            else
            if ( !isSolid( CharacterEntities.FindPosition(character) + position ) )
                moveWasMade = CharacterEntities.AddPosition(character, position);
            return moveWasMade;
        }


        IntVec a;
        IntVec b;
        Direction[] path;
        IntVec[] moveset;

        private bool checkTileSolid(int x, int y)
        {
            bool result = !(x >= 0 && x < tiles.GetLength(0) && y >= 0 && y < tiles.GetLength(1));

            if (!result)
                result = tiles[x, y].isSolid;

            return result;
        }

        public void render()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    Engine.Engine.Draw(tiles[x, y].GetSprite(), new IntVec(x, y));
                }
            }

            Environment.Draw();
            InteractableEnvironment.Draw();
            LightSources.Draw();
            DroppedItems.Draw();
            CharacterEntities.Draw();

            //CharacterEntities.InvokeOnAll( (GameCharacter character, IntVec position) =>
            //{
            //    position.X += character.GetSprite().Direction.X;
            //    position.Y += character.GetSprite().Direction.Y;
            //});

            //foreach (var pair in CharacterEntities.Tuples())
            //{
            //    pair.Item2.X += pair.Item1.GetSprite().Direction.X;
            //    pair.Item2.Y += pair.Item1.GetSprite().Direction.Y;
            //}

            //foreach (IntVec vec in moveset)
            //{
            //    Engine.Engine.Draw(new Sprite(Engine.Engine.placeHolder, Color.Bisque), vec); //Orange
            //    //sb.Draw(Tile.tileset, new Rectangle((int)(vec.X * tileWidth), (int)(vec.Y * tileWidth), (int)Math.Ceiling(tileWidth), (int)Math.Ceiling(tileWidth)), new Rectangle(0, 0, 48, 48), Color.Blue);
            //}


            //path = AStar.getPathBetween(this, a, b);

            //IntVec current = new IntVec(a.X, a.Y);
            //foreach (Direction dir in path)
            //{
            //    current += dir;
            //    Engine.Engine.Draw( new Sprite( Engine.Engine.placeHolder, Color.Orange, dir ), new IntVec(current.X, current.Y) ); //Orange
            //}

            //Engine.Engine.Draw(new Sprite( Engine.Engine.placeHolder, Color.Green ), new IntVec(a.X, a.Y) ); //Green
            //Engine.Engine.Draw(new Sprite( Engine.Engine.placeHolder, Color.Red ), new IntVec(b.X, b.Y) ); //Red


            //var nodes = AStar.getPathDrawnBetween(this, a, b, actionsToTake);
            //foreach (var node in nodes)
            //{
            //    Engine.Engine.Draw(new Sprite(Engine.Engine.placeHolder, Color.BlanchedAlmond), new IntVec(node.position.X, node.position.Y));
            //}

            //Engine.Engine.Draw(new Sprite(Engine.Engine.placeHolder, Color.Yellow), new IntVec(nodes.Min.position.X, nodes.Min.position.Y)); 

        }

        public bool isComplete()
        {
            IntVec pos = findRandomOpenPosition();

            bool[,] solidCopy = getSolid();

            IntVec[] moveset = AStar.getPossiblePositionsFrom(this, pos, -1);

            foreach( IntVec move in moveset )
            {
                solidCopy[move.X, move.Y] = true;
            }

            bool result = true;

            for (int i = 0; i < solidCopy.GetLength(0); i++)
            {
                for (int j = 0; j < solidCopy.GetLength(1); j++)
                {
                    result = result && solidCopy[i, j];
                }
            }

            return result;
        }

        int actionsToTake = 10;
        int previousPathDistance = -1;

        internal void testUpdate()
        {
            IntVec aMove = new IntVec((KeyboardController.IsTyped('H',0) ? 1 : 0) - (KeyboardController.IsTyped('F',0) ? 1 : 0), (KeyboardController.IsTyped('G',0) ? 1 : 0) - (KeyboardController.IsTyped('T',0) ? 1 : 0));
            IntVec bMove = new IntVec((KeyboardController.IsPressed('L') ? 1 : 0) - (KeyboardController.IsPressed('J') ? 1 : 0), (KeyboardController.IsPressed('K') ? 1 : 0) - (KeyboardController.IsPressed('I') ? 1 : 0));
            actionsToTake += (KeyboardController.IsDown('2') ? 1 : 0) - (KeyboardController.IsDown('1') ? 1 : 0);

            if (aMove.X != 0 || aMove.Y != 0 || bMove.X != 0 || bMove.Y != 0)
            {
                if (!isSolid(a + aMove))
                    a += aMove;

                if (!isSolid(b + bMove))
                    b += bMove;

                path = AStar.getPathBetween(this, a, b);

                actionsToTake = 1;

                int movement = Math.Abs(aMove.X) + Math.Abs(aMove.Y) + Math.Abs(bMove.X) + Math.Abs(bMove.Y);

                Engine.Engine.Log(string.Format( "New A* path length: {0}", path.Length.ToString()));
                if (previousPathDistance != -1 && Math.Abs(previousPathDistance - path.Length) > movement)
                    Engine.Engine.Log(string.Format("<INCONSISTENT PATHFIND; MOVEMENTDELTA={0},PATHDELTA={1}>", movement, Math.Abs(previousPathDistance - path.Length)));

                moveset = AStar.getPossiblePositionsFrom(this, a, 15);

                previousPathDistance = path.Length;
            }

            if (KeyboardController.IsPressed('Z'))
            {
                Item item = Item.randomItem(80, 10);

                Engine.Engine.Log(string.Format("Item Generated: {0}", item.Name));

                DroppedItems.Add(item, findRandomOpenPosition());
            }
            
        }

        internal static void LoadContent(ContentManager content)
        {
            Tile.wallTileset = content.Load<Texture2D>("dynamicTileset");
            Tile.floorTileset = content.Load<Texture2D>("floorTileset");
            Tile.tileset = content.Load<Texture2D>("levelTileset");
        }

        internal void InvalidateCache()
        {
            needToCache = true;
        }
    }
}