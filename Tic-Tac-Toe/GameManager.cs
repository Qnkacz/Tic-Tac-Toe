using System;
using System.Collections.Generic;
using System.Text;

namespace Tic_Tac_Toe
{
    class GameManager
    {
        private bool ourTurn = new bool();
        TicTacToe board = new TicTacToe();

        public GameManager(bool weFirst = true)
        {
            ourTurn = weFirst;
            ShowWhoseTurn();
        }

        public void ExecuteTurn()
        {
            /// DO ZROBIENIA
            switch(ourTurn)
            {
                // Tura gracza
                case true:
                    // COS
                    break;

                // Tura komputera
                case false:
                    // COS
                    break;
            }
        }

        public void PlayerInput()
        {
            /// DO ZROBIENIA
            // Interfejs do wprowadzenia ruchu
        }

        public void Display()
        {
            board.Display();
        }

        public void ShowWhoseTurn()
        {
            // Wypisuje czyja jest tura. Chyba tylko do konstruktora
            switch(ourTurn)
            {
                case true:
                    Console.WriteLine("TURA GRACZA");
                    break;

                case false:
                    Console.WriteLine("TURA AI");
                    break;
            }
        }
    }
}
