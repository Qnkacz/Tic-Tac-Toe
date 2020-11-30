using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tic_Tac_Toe
{
    class GameManager
    {
        int turnCounter = 0;

        private bool ourTurn = new bool();
        private bool whoStartsBackup = new bool();
        private bool AIvsAI = new bool();
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

        public GameManager(bool weFirst = true, bool RobotWars = false)
        {
            Console.WindowWidth = 70;
            Console.Title = "Kółko i krzyżyk | Bartosz Wąsik i Alan Hudela";
            Display();
            whoStartsBackup = weFirst;
            ourTurn = whoStartsBackup;
            
            AIvsAI = RobotWars;
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
                Console.Write("Wpisz numer wiersza: ");
                //loop która gwarantuje żeby była wprowadzona liczba
                do
                {
                    _val = string.Empty;
                    x = -1;
                    y = -1;
                    key = Console.ReadKey(true); //wprowadzamy klawisz
                    if (possibleInputs.Contains(key.Key)) //jezeli nie jest to backspace to działamy
                    {
                        double val = 0;
                        bool _x = double.TryParse(key.KeyChar.ToString(), out val);
                        if (_x)
                        {
                            _val += key.KeyChar;
                            x = Convert.ToInt32(_val);
                            Console.Write(x);
                        }
                    }
                }
                while (x == -1);
                Console.Write(" | Wpisz numer kolumny: ");
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
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("TURA GRACZA");
                    Console.ResetColor();
                    break;

                case false:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("TURA AI");
                    Console.ResetColor();
                    break;
            }
        }

        //gameloop
        public void GameLoop()
        {
            do
            {
                if (AIvsAI)
                {
                    AIwar();
                }
                else
                {
                    HUMANvsAI();
                }
                //if (board.ChechWhoWon() != 2) HighlightWin( board.ChechWhoWon() );
                turnCounter = 0;
                ourTurn = whoStartsBackup;
                board = new TicTacToe();
                Console.WriteLine("Wciśnij enter, aby rozegrać kolejną partię.");

            } while (Console.ReadKey().Key == ConsoleKey.Enter);
        }
        public void HUMANvsAI()
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
                turnCounter++;
            }
            while (board.IsPlaceAvaible() == true && board.ChechWhoWon() == 2);
            // powtarzaj dopoki są wolne miejsca i nikt jeszcze nie wygrał
        }
        public void AIwar()
        {
            do
            {
                switch (ourTurn)
                {
                    // Tura gracza
                    case true:
                        ShowWhoseTurn();
                        AI_Turn(1);
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
                turnCounter++;
            }
            while (board.IsPlaceAvaible() == true && board.ChechWhoWon() == 2);
            // powtarzaj dopoki są wolne miejsca i nikt jeszcze nie wygrał
        }
        //tutaj zaprogramowałem output AI
        public void AI_Turn(int sign = 0)
        {
            if (turnCounter == 0)
            {
                FirstAITurn(sign);
                turnCounter++;
            }
            else
            {
               var place = board.GetBestPlaceFor(sign); // zwraca indeksy tablicy
                if (board.CanPlace(place.Item1, place.Item2))
                {
                    board.SetValue(place.Item1, place.Item2, sign);
                }
            }
        }
        public void FirstAITurn(int sign = 0)
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
                board.SetValue(thePlace.Item1, thePlace.Item2, sign);
            }
            
        }

        private void EndTurn()
        {
            ourTurn = !ourTurn;
        }

        private void HighlightWin(int winner)
        {
            // Można dorobić podświetlanie ale to w sumie wiecej roboty niz myslalem wiec zostawiam blank
        }
    }
}
