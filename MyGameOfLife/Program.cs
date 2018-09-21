using System;
using System.Linq;

namespace MyGameOfLife
{
    class Program
    {
        static int x = 40;
        static int y = 40;

        static bool[][] matrix = new bool[x][];

        static void Main(string[] args)
        {
            InitializeMatrix(matrix);

            SetStartupScenario();

            Print(matrix);

            while(true)
            {
                Console.ReadLine();

                RunAlgoritm();
                Print(matrix);
            }
        }

        static void InitializeMatrix(bool[][] m)
        {
            for (int i = 0; i < x; i++)
            {
                m[i] = new bool[y];

                for (int j = 0; j < y; j++)
                {
                    m[i][j] = false;
                }
            }
        }

        static void SetStartupScenario()
        {
            matrix[2][3] = true;
            matrix[2][4] = true;
            matrix[3][2] = true;
            matrix[3][5] = true;
            matrix[4][3] = true;
            matrix[4][4] = true;

            matrix[3][6] = true;
        }

        static void RunAlgoritm()
        {
            bool[][] next = new bool[x][];
            InitializeMatrix(next);

            for (int row = 0; row < matrix.Length; row++)
            {
                for (int column = 0; column < matrix[row].Length; column++)
                {
                    bool a = false, b = false, c = false;
                    bool d = false, e = false, f = false;
                    bool g = false, h = false, i = false;

                     /*
                          x x x
                        y
                        y
                        y
                     */

                    if (column != 0 && row != 0)
                        a = matrix[row - 1][column - 1];

                    if (row != 0)
                        b = matrix[row - 1][column];

                    if (column + 1 != x && row != 0)
                        c = matrix[row - 1][column + 1];

                    if (column != 0)
                        d = matrix[row][column - 1];

                    e = matrix[row][column];

                    if (column + 1 != x)
                        f = matrix[row][column + 1];

                    if (row + 1 != y && column - 1 >= 0)
                        g = matrix[row + 1][column - 1];

                    if (row + 1 != y)
                        h = matrix[row + 1][column];

                    if (row + 1 != y && column + 1 != x)
                        i = matrix[row + 1][column + 1];

                    /*
                        Any live cell with fewer than two live neighbors dies, as if by under population.
                        Any live cell with two or three live neighbors lives on to the next generation.
                        Any live cell with more than three live neighbors dies, as if by overpopulation.
                        Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.    
                    */

                    var neighbours = new[] { a, b, c, d, f, g, h, i };
                    var liveNeighbours = neighbours.Count(n => n);

                    if (e)
                    {
                        if (liveNeighbours < 2)
                            e = false;
                        else if (liveNeighbours == 2 || liveNeighbours == 3)
                            e = true;
                        else if (liveNeighbours > 3)
                            e = false;
                    }
                    else
                    {
                        if (liveNeighbours == 3)
                            e = true;
                    }

                    next[row][column] = e;
                }
            }

            matrix = next;
        }

        static void Print(bool[][] matrix)
        {
            Console.Clear();

            for (int rowIndex = 0; rowIndex < matrix.Length; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < matrix[rowIndex].Length; columnIndex++)
                {
                    var cell = matrix[rowIndex][columnIndex];
                    Console.Write(cell ? "0" : "-");
                }

                Console.Write(Environment.NewLine);
            }
        }
    }
}
