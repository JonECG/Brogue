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

    public class AuxerSwitchTest
    {
        Mage bob;
        Switch lever;
        Switch leverTwo;
        Door targetObject;

        public void runTest()
        {
            testOneCreatingSwich();
            testTwoLinkSwitchToInteractable();
            testThreeActOnSwichToObject();
        }

        public void testOneCreatingSwich()
        {
            bool passed = false;

            lever = new Switch();

            if (lever != null)
            {
                passed = true;
            }

            if (!passed)
            {
                throw new System.ArgumentException("Swiches was not created", "testOneCreatingSwich");
            }
        }

        public void testTwoLinkSwitchToInteractable()
        {
            bool passed = false;

            targetObject = new Door();
            lever = new Switch(targetObject);

            if (lever.target != null)
            {
                passed = true;
            }

            if (!passed)
            {
                throw new System.ArgumentException("Switch did not link to object", "testTwoLinkSwitchToInteractable");
            }
        }

        public void testThreeActOnSwichToObject()
        {
            bool passed = false;
            bob = new Mage();
            targetObject = new Door(Direction.UP);
            lever = new Switch(targetObject);

            Direction first = targetObject.directionFacing;
            lever.actOn(bob);
            Direction second = targetObject.directionFacing;

            if (first != second)
            {
                passed = true;
            }

            if (!passed)
            {
                throw new System.ArgumentException("Switch acting on object", "testThreeActOnSwichToObject");
            }
        }
    }

    public class AuxerStairTest
    {
        Mage bob;
        Stairs stair;


        public void runTest()
        {
            testOneCreatingStair();
            testThreeActOnSwichToObject();
        }

        public void testOneCreatingStair()
        {
            bool passed = false;

            stair = new Stairs();

            if (stair != null)
            {
                passed = true;
            }

            if (!passed)
            {
                throw new System.ArgumentException("Swiches was not created", "testOneCreatingSwich");
            }
        }

        public void testThreeActOnSwichToObject()
        {
            bool passed = false;

            bob = new Mage();
            stair = new Stairs();

            stair.actOn(bob);

            if (stair.calledNextLevel == true)
            {
                passed = true;
            }

            if (!passed)
            {
                throw new System.ArgumentException("Switch acting on object", "testThreeActOnSwichToObject");
            }
        }

    }

    public class AuxerTestingZone : Microsoft.Xna.Framework.Game
    {

        AuxerHiddenPassageTest testHiddenPassage;
        AuxerSwitchTest testSwitch;
        AuxerStairTest testStairs;

        public AuxerTestingZone()
        {
            Content.RootDirectory = "Content";

            testHiddenPassage = new AuxerHiddenPassageTest();
            testSwitch = new AuxerSwitchTest();
            testStairs = new AuxerStairTest();
        }

        public void runTests()
        {
            //testHiddenPassage.runTest();
            //testSwitch.runTest();
            testStairs.runTest();
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
