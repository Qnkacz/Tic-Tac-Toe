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
        public int[,] board = new int[3, 3];
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
                    board[i, j] = filler;
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
                    switch (board[i, j])
                    {
                        case 2:
                            Console.Write("  ");
                            break;
                        case 1:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("X ");
                            Console.ResetColor();
                            break;
                        case 0:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("O ");
                            Console.ResetColor();
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
                return board[row, column];
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
                switch (board[row, column])
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
            if (!goodValues.Contains(value))
            {
                Console.WriteLine("You can't insert anything else but O or X.");
                Console.WriteLine("( 0 -> O | 1 -> X )");
                return;
            }

            // Jeżeli się tutaj dostaliśmy to wszystko powinno być w porządku i możemy ustawić wartość
            board[row, column] = value;
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

            board[row, column] = value;
        }

        //funckja sprawdzajaca czy jest wolne miejsce na planszy
        public bool IsPlaceAvaible()
        {

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 2)
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
            if (board[x, y] == 2) // zmiana z board[x-1,y-1] bo w GetBestPlace zwracasz indeksy boardu
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //returns 0 if AI won
        //return 1 if human won
        //return 2 if no winner
        public int ChechWhoWon()
        {
            //wiersze i kiolumny
            for (int i = 0; i < 3; i++)
            {
                if(board[i,0]==1&& board[i, 1]==1&&board[i, 2] == 1)
                {
                    //Console.WriteLine("Wygrał ");
                    
                    return 1;
                }
                else if(board[0,i]==1 && board[1,i]==1 && board[2, i] == 1)
                {
                    //Console.WriteLine("kolumny gracz");
                    return 1;
                }
            }
            //skoksy
            if (board[0, 0] == 1 && board[1, 1] == 1 && board[2, 2] == 1)
            {
                return 1;
            }
            if (board[0, 2] == 1 && board[1, 1] == 1 && board[2, 0] == 1)
            {
                return 1;
            }
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == 0 && board[i, 1] == 0 && board[i, 2] == 0)
                {
                    //Console.WriteLine("kolumny AI");
                    return 0;
                }
                else if (board[0, i] == 0 && board[1, i] == 0 && board[2, i] == 0)
                {
                    //Console.WriteLine("w AI");
                    return 0;
                }
            }
            //skoksy
            if (board[0, 0] == 0 && board[1, 1] == 0 && board[2, 2] == 0)
            {
                return 0;
            }
            if (board[0, 2] == 0 && board[1, 1] == 0 && board[2, 0] == 0)
            {
                return 0;
            }
            return 2;
        }
        //oddaje tablice [x,y] z koordynatami na najlepszy ruch
        //musisz wprowadzic dla kogo jest liczone
        //1-dla gracza
        //0-dla AI
        public Tuple<int, int> GetBestPlaceFor(int player)
        {
            Dictionary<Tuple<int, int>, int> RankingOfChoices = new Dictionary<Tuple<int, int>, int>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
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

                        // Liczenie wartosci po skosie
                        if (CountDiagonal(i, j)) // sprawdzamy czy punkt lezy na przekątnych
                        {
                            // Otrzymujemy punkty do sprawdzenia zajęcia przez gracza
                            List<Tuple<int, int>> points = getDiagonals(i, j);
                            foreach (Tuple<int, int> point in points)
                            {
                                // Jeżeli zajete przez gracza dodajemy punkty
                                if (board[point.Item1, point.Item2] == player) value += 10;
                            }
                        }

                        Tuple<int, int> place = new Tuple<int, int>(i, j);
                        RankingOfChoices.Add(place, value);
                    }
                }
            }
            var sortedDict = RankingOfChoices.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            RankingOfChoices = sortedDict;
            if (RankingOfChoices.Keys.Count == 0)
            {
                throw new SystemException("Nie znalazłem najlepszego ruchu, wyjebałem się");
            }
            else
            {
                return RankingOfChoices.Keys.First();
            }
        }

        public bool CountDiagonal(int x, int y)
        {
            // Jezeli nielegalne indeksy wysyp program
            if (AreIndicesLegal(x, y) == false)
            {
                throw new System.ArgumentException("Indices illegal");
            }

            // Dodajemy do listy wszystkie miejsca gdzie przekątna sie liczy
            List<Tuple<int, int>> xxx = new List<Tuple<int, int>>();
            xxx.Add(new Tuple<int, int>(0, 0));
            xxx.Add(new Tuple<int, int>(1, 1));
            xxx.Add(new Tuple<int, int>(2, 2));
            xxx.Add(new Tuple<int, int>(0, 2));
            xxx.Add(new Tuple<int, int>(2, 0));

            // Punkt do zbadania
            Tuple<int, int> point= new Tuple<int, int>(x, y);

            // Jeżeli na punkt na liście przekątnych punktow -> true
            if (xxx.Contains(point)) return true;

            return false; // w przeciwym wypadku poza -> false
        }

        public List<Tuple<int, int>> getDiagonals(int x, int y)
        {
            if (AreIndicesLegal(x, y) == false)
            {
                throw new System.ArgumentException("Indices illegal");
            }

            // Tworzymy listy możliwych przypadkow

            // punkt srodkowy. potrzeba obu przekatnych
            List<Tuple<int, int>> centerDiag = new List<Tuple<int, int>>();
            centerDiag.Add(new Tuple<int, int>(0, 0));
            centerDiag.Add(new Tuple<int, int>(1, 1));
            centerDiag.Add(new Tuple<int, int>(2, 2));
            centerDiag.Add(new Tuple<int, int>(0, 2));
            centerDiag.Add(new Tuple<int, int>(2, 0));

            // przekątna top left -> bottom right
            List<Tuple<int, int>> leftDiag = new List<Tuple<int, int>>();
            leftDiag.Add(new Tuple<int, int>(0, 0));
            leftDiag.Add(new Tuple<int, int>(1, 1));
            leftDiag.Add(new Tuple<int, int>(2, 2));

            // przekatna top right -> bottom left
            List<Tuple<int, int>> rightDiag = new List<Tuple<int, int>>();
            rightDiag.Add(new Tuple<int, int>(0, 2));
            rightDiag.Add(new Tuple<int, int>(2, 2));
            rightDiag.Add(new Tuple<int, int>(2, 0));

            // Badany punkt w przestrzeni
            Tuple<int, int> examinedPoint = new Tuple<int, int>(x, y);

            if (centerDiag.Contains(examinedPoint))
            {
                centerDiag.Remove(examinedPoint);
                return centerDiag;
            }
            else if (leftDiag.Contains(examinedPoint))
            {
                leftDiag.Remove(examinedPoint);
                return leftDiag;
            }
            else if (rightDiag.Contains(examinedPoint))
            {
                rightDiag.Remove(examinedPoint);
                return rightDiag;
            }
            else
            {
                throw new System.ArgumentException("Diagonals returning error");
            }
        }

        public bool AreIndicesLegal(int row, int col)
        {
            if (row >= 0 && row < 3 && col >= 0 && col < 3) return true;

            return false;
        }
    }
}
