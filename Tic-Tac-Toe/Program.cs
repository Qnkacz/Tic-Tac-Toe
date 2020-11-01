using System;

namespace Tic_Tac_Toe
{
    class Program
    {
        static void Main(string[] args)
        {
            bool HumanStart = true;
            bool AIvsAI = false;
            
            GameManager GM = new GameManager(HumanStart, AIvsAI);
            GM.GameLoop();
        }
    }
}
