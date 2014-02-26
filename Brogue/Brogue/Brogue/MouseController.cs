using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Brogue
{
    static class MouseController
    {

        private static MouseState state, prevstate;
        private static IntVec worldPosition = new IntVec(0, 0);

        public static void Update()
        {
            prevstate = state;
            state = Mouse.GetState();
            Matrix mouseMat = Matrix.Invert(Engine.Engine.worldToView);
            Vector3 test = Vector3.Transform(new Vector3(state.X + Engine.Engine.CELLWIDTH / 2, state.Y + Engine.Engine.CELLWIDTH / 2, 0), mouseMat);
            worldPosition.X = (int)(test.X / Engine.Engine.CELLWIDTH);
            worldPosition.Y = (int)(test.Y / Engine.Engine.CELLWIDTH);
        }

        public static bool LeftClicked()
        {
            return (state.LeftButton == ButtonState.Pressed && prevstate.LeftButton == ButtonState.Released) ;
        }
        public static bool RightClicked()
        {
            return (state.RightButton == ButtonState.Pressed && prevstate.RightButton == ButtonState.Released);
        }
        public static bool MiddleClicked()
        {
            return (state.MiddleButton == ButtonState.Pressed && prevstate.MiddleButton == ButtonState.Released);
        }

        public static bool LeftDown()
        {
        	return (state.LeftButton == ButtonState.Pressed);
        }

        public static bool RightDown()
        {
            return (state.RightButton == ButtonState.Pressed);
        }

        public static bool MiddleDown()
        {
            return (state.MiddleButton == ButtonState.Pressed);
        }

        public static IntVec MouseGridPosition()
        {
            return worldPosition;
        }
    }
}
