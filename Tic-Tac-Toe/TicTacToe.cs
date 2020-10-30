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
        private int[,] board = new int [3,3];
        
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
            Console.Write(bars);
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

            board[row-1,column-1] = value;
        }

        //funckja sprawdzajaca czy jest wolne miejsce na planszy
        public bool IsPlaceAvaible()
        {

            for (int i = 0; i < board.GetUpperBound(0); i++)
            {
                for (int j = 0; j < board.GetUpperBound(1); j++)
                {
                    if (board[i,j] == 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        //sprawdzanie czy konkretne miejsce jest wolne
        public bool CanPlace(int x, int y)
        {
            if (board[x-1,y-1] == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
