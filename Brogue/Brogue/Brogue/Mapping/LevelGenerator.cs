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
                public enum __RoomType { EMPTY, NOTHING_SPECIAL, DOORWAY, HALLWAY, TREASURE_ROOM, FOYER, BOSS_ROOM, MOB_ROOM };

                public __RoomType type;

                bool[,] floorPlan;

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

                    if (dimensions.Width > 3 && dimensions.Height > 3 && dimensions.Width < 7 && dimensions.Height < 7)
                    {
                        if ( getOpenings() < 6 )
                            type = __RoomType.TREASURE_ROOM;
                        else
                            type = __RoomType.MOB_ROOM;
                    }

                    if (dimensions.Width == 1 || dimensions.Height == 1)
                        type = __RoomType.HALLWAY;

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

                public IEnumerable<Tuple<IntVec,Direction>> GetWalls( bool inside = false )
                {
                    int offset = (inside) ? 1 : 0 ;
                    IntVec pos;
                    for (int i = 0; i < dimensions.Height; i++)
                    {
                        pos = new IntVec(dimensions.X + offset - 1, dimensions.Y + i);
                        if (!floorPlan[pos.X - offset, pos.Y])
                            yield return Tuple.Create<IntVec, Direction>(pos, Direction.RIGHT);
                    }

                    for (int i = 0; i < dimensions.Width; i++)
                    {
                        pos = new IntVec(dimensions.X + i, dimensions.Y + offset - 1);
                        if (!floorPlan[pos.X, pos.Y-offset])
                            yield return Tuple.Create<IntVec, Direction>(pos, Direction.DOWN);
                    }

                    if (dimensions.Width > 1)
                    for (int i = 0; i < dimensions.Height; i++)
                    {
                        pos = new IntVec(dimensions.X + dimensions.Width - offset, dimensions.Y + i);
                        if (!floorPlan[pos.X+offset, pos.Y])
                            yield return Tuple.Create<IntVec, Direction>(pos, Direction.LEFT);
                    }

                    if (dimensions.Height != 1)
                    for (int i = 0; i < dimensions.Width; i++)
                    {
                        pos = new IntVec(dimensions.X + i, dimensions.Y + dimensions.Height - offset);
                        if (!floorPlan[pos.X, pos.Y + offset])
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
            }

            bool[,] isFloor;
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

        public static Level generate(int seed, int levels)
        {
            Random rand = new Random(seed);

            __FloorPlan floorPlan = createFloorPlan(rand, levels);

            GridBoundList<IEnvironmentObject> environment = new GridBoundList<IEnvironmentObject>();
            GridBoundList<IInteractable> interactableEnvironment = new GridBoundList<IInteractable>();
            GridBoundList<ILightSource> lightSources = new GridBoundList<ILightSource>();
            GridBoundList<GameCharacter> characters = new GridBoundList<GameCharacter>();

            int entryRoom = rand.Next( floorPlan.hallstart );
            while (floorPlan.rooms[entryRoom].type != __FloorPlan.__Room.__RoomType.FOYER && floorPlan.rooms[entryRoom].type != __FloorPlan.__Room.__RoomType.TREASURE_ROOM )
            {
                entryRoom = (entryRoom + 1) % floorPlan.hallstart;
            }
            __FloorPlan.__Room start = floorPlan.rooms[entryRoom];
            IntVec startPoint = start.GetCenter();
            Engine.Engine.Log( string.Format( "Start point: <{0}, {1}>", startPoint.X, startPoint.Y) );
            floorPlan.rooms[entryRoom].type = __FloorPlan.__Room.__RoomType.EMPTY;

            int endRoom = (entryRoom + floorPlan.hallstart / 2) % floorPlan.hallstart;
            while (floorPlan.rooms[endRoom].type != __FloorPlan.__Room.__RoomType.FOYER && floorPlan.rooms[endRoom].type != __FloorPlan.__Room.__RoomType.TREASURE_ROOM)
            {
                endRoom = (endRoom + 1) % floorPlan.hallstart;
            }
            __FloorPlan.__Room end = floorPlan.rooms[ endRoom ];
            floorPlan.rooms[endRoom].type = __FloorPlan.__Room.__RoomType.EMPTY;
            Engine.Engine.Log(string.Format("End point: <{0}, {1}>", end.GetCenter().X, end.GetCenter().Y));
            interactableEnvironment.Add( new ColorEnvironment(Color.Blue, true), end.GetCenter());


            foreach (var room in floorPlan.rooms)
            {
                populateEnvironmentObjects(room, environment, rand);
                populateInteractiveEnvironmentObjects(room, interactableEnvironment, rand);
                populateLightSources(room, lightSources, rand);
                populateGameCharacters(room, characters, rand);
            }

            Level result = new Level( startPoint, floorPlan.tiles, environment, interactableEnvironment, lightSources, characters);

            if (!result.isComplete())
            {
                Engine.Engine.Log( "Level contains places which are impossible to reach from the starting position" );
            }

            return result;
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
                environ.Add(new ColorEnvironment(col, false), pos);
            }
        }

        private static void populateInteractiveEnvironmentObjects(__FloorPlan.__Room room, GridBoundList<IInteractable> interact, Random rand)
        {
            switch (room.type)
            {
                case __FloorPlan.__Room.__RoomType.DOORWAY:
                    interact.Add(new Door(), new IntVec(room.dimensions.X, room.dimensions.Y));
                    break;
                case __FloorPlan.__Room.__RoomType.TREASURE_ROOM:
                    Item[] items = new Item[ rand.Next( 2, 8 ) ];
                    for (int i = 0; i < items.Length; i++)
                    {
                        items[i] = Item.randomItem(10, 10);
                    }
                    interact.Add(new Chest(items), room.GetCenter());
                    //interact.Add(new ColorEnvironment( Color.Brown, true ), room.GetCenter());
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

        private static void populateGameCharacters(__FloorPlan.__Room room, GridBoundList<GameCharacter> chars, Random rand)
        {
            switch (room.type)
            {
                case __FloorPlan.__Room.__RoomType.BOSS_ROOM:
                    chars.Add(EnemyCreator.GetRandomBoss(9001), room.GetCenter());
                    break;
                case __FloorPlan.__Room.__RoomType.NOTHING_SPECIAL:
                    foreach (var pos in room.GetCells())
                    {
                        if (rand.NextDouble() > 0.98)
                            chars.Add(EnemyCreator.GetRandomEnemy(1,40).ElementAt(0), pos);
                        //lights.Add(new ColorEnvironment(new Color(rand.Next(100,256), rand.Next(100,256), rand.Next(100,256)), false), position);
                    }
                    break;
                case __FloorPlan.__Room.__RoomType.MOB_ROOM:
                        List<Enemy> enemies = EnemyCreator.GetRandomEnemy(rand.Next(2,6),40 );
                        int dropped = 1;
                        chars.Add(enemies.ElementAt(0), room.GetCenter() );
                        foreach (var dir in Direction.Values)
                        {
                            if( dropped < enemies.Count )
                                chars.Add(enemies.ElementAt(dropped), room.GetCenter() + dir);
                            dropped++;
                        }
                    break;
            }
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
