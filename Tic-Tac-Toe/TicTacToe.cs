﻿using System;
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
        private List<List<int>> board = new List<List<int>>();
        
        public TicTacToe()
        {
            /// Tworzymy listę list i wypełniamy ją 2kami
            int filler = 2;
            int[] blanks = { filler, filler, filler }; // linia
            List<int> line = blanks.ToList();

            for (int i = 0; i < 3; i++)
            {
                board.Add(line); // trzykrotnie wstawiamy puste linie
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
                    switch(board[i][j])
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
                            break;
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
                int[] values = {    board[0][columnNumber],
                                    board[1][columnNumber],
                                    board[2][columnNumber]};

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
                int[] values = {    board[rowNumber][0],
                                    board[rowNumber][1],
                                    board[rowNumber][2]};

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
                return board[row][column];
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
                switch (board[row][column])
                {
                    case 2:
                        return '.';
                        break;
                    case 1:
                        return 'x';
                        break;
                    case 0:
                        return 'o';
                        break;
                    default:
                        throw new System.ArgumentException("Something weird happened. ERROR!");
                        return 'f';
                        break;
                }
            }
            else
            {
                Console.WriteLine("Impossible row or column.");
                return 'f'; // F in the chat
            }
        }
    }
}
