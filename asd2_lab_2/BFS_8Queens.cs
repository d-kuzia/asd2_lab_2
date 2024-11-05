using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asd2_lab_2
{
    public class BFS_8Queens
    {
        private int size;

        public BFS_8Queens(int size)
        {
            this.size = size;
        }

        public List<(int, int)> SolveBFS()
        {
            Queue<List<(int, int)>> queue = new Queue<List<(int, int)>>();
            queue.Enqueue(new List<(int, int)>());

            while (queue.Count > 0)
            {
                var currentBoard = queue.Dequeue();

                if (currentBoard.Count == size)
                {
                    return currentBoard;
                }

                int row = currentBoard.Count;
                for (int col = 0; col < size; col++)
                {
                    if (IsSafe(currentBoard, row, col))
                    {
                        var newBoard = new List<(int, int)>(currentBoard)
                        {
                            (row, col)
                        };
                        queue.Enqueue(newBoard);
                    }
                }
            }

            return null;
        }

        private bool IsSafe(List<(int, int)> board, int row, int col)
        {
            foreach (var queen in board)
            {
                int queenRow = queen.Item1;
                int queenCol = queen.Item2;

                if (queenCol == col ||
                    queenRow - queenCol == row - col ||
                    queenRow + queenCol == row + col)
                {
                    return false;
                }
            }
            return true;
        }

        public void PrintSolution(List<(int, int)> solution)
        {
            if (solution == null)
            {
                Console.WriteLine("No solution found.");
                return;
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (solution.Contains((i, j)))
                        Console.Write("Q ");
                    else
                        Console.Write(". ");
                }
                Console.WriteLine();
            }
        }
    }
}
