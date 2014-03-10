using Brogue.Enemies;
using Brogue.Engine;
using Brogue.EnviromentObjects.Decorative;
using Brogue.EnviromentObjects.Interactive;
using Brogue.Items;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Mapping
{
    static class LevelGenerator
    {
        #region FLOOR PLAN AND ROOM
        private class __FloorPlan
        {
            public struct __Room
            {
                public enum __RoomType { EMPTY, NOTHING_SPECIAL, DOORWAY, HALLWAY, TREASURE_ROOM, SUPAH_TREASURE_ROOM, FOYER, BOSS_ROOM, MOB_ROOM };

                public __RoomType type;

                public bool[,] floorPlan;

                public Rectangle dimensions;

                public __Room(Rectangle dimensions, bool[,] floorPlan)
                {
                    this.dimensions = dimensions;

                    type = __RoomType.NOTHING_SPECIAL;

                    this.floorPlan = floorPlan;

                    if (dimensions.Width > 5 && dimensions.Height > 5)
                    {
                        if (getLargestOpening() == 1)
                        {
                            Engine.Engine.Log( string.Format( "BOSS ROOM GENERATED AT <{0}, {1}>", dimensions.X, dimensions.Y ) );
                            type = __RoomType.BOSS_ROOM;
                        }
                        else
                            type = __RoomType.FOYER;
                    }

                    if (dimensions.Width > 3 && dimensions.Height >3 && dimensions.Width < 7 && dimensions.Height < 7)
                    {
                        if (getOpenings() < 6)
                            type = __RoomType.TREASURE_ROOM;
                        else
                            type = __RoomType.MOB_ROOM;
                    }

                    if (dimensions.Width > 3 && dimensions.Height > 3 && getOpenings() <= 2 && ( getOpenings() == getLargestOpening() ) )
                    {
                        Engine.Engine.Log(string.Format("SEED: {2} with {3} -- SUPER TREASURE GENERATED AT <{0}, {1}>", dimensions.X, dimensions.Y, seedDebug, levelDebug));
                        type = __RoomType.SUPAH_TREASURE_ROOM;
                    }

                    if (dimensions.Width == 1 || dimensions.Height == 1)
                    {
                        type = __RoomType.HALLWAY;
                    }
                    
                    if (dimensions.Width == 1 && dimensions.Height == 1
                        && !((floorPlan[dimensions.X + 1, dimensions.Y] ^ floorPlan[dimensions.X - 1, dimensions.Y]) || (floorPlan[dimensions.X, dimensions.Y - 1] ^ floorPlan[dimensions.X, dimensions.Y + 1])))
                        type = __RoomType.DOORWAY;
                }

                public IntVec GetCenter()
                {
                    return new IntVec(dimensions.X + dimensions.Width / 2, dimensions.Y + dimensions.Height / 2);
                }

                public IEnumerable<IntVec> GetCells()
                {
                    for (int i = 0; i < dimensions.Width; i++)
                    {
                        for (int j = 0; j < dimensions.Height; j++)
                        {
                            yield return new IntVec(dimensions.X + i, dimensions.Y + j);
                        }
                    }
                }

                public IEnumerable<Tuple<IntVec,Direction>> GetWalls( bool inside = false, bool checkForSolid = true )
                {
                    int offset = (inside) ? 1 : 0 ;
                    IntVec pos;
                    for (int i = 0; i < dimensions.Height; i++)
                    {
                        pos = new IntVec(dimensions.X + offset - 1, dimensions.Y + i);
                        if (floorPlan[pos.X - offset, pos.Y] != checkForSolid)
                            yield return Tuple.Create<IntVec, Direction>(pos, Direction.RIGHT);
                    }

                    for (int i = 0; i < dimensions.Width; i++)
                    {
                        pos = new IntVec(dimensions.X + i, dimensions.Y + offset - 1);
                        if (floorPlan[pos.X, pos.Y - offset] != checkForSolid)
                            yield return Tuple.Create<IntVec, Direction>(pos, Direction.DOWN);
                    }

                    if (dimensions.Width > 1)
                    for (int i = 0; i < dimensions.Height; i++)
                    {
                        pos = new IntVec(dimensions.X + dimensions.Width - offset, dimensions.Y + i);
                        if (floorPlan[pos.X + offset, pos.Y] != checkForSolid)
                            yield return Tuple.Create<IntVec, Direction>(pos, Direction.LEFT);
                    }

                    if (dimensions.Height != 1)
                    for (int i = 0; i < dimensions.Width; i++)
                    {
                        pos = new IntVec(dimensions.X + i, dimensions.Y + dimensions.Height - offset);
                        if (floorPlan[pos.X, pos.Y + offset] != checkForSolid)
                            yield return Tuple.Create<IntVec, Direction>(pos, Direction.UP);
                    }
                }

                public int getOpenings()
                {
                    int result = 0;
                    IntVec pos;
                    for (int i = 0; i < dimensions.Height; i++)
                    {
                        pos = new IntVec(dimensions.X - 1, dimensions.Y + i);
                        if (floorPlan[pos.X, pos.Y])
                            result++;
                    }

                    for (int i = 0; i < dimensions.Width; i++)
                    {
                        pos = new IntVec(dimensions.X + i, dimensions.Y - 1);
                        if (floorPlan[pos.X, pos.Y])
                            result++;
                    }

                    for (int i = 0; i < dimensions.Height; i++)
                    {
                        pos = new IntVec(dimensions.X + dimensions.Width, dimensions.Y + i);
                        if (floorPlan[pos.X, pos.Y])
                            result++;
                    }

                    for (int i = 0; i < dimensions.Width; i++)
                    {
                        pos = new IntVec(dimensions.X + i, dimensions.Y + dimensions.Height);
                        if (floorPlan[pos.X, pos.Y])
                            result++;
                    }
                    return result;
                }

                public int getLargestOpening()
                {
                    int currentOpening = 0;
                    int largest = 0;
                    IntVec pos;
                    for (int i = 0; i < dimensions.Height; i++)
                    {
                        pos = new IntVec(dimensions.X - 1, dimensions.Y + i);
                        if (floorPlan[pos.X, pos.Y])
                            currentOpening++;
                        else
                            currentOpening = 0;
                        largest = Math.Max(largest, currentOpening);
                    }

                    for (int i = 0; i < dimensions.Width; i++)
                    {
                        pos = new IntVec(dimensions.X + i, dimensions.Y - 1);
                        if (floorPlan[pos.X, pos.Y])
                            currentOpening++;
                        else
                            currentOpening = 0;
                        largest = Math.Max(largest, currentOpening);
                    }

                    for (int i = 0; i < dimensions.Height; i++)
                    {
                        pos = new IntVec(dimensions.X + dimensions.Width, dimensions.Y + i);
                        if (floorPlan[pos.X, pos.Y])
                            currentOpening++;
                        else
                            currentOpening = 0;
                        largest = Math.Max(largest, currentOpening);
                    }

                    for (int i = 0; i < dimensions.Width; i++)
                    {
                        pos = new IntVec(dimensions.X + i, dimensions.Y + dimensions.Height);
                        if (floorPlan[pos.X, pos.Y])
                            currentOpening++;
                        else
                            currentOpening = 0;
                        largest = Math.Max(largest, currentOpening);
                    }
                    return largest;
                }

                public bool setUnfree(int x, int y)
                {
                    bool result = floorPlan[x, y];
                    floorPlan[x, y] = false;
                    return result;
                }

                public bool setUnfree(IntVec pos)
                {
                    return setUnfree(pos.X, pos.Y);
                }

                internal bool isFree(IntVec pos)
                {
                    return floorPlan[pos.X, pos.Y];
                }
            }

            public bool[,] isFloor;
            public Tile[,] tiles;
            public __Room[] rooms;

            public int hallstart;

            public __FloorPlan( bool[,] isFloor )
            {
                this.isFloor = isFloor;
                rooms = findRooms(isFloor);

                tiles = new Tile[isFloor.GetLength(0), isFloor.GetLength(1)];

                for (int x = 0; x < isFloor.GetLength(0); x++)
                {
                    for (int y = 0; y < isFloor.GetLength(1); y++)
                    {
                        tiles[x, y] = new Tile(!isFloor[x, y]);
                    }
                }

                for (int i = 0; i < rooms.Length; i++)
                {
                    if (rooms[i].type != __Room.__RoomType.DOORWAY && rooms[i].type != __Room.__RoomType.HALLWAY)
                        hallstart = i;
                }
            }

            private static __Room[] findRooms(bool[,] floorPlan)
            {
                bool[,] floorPlanRoom = new bool[floorPlan.GetLength(0), floorPlan.GetLength(1)];
                for (int x = 0; x < floorPlan.GetLength(0); x++)
                {
                    for (int y = 0; y < floorPlan.GetLength(1); y++)
                    {
                        floorPlanRoom[x, y] = floorPlan[x, y];
                    }
                }

                List<__Room> rooms = new List<__Room>();

                //rooms
                for (int x = 0; x < floorPlanRoom.GetLength(0); x++)
                {
                    for (int y = 0; y < floorPlanRoom.GetLength(1); y++)
                    {
                        if (floorPlanRoom[x, y])
                        {
                            Rectangle potentialRoom = fitRectangle(floorPlanRoom, x, y);

                            if (potentialRoom.Width != 1 && potentialRoom.Height != 1)
                            {
                                rooms.Add(new __Room(potentialRoom, floorPlan));
                                for (int i = 0; i < potentialRoom.Width; i++)
                                {
                                    for (int j = 0; j < potentialRoom.Height; j++)
                                    {
                                        floorPlanRoom[x + i, y + j] = false;
                                    }
                                }
                            }
                        }
                    }
                }

                //Hallways and cells
                for (int x = 0; x < floorPlanRoom.GetLength(0); x++)
                {
                    for (int y = 0; y < floorPlanRoom.GetLength(1); y++)
                    {
                        if (floorPlanRoom[x, y])
                        {
                            Rectangle potentialRoom = fitRectangle(floorPlanRoom, x, y);

                            rooms.Add(new __Room(potentialRoom, floorPlan));
                            for (int i = 0; i < potentialRoom.Width; i++)
                            {
                                for (int j = 0; j < potentialRoom.Height; j++)
                                {
                                    floorPlanRoom[x + i, y + j] = false;
                                }
                            }
                        }
                    }
                }

                return rooms.ToArray();
            }

            private static Rectangle fitRectangle(bool[,] floorPlanRoom, int x, int y)
            {
                int width = 1;
                int height = 1;

                bool canWiden = true;
                bool canHeighten = true;

                while (canHeighten || canWiden)
                {
                    if (canWiden)
                    {
                        for (int i = 0; i < height; i++)
                        {
                            canWiden = canWiden && floorPlanRoom[x + width, y + i];
                        }
                        if (canWiden)
                            width++;
                    }

                    if (canHeighten)
                    {
                        for (int i = 0; i < width; i++)
                        {
                            canHeighten = canHeighten && floorPlanRoom[x + i, y + height];
                        }
                        if (canHeighten)
                            height++;
                    }
                }

                return new Rectangle(x, y, width, height);
            }
        }
        #endregion

        static int seedDebug, levelDebug;
        public static Level generate(int seed, int levels, int dungeonLevel = 1, int heroLevel = 1 )
        {
            //seed = 1599313429;
            //levels = 2150;

            seedDebug = seed;
            levelDebug = levels;

            Random rand = new Random(seed);

            __FloorPlan floorPlan = createFloorPlan(rand, levels);

            GridBoundList<IEnvironmentObject> environment = new GridBoundList<IEnvironmentObject>();
            GridBoundList<IInteractable> interactableEnvironment = new GridBoundList<IInteractable>();
            GridBoundList<ILightSource> lightSources = new GridBoundList<ILightSource>();
            GridBoundList<GameCharacter> characters = new GridBoundList<GameCharacter>();

            IntVec startPoint = findEndPoints(floorPlan, interactableEnvironment, rand);

            Tuple<HiddenPassage, IntVec>[] previousPassage = new Tuple<HiddenPassage,IntVec>[1];
            List<IInteractable> subscribedForSwitches = new List<IInteractable>();


            foreach (var room in floorPlan.rooms)
            {
                populateEnvironmentObjects(room, environment, rand);
                populateInteractiveEnvironmentObjects(room, interactableEnvironment, rand, dungeonLevel, heroLevel, previousPassage, subscribedForSwitches);
                populateLightSources(room, lightSources, rand);
                populateGameCharacters(room, characters, rand, dungeonLevel, heroLevel);
            }

            populateSwitches(floorPlan, interactableEnvironment, rand, subscribedForSwitches);

            Level result = new Level( startPoint, floorPlan.tiles, environment, interactableEnvironment, lightSources, characters, dungeonLevel );

            //if (!result.isComplete())
            {
                //Engine.Engine.Log( "Level contains places which are impossible to reach from the starting position" );
            }

            return result;
        }

        private static void populateSwitches(__FloorPlan floorPlan, GridBoundList<IInteractable> interactableEnvironment, Random rand, List<IInteractable> subscribedForSwitches)
        {
            while (subscribedForSwitches.Count > 0)
            {
                Brogue.Mapping.LevelGenerator.__FloorPlan.__Room room = floorPlan.rooms[rand.Next(floorPlan.rooms.Length)];

                if (room.type == __FloorPlan.__Room.__RoomType.NOTHING_SPECIAL)
                {
                    foreach (var wall in room.GetWalls(true))
                    {
                        if (subscribedForSwitches.Count > 0 && rand.NextDouble() > 0.99 )
                        {
                            interactableEnvironment.Add(new Switch(subscribedForSwitches[0], wall.Item2), wall.Item1);
                            subscribedForSwitches.RemoveAt(0);
                        }
                    }
                }
            }
        }

        private static IntVec findEndPoints(__FloorPlan floorPlan, GridBoundList<IInteractable> interactableEnvironment, Random rand)
        {
            int entryRoom = rand.Next(floorPlan.hallstart);
            while (floorPlan.rooms[entryRoom].type != __FloorPlan.__Room.__RoomType.FOYER && floorPlan.rooms[entryRoom].type != __FloorPlan.__Room.__RoomType.TREASURE_ROOM)
            {
                entryRoom = (entryRoom + 1) % floorPlan.hallstart;
            }
            __FloorPlan.__Room start = floorPlan.rooms[entryRoom];
            IntVec startPoint = start.GetCenter();
            Engine.Engine.Log(string.Format("Start point: <{0}, {1}>", startPoint.X, startPoint.Y));
            floorPlan.isFloor[startPoint.X, startPoint.Y] = false;
            floorPlan.rooms[entryRoom].type = __FloorPlan.__Room.__RoomType.EMPTY;

            int endRoom = (entryRoom + floorPlan.hallstart / 2) % floorPlan.hallstart;
            while (floorPlan.rooms[endRoom].type != __FloorPlan.__Room.__RoomType.FOYER && floorPlan.rooms[endRoom].type != __FloorPlan.__Room.__RoomType.TREASURE_ROOM)
            {
                endRoom = (endRoom + 1) % floorPlan.hallstart;
            }
            __FloorPlan.__Room end = floorPlan.rooms[endRoom];
            floorPlan.rooms[endRoom].type = __FloorPlan.__Room.__RoomType.EMPTY;
            Engine.Engine.Log(string.Format("End point: <{0}, {1}>", end.GetCenter().X, end.GetCenter().Y));
            floorPlan.isFloor[end.GetCenter().X, end.GetCenter().Y] = false;
            try
            {
                Stairs s = new Stairs();
            }
            catch (Exception e)
            {
                Console.WriteLine( "" );
            }
            interactableEnvironment.Add(new Stairs(), end.GetCenter());
            return startPoint;
        }
        
        private static void populateEnvironmentObjects(__FloorPlan.__Room room, GridBoundList<IEnvironmentObject> environ, Random rand)
        {
            Color col = Color.White;
            switch (room.type)
            {
                case __FloorPlan.__Room.__RoomType.BOSS_ROOM:
                    col = Color.Red;
                    break;
                case __FloorPlan.__Room.__RoomType.FOYER:
                    col = Color.Blue;
                    break;
                case __FloorPlan.__Room.__RoomType.TREASURE_ROOM:
                    col = Color.Yellow;
                    break;
                case __FloorPlan.__Room.__RoomType.NOTHING_SPECIAL:
                    col = Color.Gray;
                    break;
                case __FloorPlan.__Room.__RoomType.MOB_ROOM:
                    col = Color.Green;
                    break;
                //case __FloorPlan.__Room.__RoomType.DOORWAY:
                //    environ.Add(new ColorEnvironment(Color.Magenta, true), new IntVec(room.dimensions.X, room.dimensions.Y));
                //    break;
            }

            foreach (IntVec pos in room.GetCells())
            {
                //environ.Add(new ColorEnvironment(col, false), pos);
            }



            switch (room.type)
            {
                case __FloorPlan.__Room.__RoomType.BOSS_ROOM:
                    Decoration[] scratches = {  new Decoration(new Sprite(Engine.Engine.GetTexture("Enviroment/Scratches"), new IntVec(0,0))), 
                                                new Decoration(new Sprite(Engine.Engine.GetTexture("Enviroment/Scratches"), new IntVec(1,0))) };
                    foreach (IntVec pos in room.GetCells())
                    {
                        //Scratches on the floor
                        if (rand.NextDouble() > 0.8)
                            environ.Add( ChooseOne(rand, scratches) , pos);
                    }
                    goto case __FloorPlan.__Room.__RoomType.MOB_ROOM;
                case __FloorPlan.__Room.__RoomType.MOB_ROOM:
                    Decoration[] bloods = {  new Decoration(new Sprite(Engine.Engine.GetTexture("Enviroment/Blood"), new IntVec(0,0))), 
                                                new Decoration(new Sprite(Engine.Engine.GetTexture("Enviroment/Blood"), new IntVec(1,0))) };
                    foreach (IntVec pos in room.GetCells())
                    {
                        //Blood on the floor
                        if (rand.NextDouble() > 0.8)
                            environ.Add(ChooseOne(rand, bloods), pos);
                    }
                    break;
                case __FloorPlan.__Room.__RoomType.NOTHING_SPECIAL:
                    foreach (Tuple<IntVec,Direction> pos in room.GetWalls(true))
                    {
                        if (rand.NextDouble() > 0.95)
                            environ.Add(new Plant(), pos.Item1);
                    }

                    Decoration[] floorStuff = {  new Decoration(new Sprite(Engine.Engine.GetTexture("Enviroment/FloorStuff"), new IntVec(0,0))), 
                                                new Decoration(new Sprite(Engine.Engine.GetTexture("Enviroment/FloorStuff"), new IntVec(1,0))),
                                                new Decoration(new Sprite(Engine.Engine.GetTexture("Enviroment/FloorStuff"), new IntVec(2,0))) };
                    foreach (IntVec pos in room.GetCells())
                    {
                        //Random stuff on the floor
                        if (rand.NextDouble() > 0.95)
                            environ.Add(ChooseOne(rand, floorStuff), pos);
                    }
                    break;
                case __FloorPlan.__Room.__RoomType.FOYER:
                    Decoration[] tableItems = {  new Decoration(new Sprite(Engine.Engine.GetTexture("Enviroment/Plate"), new IntVec(0,0) )), 
                                                new Decoration(new Sprite(Engine.Engine.GetTexture("Enviroment/Plate"), new IntVec(0,0) )), 
                                                new Decoration(new Sprite(Engine.Engine.GetTexture("Enviroment/Plate"), new IntVec(1,0) )), 
                                                new Decoration(new Sprite(Engine.Engine.GetTexture("Enviroment/Plate"), new IntVec(2,0) )), 
                                                new Decoration(new Sprite(Engine.Engine.GetTexture("Enviroment/Plate"), new IntVec(3,0))) };
                    //Table
                    for (int x = 2; x < room.dimensions.Width - 2; x++)
                    {
                        for (int y = 2; y < room.dimensions.Height - 2; y++)
                        {
                            room.setUnfree(room.dimensions.X + x, room.dimensions.Y + y);
                            environ.Add(new Decoration(new Sprite(Engine.Engine.GetTexture("Enviroment/Table"), new IntVec(1 - ((x == 2) ? 1 : 0) + ((x == room.dimensions.Width - 3) ? 1 : 0), 1 - ((y == 2) ? 1 : 0) + ((y == room.dimensions.Height - 3) ? 1 : 0))), true), new IntVec(room.dimensions.X + x, room.dimensions.Y + y));
                            //environ.Add(new ColorEnvironment( Color.Aqua, true ) , new IntVec( room.dimensions.X + x, room.dimensions.Y + y ) );
                            if (rand.NextDouble() > 0.85)
                            {
                                //Something on the table
                                environ.Add(ChooseOne(rand, tableItems), new IntVec(room.dimensions.X + x, room.dimensions.Y + y));
                            }
                        }
                    }
                    //Chairs
                    for (int x = 2; x < room.dimensions.Width - 2; x++)
                    {
                        environ.Add( new Chair( Direction.DOWN ), new IntVec(room.dimensions.X + x, room.dimensions.Y + 1) );
                        environ.Add(new Chair(Direction.UP), new IntVec(room.dimensions.X + x, room.dimensions.Y + room.dimensions.Height - 2));
                    }
                    for (int y = 2; y < room.dimensions.Height - 2; y++)
                    {
                        environ.Add(new Chair(Direction.RIGHT), new IntVec(room.dimensions.X + 1, room.dimensions.Y + y));
                        environ.Add(new Chair(Direction.LEFT), new IntVec(room.dimensions.X + room.dimensions.Width - 2, room.dimensions.Y + y));
                    }
                    //environ.Add( new Plant(), room.GetCenter() );
                    break;
            }
        }


        private static void populateInteractiveEnvironmentObjects(__FloorPlan.__Room room, GridBoundList<IInteractable> interact, Random rand, int dungeonLevel, int heroLevel, Tuple<HiddenPassage, IntVec>[] previousPassage, List<IInteractable> subscribedForSwitches)
        {

            switch (room.type)
            {
                case __FloorPlan.__Room.__RoomType.DOORWAY:
                    if ( room.setUnfree(room.dimensions.X, room.dimensions.Y) )
                        interact.Add(new Door( (room.floorPlan[room.dimensions.X-1,room.dimensions.Y]) ? Direction.RIGHT: Direction.UP ), new IntVec(room.dimensions.X, room.dimensions.Y));
                    break;
                case __FloorPlan.__Room.__RoomType.SUPAH_TREASURE_ROOM:
                    if (room.setUnfree(room.GetCenter()))
                    {
                        Item[] items = new Item[rand.Next(2, 8)];
                        for (int i = 0; i < items.Length; i++)
                        {
                            items[i] = Item.randomLegendary(dungeonLevel,heroLevel);
                        }
                        interact.Add(new Chest(items), room.GetCenter());
                    }

                    foreach (var wall in room.GetWalls(false,false))
                    {
                        room.setUnfree( wall.Item1 );

                        interact.RemoveAtPosition(wall.Item1);
                        Gate door = new Gate(wall.Item2);
                        interact.Add(door, wall.Item1);
                        subscribedForSwitches.Add(door);
                    }
                    break;
                case __FloorPlan.__Room.__RoomType.TREASURE_ROOM:
                    if (room.setUnfree(room.GetCenter()))
                    {
                        Item[] items = new Item[rand.Next(2, 8)];
                        for (int i = 0; i < items.Length; i++)
                        {
                            items[i] = Item.randomItem(dungeonLevel, heroLevel);
                        }
                        interact.Add(new Chest(items), room.GetCenter());
                    }
                    break;

                case __FloorPlan.__Room.__RoomType.NOTHING_SPECIAL:
                    foreach (var wall in room.GetWalls(false))
                    {
                        if (rand.NextDouble() > 0.998)
                        {
                            if( previousPassage[0] == null )
                            {
                                previousPassage[0] = Tuple.Create<HiddenPassage, IntVec>(new HiddenPassage(wall.Item2), wall.Item1);
                            }
                            else
                            {
                                interact.Add(previousPassage[0].Item1, previousPassage[0].Item2);
                                interact.Add(new HiddenPassage(previousPassage[0].Item1, wall.Item2), wall.Item1);
                                previousPassage[0] = null;
                            }
                        }
                    }
                    break;
            }
        }

        private static void populateLightSources(__FloorPlan.__Room room, GridBoundList<ILightSource> lights, Random rand)
        {
            if (room.type != __FloorPlan.__Room.__RoomType.DOORWAY)
            foreach (var wall in room.GetWalls(true))
            {
                if (rand.NextDouble() > 0.94)
                    lights.Add(new Tourch(wall.Item2, new Color(255, 155, 55)), wall.Item1);
                    //lights.Add(new ColorEnvironment(new Color(rand.Next(100,256), rand.Next(100,256), rand.Next(100,256)), false), position);
            }
            //switch (room.type)
            //{
            //}
        }

        private static void populateGameCharacters(__FloorPlan.__Room room, GridBoundList<GameCharacter> chars, Random rand, int dungeonLevel, int heroLevel)
        {
            switch (room.type)
            {
                case __FloorPlan.__Room.__RoomType.BOSS_ROOM:
                    if (room.isFree(room.GetCenter()))
                    {
                        chars.Add(EnemyCreator.GetRandomBoss(dungeonLevel), room.GetCenter());
                    }
                    break;
                case __FloorPlan.__Room.__RoomType.NOTHING_SPECIAL:
                    foreach (var pos in room.GetCells())
                    {
                        if (rand.NextDouble() > 0.98 && room.isFree(pos))
                            chars.Add(EnemyCreator.GetRandomEnemy(1, dungeonLevel)[0], pos);
                        //lights.Add(new ColorEnvironment(new Color(rand.Next(100,256), rand.Next(100,256), rand.Next(100,256)), false), position);
                    }
                    break;
                case __FloorPlan.__Room.__RoomType.MOB_ROOM:
                    Enemy[] enemies = EnemyCreator.GetRandomEnemy(rand.Next(2, 6), dungeonLevel);
                    int dropped = 1;
                    chars.Add(enemies[0], room.GetCenter() );
                    foreach (var dir in Direction.Values)
                    {
                        if (dropped < enemies.Length && (room.isFree(room.GetCenter() + dir)))
                        {
                            chars.Add(enemies[dropped], room.GetCenter() + dir);
                            dropped++;
                        }
                    }
                    break;
            }
        }

        private static T ChooseOne<T>(Random rand, params T[] choices)
        {
            return choices[ rand.Next( choices.Length ) ];
        }

        #region FLOOR PLAN CREATION
        private static __FloorPlan createFloorPlan(Random rand, int levels)
        {
            const int PADDING = 5;

            int maxPlayGround = 3500;
            bool[,] playGround = new bool[maxPlayGround, maxPlayGround];
            int targetX = maxPlayGround / 2,
                targetY = maxPlayGround / 2,
                left = maxPlayGround / 2,
                right = maxPlayGround / 2,
                top = maxPlayGround / 2,
                bottom = maxPlayGround / 2;

            int floorComplexity = levels;// rand.Next(100) + 10;

            for (int level = 0; level < floorComplexity; level++)
            {
                int roomWidth = rand.Next(15) + 6;
                int roomHeight = rand.Next(15) + 6;
                int anchorX = rand.Next(roomWidth - PADDING) + PADDING / 2;
                int anchorY = rand.Next(roomHeight - PADDING) + PADDING / 2;

                for (int x = 1; x < roomWidth; x++)
                {
                    for (int y = 1; y < roomHeight; y++)
                    {
                        playGround[x - anchorX + targetX, y - anchorY + targetY] = true;
                    }
                }

                createWalls(playGround, roomWidth, roomHeight, targetX, targetY, anchorX, anchorY, rand);

                left = Math.Min(left, targetX - anchorX);
                right = Math.Max(right, roomWidth + targetX - anchorX);
                bottom = Math.Min(bottom, targetY - anchorY);
                top = Math.Max(top, roomHeight + targetY - anchorY);

                targetX += rand.Next(roomWidth - PADDING) + PADDING / 2 - anchorX;
                targetY += rand.Next(roomHeight - PADDING) + PADDING / 2 - anchorY;
            }

            bool[,] cropped = arrayCrop(playGround, left - 1, bottom - 1, right + 1, top + 1);
            //widenFloorPlan(cropped);

            return new __FloorPlan( cropped );
        }

        private static void createWalls(bool[,] playGround, int roomWidth, int roomHeight, int targetX, int targetY, int anchorX, int anchorY, Random rand)
        {
            Vector2[] edges = getEdges(targetX - anchorX, targetY - anchorY, roomWidth, roomHeight);
            Vector2[] corners = { new Vector2(-anchorX + targetX, -anchorY + targetY),
                                        new Vector2( roomWidth-anchorX + targetX-1, -anchorY + targetY),
                                        new Vector2(roomWidth-anchorX + targetX-1, roomHeight-anchorY + targetY-1),
                                        new Vector2(-anchorX + targetX, roomHeight-anchorY + targetY-1) };

            int startingIndex = rand.Next(edges.Length);

            bool foundEmpty = false;

            bool waitingForDoor = false;
            bool previousWasEmpty = true;

            for (int i = 0; i < edges.Length; i++)
            {
                if (!foundEmpty && !playGround[(int)edges[i].X, (int)edges[i].Y])
                {
                    startingIndex = i + 1;
                    waitingForDoor = true;
                    previousWasEmpty = true;
                    foundEmpty = true;
                }
            }

            bool[,] section = new bool[roomWidth + 3, roomHeight + 3];
            for (int x = 0; x < roomWidth + 3; x++)
            {
                for (int y = 0; y < roomHeight + 3; y++)
                {
                    section[x, y] = playGround[x - anchorX + targetX - 1, y - anchorY + targetY - 1];
                }
            }



            for (int i = 0; i < edges.Length; i++)
            {
                Vector2 vec = edges[(i + startingIndex) % edges.Length];
                int x = (int)vec.X;
                int y = (int)vec.Y;

                waitingForDoor = previousWasEmpty && playGround[x, y];

                bool newSolid;
                if (waitingForDoor && previousWasEmpty && !corners.Contains(vec))
                {
                    waitingForDoor = false;
                    newSolid = true;
                }
                else
                {
                    newSolid = false;

                    for (int w = -1; w <= 1; w++)
                    {
                        for (int h = -1; h <= 1; h++)
                        {
                            if (w != 0 || h != 0)
                                newSolid = newSolid || (!section[x + w - targetX + anchorX + 1, y + h - targetY + anchorY + 1]);
                        }
                    }

                    newSolid = (newSolid) ? playGround[x, y] : false;

                    //newSolid = ( (!playGround[x - 1, y]) || (!playGround[x + 1, y]) || (!playGround[x, y - 1]) || (!playGround[x, y + 1]) ) ? playGround[x,y] : false;
                }


                previousWasEmpty = !playGround[x, y];

                playGround[x, y] = newSolid;
            }
        }

        private static void widenFloorPlan(bool[,] playGround)
        {
            //Widen single areas
            bool[,] copy = new bool[playGround.GetLength(0), playGround.GetLength(1)];
            for (int x = 0; x < copy.GetLength(0); x++)
            {
                for (int y = 0; y < copy.GetLength(1); y++)
                {
                    copy[x, y] = playGround[x, y];
                }
            }
            for (int x = 1; x < copy.GetLength(0) - 1; x++)
            {
                for (int y = 1; y < copy.GetLength(1) - 1; y++)
                {
                    if (copy[x, y])
                    {
                        if ((!copy[x - 1, y] && !copy[x + 1, y]) || (!copy[x, y - 1] && !copy[x, y + 1]))
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                for (int j = -1; j <= 1; j++)
                                {
                                    playGround[x + i, y + j] = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private static bool[,] arrayCrop(bool[,] playGround, int left, int bottom, int right, int top)
        {
            bool[,] result = new bool[right - left, top - bottom];

            for (int x = left; x < right; x++)
            {
                for (int y = bottom; y < top; y++)
                {
                    result[x - left, y - bottom] = playGround[x, y];
                }
            }

            return result;
        }

        static int boolToInt(bool test)
        {
            return (test) ? 1 : 0;
        }

        private static Vector2[] getEdges(int x, int y, int width, int height)
        {
            Vector2[] result = new Vector2[2 * width + 2 * height];

            int currentIndex = 0;

            for (int i = 0; i < width; i++)
                result[currentIndex++] = new Vector2(x + i, y);
            for (int i = 0; i < height; i++)
                result[currentIndex++] = new Vector2(x + width, y + i);
            for (int i = 0; i < width; i++)
                result[currentIndex++] = new Vector2(x + width - i, y + height);
            for (int i = 0; i < height; i++)
                result[currentIndex++] = new Vector2(x, y + height - i);


            return result;
        }

        #endregion
    }
}
