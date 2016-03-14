#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace Stomp
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static Game1 game;
        [STAThread]
        static void Main()
        {
            game = new Game1();
            game.Run();
        }
    }
#endif
}
