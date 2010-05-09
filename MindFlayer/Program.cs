using System;

namespace MindFlayer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (MindFlayer game = new MindFlayer())
            {
                game.Run();
            }
        }
    }
}

