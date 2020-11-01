using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tic_Tac_Toe
{
    class GameManager
    {
        int aiTurnCounter = 0;
        private int humanSign;
        private int aiSign;

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

            humanSign = 1;
            aiSign = 0;
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
                Console.WriteLine("Wpisz wartość wiersza, gdzie chcesz wstawić znak");
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
                Console.WriteLine("Wpisz wartość kolumny, gdzie chcesz wstawić znak");
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
            } while (!board.CanPlace(x-1, y-1));


            board.SetChar(x-1, y-1, 'x');
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
            do
            {
                switch (ourTurn)
                {
                    // Tura gracza
                    case true:
                        ShowWhoseTurn();
                        PlayerInput();
                        EndTurn();
                        Display();
                        break;

                    // Tura AI
                    case false:
                        ShowWhoseTurn();
                        AI_Turn();
                        EndTurn();
                        Display();
                        break;
                }
            }
            while (board.IsPlaceAvaible() == true && board.ChechWhoWon() == 2);
            // powtarzaj dopoki są wolne miejsca i nikt jeszcze nie wygrał
            
        }
        //tutaj zaprogramowałem output AI
        public void AI_Turn()
        {
            if (aiTurnCounter == 0)
            {
                FirstAITurn();
                aiTurnCounter++;
            }
            else
            {
               var place = board.GetBestPlaceFor(0); // zwraca indeksy tablicy
                if (board.CanPlace(place.Item1, place.Item2))
                {
                    board.SetChar(place.Item1, place.Item2, 'o');
                }
            }
        }
        public void FirstAITurn()
        {
            Random r = new Random();
            List<Tuple<int, int>> listofAvaiblePlaces = new List<Tuple<int, int>>();
            for (int i = 0; i < board.board.GetUpperBound(0); i++)
            {
                for (int j = 0; j < board.board.GetUpperBound(1); j++)
                {
                    if (board.board[i, j] == 2)
                    {
                        Tuple<int, int> place = new Tuple<int, int>(i, j);
                        listofAvaiblePlaces.Add(place);
                    }
                }
            }
            int randIndex = r.Next(0, listofAvaiblePlaces.Count);
            Tuple<int, int> thePlace = new Tuple<int, int>(listofAvaiblePlaces[randIndex].Item1, listofAvaiblePlaces[randIndex].Item2);
            if(board.CanPlace(thePlace.Item1, thePlace.Item2) == true)
            {
                board.SetChar(thePlace.Item1, thePlace.Item2, 'o');
            }
            
        }

        private void EndTurn()
        {
            ourTurn = !ourTurn;
        }
    }
}
