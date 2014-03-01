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
        private static void DrawMiniMap(SpriteBatch uisb)
        {
            IntVec offset = new IntVec(game.Width / 4 * 3, 20);
            IntVec size = new IntVec(2, 2);
        //
            for (int x = 0; x < currentLevel.GetWidth(); x++)
            {
                for (int y = 0; y < currentLevel.GetHeight(); y++)
                {
                    IntVec position = new IntVec(x, y);
                    DrawPoint(uisb, offset + position * 2, size, currentLevel.isSolid(position) ? Color.Gray : Color.DarkGray);
                }
            }
        //
            foreach (IntVec position in currentLevel.InteractableEnvironment.Positions())
            {
                DrawPoint(uisb, offset + position * 2, size, Color.Magenta);
            }
        //
            foreach (IntVec position in currentLevel.DroppedItems.Positions())
            {
                DrawPoint(uisb, offset + position * 2, size, Color.Green);
            }
        //
            foreach (IntVec position in currentLevel.CharacterEntities.Positions())
            {
                DrawPoint(uisb, offset + position * 2, size, Color.Red);
            }
        //
            DrawPoint(uisb, offset + currentLevel.CharacterEntities.FindPosition(hero) * 2, size, Color.Gold);
        }
        //
        static DynamicTexture empty = GetTexture("levelTileset");

        private static void DrawPoint(SpriteBatch uisb, IntVec position, IntVec size, Color color)
        {
            uisb.Draw(empty.texture, new Rectangle(position.X, position.Y, size.X, size.Y), new Rectangle(5, 5, 1, 1), color);
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
