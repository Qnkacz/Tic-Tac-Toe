using System;

namespace Tic_Tac_Toe
{
    class Program
    {
        static void Main(string[] args)
        {
            bool HumanStart = true;
            bool AIvsAI = false;

            Console.WriteLine("TIC TAC TOE\n");
            if (!AIvsAI)
            {
                GameManager GM = new GameManager(HumanStart);
                GM.Gameloop();
            }
            else
            {
                GameManager GM = new GameManager(HumanStart);
                GM.RobotWars();
            }
        }
    }
}
