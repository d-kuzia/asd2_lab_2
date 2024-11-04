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
        private int Size;
        private List<(int, int)> InitialState;

        public BFS_8Queens(int size, List<(int, int)> initialState)
        {
            this.Size = size;
            this.InitialState = initialState;
        }

        public List<(int, int)> SolveBFS()
        {
            var queue = new Queue<List<(int, int)>>();
            queue.Enqueue(new List<(int, int)>(InitialState));

            while (queue.Count > 0)
            {
                var solution = queue.Dequeue();

                if (solution.Count == Size)
                {
                    if (!HasConflict(solution))
                    {
                        return solution;
                    }
                    continue;
                }

                int row = solution.Count;

                for (int col = 0; col < Size; col++)
                {
                    var newSolution = new List<(int, int)>(solution) { (row, col) };

                    if (!HasConflict(newSolution))
                    {
                        queue.Enqueue(newSolution);
                    }
                }
            }

            return null;
        }

        private bool HasConflict(List<(int, int)> queens)
        {
            for (int i = 0; i < queens.Count; i++)
            {
                for (int j = i + 1; j < queens.Count; j++)
                {
                    var (row1, col1) = queens[i];
                    var (row2, col2) = queens[j];

                    if (col1 == col2 || Math.Abs(row1 - row2) == Math.Abs(col1 - col2))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void PrintSolution(List<(int, int)> solution)
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (solution.Contains((row, col)))
                        Console.Write("Q ");
                    else
                        Console.Write(". ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
