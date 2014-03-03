using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue;
using Brogue.Engine;
using Brogue.HeroClasses;
using Brogue.EnviromentObjects.Interactive;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Brogue
{
    public class AuxerHiddenPassageTest 
    {
        Mage bob;
        HiddenPassage HPone;
        HiddenPassage HPtwo;

        public AuxerHiddenPassageTest()
        {

        }

        public void runTest()
        {
            testOneCreatingHiddsenPassage();
            testTwoLinkHiddenPassages();
            testThreeActOnMovesGameCharacter();
        }

        public void testOneCreatingHiddsenPassage()
        {
            bool passed = false;

            HPone = new HiddenPassage();
            if (HPone != null)
            {
                passed = true;
            }


            if (!passed)
            {
                throw new System.ArgumentException("HiddenPassage was not created", "testOneCreatingHiddsenPassage");
            }
        }

        public void testTwoLinkHiddenPassages()
        {
            bool passed = false;

            HPone = new HiddenPassage();
            HPtwo = new HiddenPassage(HPone);
           
            if (HPtwo.other != null)
            {
                passed = true;
            }


            if (!passed)
            {
                throw new System.ArgumentException("HiddenPassage's did not link", "testTwoLinkHiddenPassages");
            }
        }

        public void testThreeActOnMovesGameCharacter()
        {
            bool passed = false;

            HPone = new HiddenPassage();
            HPtwo = new HiddenPassage(HPone);

            bob = new Mage();

            HPone.testPlayerPosition = new IntVec(5, 3);
            HPone.testPosition = new IntVec(5, 4);
            HPtwo.testPosition = new IntVec(2, 6);

            HPone.directionFacing = Direction.UP;
            HPtwo.directionFacing = Direction.DOWN;

            IntVec preveosSpot = HPone.testPlayerPosition;

            HPone.actOn(bob);
            HPtwo.actOn(bob);

            IntVec finalSpot = HPone.testPlayerPosition;

            HPone = new HiddenPassage();
            if (preveosSpot != finalSpot)
            {
                passed = true;
            }

            if (!passed)
            {
                throw new System.ArgumentException("The GameCharacter position did not change", "testThreeActOnMovesGameCharacter");
            }
        }

    }

    public class AuxerSwichTest
    {

    }



    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class AuxerTestingZone : Microsoft.Xna.Framework.Game
    {

        AuxerHiddenPassageTest testHiddenPassage;

        public AuxerTestingZone()
        {
            Content.RootDirectory = "Content";

            testHiddenPassage = new AuxerHiddenPassageTest();
            //testHiddenPassage.runTest();
        }

        public void runTests()
        {
            testHiddenPassage.runTest();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Engine.Engine.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

        }
    }



}
