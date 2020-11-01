using System;

namespace Tic_Tac_Toe
{
    class Program
    {
        static void Main(string[] args)
        {
            // Mam nadzieje ze sie nie wysypalo pls
            GameManager GM = new GameManager(true);

            Console.WriteLine("TIC TAC TOE\n");

            //GM.Display();

            GM.Gameloop();
            //GM.AI_Turn();

        }
    }
}
