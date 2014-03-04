using Brogue.Mapping;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Engine
{
    partial class Engine
    {
        private static Minimap map;

        private static void DrawMiniMap(SpriteBatch uisb)
        {
            if (map == null || map.level != currentLevel )
            {
                map = new Minimap(currentLevel);
            }

            IntVec mapCenter = currentLevel.CharacterEntities.FindPosition(hero);

            map.UpdateView(mapCenter);

            IntVec offset = new IntVec(game.Width / 6 * 5, 20);
            IntVec mapSize = new IntVec(70, 70);
            Rectangle tempMap = new Rectangle(0, 0, mapSize.X, mapSize.Y);

            IntVec size = new IntVec(3, 3);

            FillRect(uisb, offset, mapSize * size, new Color(60,60,90,255));

            for (int x = 0; x < currentLevel.GetWidth(); x++)
            {
                for (int y = 0; y < currentLevel.GetHeight(); y++)
                {
                    IntVec position = new IntVec(x, y);
                    IntVec mapPos = position - mapCenter + mapSize / 2;

                    if( map.IsDetected( position ) && tempMap.Contains( mapPos.X, mapPos.Y ) )
                        DrawPoint(uisb, offset + mapPos * size, size, currentLevel.isFloor(position) ? (map.IsActive(position) ? new Color(160, 160, 160, 255) : Color.Gray) : (map.IsActive(position) ? new Color(210, 210, 210, 255) : Color.DarkGray));
                }
            }

            foreach (IntVec position in currentLevel.InteractableEnvironment.Positions())
            {
                IntVec mapPos = position - mapCenter + mapSize / 2;

                if (map.IsActive(position) && tempMap.Contains(mapPos.X, mapPos.Y))
                    DrawPoint(uisb, offset + mapPos * size, size, Color.Magenta);
            }

            foreach (IntVec position in currentLevel.DroppedItems.Positions())
            {
                IntVec mapPos = position - mapCenter + mapSize / 2;

                if (map.IsActive(position) && tempMap.Contains(mapPos.X, mapPos.Y))
                    DrawPoint(uisb, offset + mapPos * size, size, Color.Green);
            }

            foreach (IntVec position in currentLevel.CharacterEntities.Positions())
            {
                IntVec mapPos = position - mapCenter + mapSize / 2;

                if (map.IsActive(position) && tempMap.Contains(mapPos.X, mapPos.Y))
                    DrawPoint(uisb, offset + mapPos * size, size, Color.Red);
            }

            DrawPoint(uisb, offset + (mapSize / 2) * size, size, Color.Gold);
        }

        static DynamicTexture empty = GetTexture("levelTileset");

        private static void DrawPoint(SpriteBatch uisb, IntVec position, IntVec size, Color color)
        {
            uisb.Draw(empty.texture, new Rectangle(position.X, position.Y, size.X, size.Y), new Rectangle(5, 5, 1, 1), color);
        }

        private static void FillRect(SpriteBatch uisb, IntVec position, IntVec dimensions, Color color)
        {
            uisb.Draw(empty.texture, new Rectangle(position.X, position.Y, dimensions.X, dimensions.Y), new Rectangle(5, 5, 1, 1), color);
        }


        static Dictionary<string, DynamicTexture> textureDictionary;
        static List<string> subscribed;

        public static DynamicTexture GetTexture(string path)
        {
            textureDictionary = textureDictionary ?? new Dictionary<string, DynamicTexture>();
            subscribed = subscribed ?? new List<string>();

            DynamicTexture result = null;

            if (!textureDictionary.TryGetValue(path, out result))
            {
                if (contentManager == null)
                {
                    result = new DynamicTexture();
                    textureDictionary.Add(path, result);
                    subscribed.Add(path);
                }
                else
                {
                    DynamicTexture newTex =  new DynamicTexture(contentManager.Load<Texture2D>(path));
                    textureDictionary.Add(path, newTex);
                    result = newTex;
                }
            }
            return result;
        }


        static ContentManager contentManager;

        public static void LoadContent(ContentManager content)
        {
            contentManager = content;
            
            foreach (string path in subscribed)
            {
                textureDictionary[path].texture = contentManager.Load<Texture2D>(path);
            }


            ContentLoaded(content);




        }
    }
}
