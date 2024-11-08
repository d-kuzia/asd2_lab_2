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

        public List<(List<(int, int)> solution, int steps)> SolveBFS(int numSolutions, out int generatedStates, out int maxStoredStates)
        {
            List<(List<(int, int)>, int)> solutions = new List<(List<(int, int)>, int)>();
            Queue<List<(int, int)>> queue = new Queue<List<(int, int)>>();
            generatedStates = 0;
            maxStoredStates = 0;
            int steps = 0;

            queue.Enqueue(new List<(int, int)>());
            while (queue.Count > 0 && solutions.Count < numSolutions)
            {
                maxStoredStates = Math.Max(maxStoredStates, queue.Count);
                var currentBoard = queue.Dequeue();
                steps++;

                if (currentBoard.Count == size)
                {
                    solutions.Add((new List<(int, int)>(currentBoard), steps));
                    continue;
                }

                int row = currentBoard.Count;
                for (int col = 0; col < size; col++)
                {
                    if (IsSafe(currentBoard, row, col))
                    {
                        var newBoard = new List<(int, int)>(currentBoard) { (row, col) };
                        queue.Enqueue(newBoard);
                        generatedStates++;
                    }
                }
            }

            return solutions;
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
