using System;

namespace Brogue
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //using (Game1 game = new Game1())
            //{
            //    game.Run();
            //}

            using (JonTest game = new JonTest())
            {
                game.Run();
            }
        }
    }
#endif
}
