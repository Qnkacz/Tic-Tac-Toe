using System;

namespace Tic_Tac_Toe
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager GM = new GameManager(false);

            Console.WriteLine("TIC TAC TOE\n");

            GM.Display();
        }
    }
}
