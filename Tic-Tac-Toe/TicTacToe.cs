using System;
using System.Collections.Generic;
using System.Linq;

namespace Tic_Tac_Toe
{
    class TicTacToe
    {
        /// Przechowuje plansze gry
        // Możliwe wartości
        // 2 - Brak wstawionego znaku
        // 0 - 0 - kółko
        // 1 - X - iks
        public int[,] board = new int [3,3];
        int[,] magicSquare = new int[3, 3]
            {
                { 8,1,6},
                {3,5,7 },
                {4,9,2 }
            };

        public TicTacToe()
        {
            /// Tworzymy listę list i wypełniamy ją 2kami
            int filler = 2;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i,j] = filler;
                }
            }
        }
        public void Display() // Rysuje stan gry w konsoli
        {
            string bars = "████████";
            Console.WriteLine(bars);
            for (int i = 0; i < 3; i++)
            {
                Console.Write("█");
                for (int j = 0; j < 3; j++)
                {
                    switch(board[i,j])
                    {
                        case 2:
                            Console.Write("  ");
                            break;
                        case 1:
                            Console.Write("X ");
                            break;
                        case 0:
                            Console.Write("O ");
                            break;
                        default:
                            throw new System.ArgumentException("Parameter other than [0,2]. ERROR!");
                    }
                }
                Console.WriteLine("█");
            }
            Console.WriteLine(bars);
        }

        public List<int> GetColumn(int columnNumber)
        {
            // GetColumn albo zwraca pustą listę -> zły input
            //           w przeciwnym wypadku zwraca listę wartości w kolumnie
            if (columnNumber > 0 && columnNumber < 3)
            {
                int[] values = {    board[0,columnNumber],
                                    board[1,columnNumber],
                                    board[2,columnNumber]};

                return values.ToList();
            }
            else
            {
                Console.WriteLine("columnNumber is lower than 0 or higher than 2");

                return new List<int>();
            }
        }
        public List<int> GetRow(int rowNumber)
        {
            // GetRow albo zwraca pustą listę -> zły input
            //           w przeciwnym wypadku zwraca listę wartości w wierszu
            if (rowNumber > 0 && rowNumber < 3)
            {
                int[] values = {    board[rowNumber,0],
                                    board[rowNumber,1],
                                    board[rowNumber,2]};

                return values.ToList();
            }
            else
            {
                Console.WriteLine("columnNumber is lower than 0 or higher than 2");

                return new List<int>();
            }
        }

        public int GetValue(int row, int column)
        {
            // Przy złej kolumnie/wierszu zwraca -1
            // W przeciwnym wypadku zwraca wartość stosując wzorzec 
            // 2 - Brak wstawionego znaku
            // 0 - 0 - kółko
            // 1 - X - iks
            if (row > 0 && row < 3 && column > 0 && column < 3)
            {
                return board[row,column];
            }
            else
            {
                Console.WriteLine("Impossible row or column.");
                return -1;
            }
        }

        public int GetSign(int row, int column)
        {
            // Przy złej kolumnie/wierszu zwraca 'f'
            // W przeciwnym wypadku zwraca znak stosując wzorzec 
            // . - Brak wstawionego znaku
            // o - 0 - kółko
            // x - X - iks
            if (row > 0 && row < 3 && column > 0 && column < 3)
            {
                switch (board[row,column])
                {
                    case 2:
                        return '.';
                    case 1:
                        return 'x';
                    case 0:
                        return 'o';
                    default:
                        return 'f';
                        
                }
            }
            else
            {
                Console.WriteLine("Impossible row or column.");
                return 'f'; // F in the chat
            }
        }

        public void SetValue(int row, int column, int value)
        {
            int[] goodValues = new int[] { 0, 1 };
            if (row < 0 && row > 3 && column < 0 && column > 3)
            {
                Console.WriteLine("Impossible row or column.");
                return;
            }
            if ( !goodValues.Contains(value))
            {
                Console.WriteLine("You can't insert anything else but O or X.");
                Console.WriteLine("( 0 -> O | 1 -> X )");
                return;
            }

            // Jeżeli się tutaj dostaliśmy to wszystko powinno być w porządku i możemy ustawić wartość
            board[row,column] = value;
        }
        public void SetChar(int row, int column, char c)
        {
            char[] goodValues = new char[] { 'o', 'x' };
            if (row < 0 && row > 3 && column < 0 && column > 3)
            {
                Console.WriteLine("Impossible row or column.");
                return;
            }
            if (!goodValues.Contains(Char.ToLower(c)))
            {
                Console.WriteLine("You can't insert anything else but O or X.");
                Console.WriteLine("( 0 -> O | 1 -> X )");
                return;
            }

            // Jeżeli się tutaj dostaliśmy to wszystko powinno być w porządku i możemy ustawić wartość
            int value = new int();
            if (c == 'x') value = 1;
            if (c == 'o') value = 0;

            board[row,column] = value;
        }

        //funckja sprawdzajaca czy jest wolne miejsce na planszy
        public bool IsPlaceAvaible()
        {

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i,j] == 2)
                    {
                        //Console.WriteLine("i: "+i+" j: "+j+" value: "+board[i,j]);
                        return true;
                    }
                }
            }
            Console.WriteLine("nie ma miejsca na planszy");
            return false;
        }
        //sprawdzanie czy konkretne miejsce jest wolne
        public bool CanPlace(int x, int y)
        {
            if (board[x,y] == 2) // zmiana z board[x-1,y-1] bo w GetBestPlace zwracasz indeksy boardu
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // sprawdza wygranego za pomocą magic square
        // https://mathworld.wolfram.com/MagicSquare.html
        //returns 0 if AI won
        //return 1 if human won
        //return 2 if no winner
        public int ChechWhoWon()
        {
            
            int AI_Value = 0;
            int Human_Value = 0;
            for (int i = 0; i < board.GetUpperBound(0); i++)
            {
                for (int j = 0; j < board.GetUpperBound(1); j++)
                {
                    if (board[i, j] == 1)
                    {
                        Human_Value += magicSquare[i, j];
                    }
                    if (board[i, j] == 0)
                    {
                        AI_Value += magicSquare[i, j];
                    }
                }
            }
            if (AI_Value >= 15)
            {
                Console.WriteLine("Wygral gracz!");
                return 0;
            }
            else if (Human_Value >= 15)
            {
                Console.WriteLine("Wygrala sztuczna inteligencja!");
                return 1;
            }
            else
            {
                return 2;
            }
        }
        //oddaje tablice [x,y] z koordynatami na najlepszy ruch
        //musisz wprowadzic dla kogo jest liczone
        //1-dla gracza
        //0-dla AI
        public Tuple<int,int> GetBestPlaceFor(int player)
        {
            Dictionary<Tuple<int,int>, int> RankingOfChoices = new Dictionary<Tuple<int, int>, int>();
            for (int i = 0; i < board.GetUpperBound(0); i++)
            {
                for (int j = 0; j < board.GetUpperBound(1); j++)
                {
                    if (board[i, j] == 2) //znalazło wolne miejsce
                    {
                        int value = 0; //mamy value danego miejsca
                        var row = GetRow(i).ToArray();
                        var col = GetColumn(j).ToArray();
                        
                        for (int x = 0; x < col.Length; x++) //dodajemy value w zaleznosci od kolumny
                        {
                            if (col[x] == player) value += 10;
                        }
                        for (int x = 0; x < row.Length; x++) //dodajemy value w zaleznosci od wiersza
                        {
                            if (row[x] == player) value += 10;
                        }
                        // NIE LICZYMY WARTOSCI PO SKOSIE

                        Tuple<int, int> place = new Tuple<int, int>(i, j);
                        RankingOfChoices.Add(place, value);
                    }
                }
            }
            var sortedDict = RankingOfChoices.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            RankingOfChoices = sortedDict;
            return RankingOfChoices.Keys.First();
        }
    }
}
