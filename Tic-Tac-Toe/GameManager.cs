using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tic_Tac_Toe
{
    class GameManager
    {
        private bool ourTurn = new bool();
        TicTacToe board = new TicTacToe();
        ConsoleKey[] possibleInputs = new ConsoleKey[]
        {
                ConsoleKey.D1,
                ConsoleKey.D2,
                ConsoleKey.D3,
                ConsoleKey.NumPad1,
                ConsoleKey.NumPad2,
                ConsoleKey.NumPad3
        };

        public GameManager(bool weFirst = true)
        {
            ourTurn = weFirst;
            ShowWhoseTurn();
        }

        public void ExecuteTurn()
        {
            /// DO ZROBIENIA
            switch (ourTurn)
            {
                // Tura gracza
                case true:
                    //char is x
                    //sprawdzanie czy mozna
                    //sprawdzanie czy wygranko
                    break;

                // Tura komputera
                case false:
                    //char is o
                    //sprawdzanie czy mozna
                    //sprawdzanie czy wygranbkjo
                    break;
            }
        }
        /// <summary>
        /// jest to funkcja, która zbiera input od fizycznego gracza
        /// </summary>
        /// <returns></returns>
        public void PlayerInput()
        {
            // Interfejs do wprowadzenia ruchu
            ConsoleKeyInfo key;
            string _val = string.Empty;
            int x = -1;
            int y = -1;
            do
            {
                Console.WriteLine("Wpisz wartość kolumny, gdzie chcesz wstawić znak");
                //loop która gwarantuje żeby była wprowadzona liczba
                do
                {
                    key = Console.ReadKey(true); //wprowadzamy klawisz
                    if (possibleInputs.Contains(key.Key)) //jezeli nie jest to backspace to działamy
                    {
                        double val = 0;
                        bool _x = double.TryParse(key.KeyChar.ToString(), out val);
                        if (_x)
                        {
                            _val += key.KeyChar;
                            x = Convert.ToInt32(_val);
                            Console.WriteLine(x);
                        }
                    }
                }
                while (x == -1);
                Console.WriteLine("Wpisz wartość wiersza, gdzie chcesz wstawić znak");
                //powtarzamy dla wiersza
                _val = string.Empty;
                do
                {
                    key = Console.ReadKey(true); //wprowadzamy klawisz
                    if (possibleInputs.Contains(key.Key)) //jezeli nie jest to backspace to działamy
                    {
                        double val = 0;
                        bool _x = double.TryParse(key.KeyChar.ToString(), out val);
                        if (_x)
                        {
                            _val += key.KeyChar;
                            y = Convert.ToInt32(_val);
                            Console.WriteLine(y);
                        }
                    }
                }
                while (y == -1);
            } while (!board.CanPlace(x, y));


            board.SetChar(x, y, 'x');
            board.Display();
        }

        public void Display()
        {
            board.Display();
        }

        public void ShowWhoseTurn()
        {
            // Wypisuje czyja jest tura. Chyba tylko do konstruktora
            switch (ourTurn)
            {
                case true:
                    Console.WriteLine("TURA GRACZA");
                    break;

                case false:
                    Console.WriteLine("TURA AI");
                    break;
            }
        }

        ///TODO
        //gameloop
        public void Gameloop()
        {
            while (board.IsPlaceAvaible() == true)
            {
                //gamellop
            }
        }

        //
    }
}
