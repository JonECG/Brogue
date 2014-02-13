using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Mapping
{
    public class Level : IRenderable
    {
        Tile[,] tiles;
        //EnvironmentObject[] environment;
        List<Item> droppedItems;
        List<GameCharacter> characterEntities;
        bool[,] cachedSolid;

        public static Level generate(int seed, int levels)
        {
            Random rand = new Random(seed);

            //bool[,] module = createModule(rand, levels);


            
            const int PADDING = 4;

            int maxPlayGround = 1500;
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
                int roomWidth = rand.Next(10) + 6;
                int roomHeight = rand.Next(10) + 6;
                int anchorX = rand.Next(roomWidth - PADDING) + PADDING/2;
                int anchorY = rand.Next(roomHeight - PADDING) + PADDING/2;

                for (int x = 1; x < roomWidth; x++)
                {
                    for (int y = 1; y < roomHeight; y++)
                    {
                        playGround[x - anchorX + targetX, y - anchorY + targetY] = true;
                    }
                }

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
                        startingIndex = i+1;
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

                

                for( int i = 0; i < edges.Length; i++ )
                {
                    Vector2 vec = edges[(i + startingIndex) % edges.Length];
                    int x = (int)vec.X;
                    int y = (int)vec.Y;

                    waitingForDoor = previousWasEmpty && playGround[x,y];

                    bool newSolid;
                    if (waitingForDoor && previousWasEmpty && !corners.Contains( vec ) )
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
                    
                     
                    previousWasEmpty = !playGround[x,y];
                    
                    playGround[x,y] = newSolid;
                }

                left = Math.Min(left, targetX - anchorX);
                right = Math.Max(right, roomWidth + targetX - anchorX );
                bottom = Math.Min(bottom, targetY - anchorY );
                top = Math.Max(top, roomHeight + targetY - anchorY );

                targetX += rand.Next(roomWidth - PADDING) + PADDING/2 - anchorX;
                targetY += rand.Next(roomHeight - PADDING) + PADDING/2 - anchorY;
            }

            //Widen single areas
            //bool[,] copy = new bool[playGround.GetLength(0), playGround.GetLength(1)];
            //for (int x = 0; x < copy.GetLength(0); x++)
            //{
            //    for (int y = 0; y < copy.GetLength(1); y++)
            //    {
            //        copy[x, y] = playGround[x, y];
            //    }
            //}
            //for (int x = left; x < right; x++)
            //{
            //    for (int y = bottom; y < top; y++)
            //    {
            //        if (copy[x, y])
            //        {
            //            if ( (!copy[x - 1, y] && !copy[x + 1, y]) || (!copy[x, y - 1] && !copy[x, y + 1]) )
            //            {
            //                for (int i = -1; i <= 1; i++)
            //                {
            //                    for (int j = -1; j <= 1; j++)
            //                    {
            //                        playGround[x + i, y + j] = true;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}


            Level result = new Level();

            result.tiles = new Tile[right - left, top - bottom];

            for (int x = left; x < right; x++)
            {
                for (int y = bottom; y < top; y++)
                {
                    result.tiles[x - left, y - bottom] = new Tile(playGround[x,y]);
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

        public void render(SpriteBatch sb)
        {
            //sb.Draw(Tile.tileset, new Rectangle(0, 0, 48, 48), new Rectangle(0, 0, 48, 48), Color.White);
            int tileWidth = 640 / Math.Max( tiles.GetLength(0), tiles.GetLength(1) );

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if( tiles[x,y].isSolid )
                        sb.Draw(Tile.tileset, new Rectangle(x * tileWidth, y * tileWidth, tileWidth, tileWidth), new Rectangle(0, 0, 48, 48), Color.White);
                }
            }
        }
    }
}
