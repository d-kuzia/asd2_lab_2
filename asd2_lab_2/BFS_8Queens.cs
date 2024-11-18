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
        private HashSet<string> foundSolutions;

        public BFS_8Queens(int size)
        {
            this.size = size;
            this.foundSolutions = new HashSet<string>();
        }

        public List<(List<(int, int)> solution, Metrics metrics)> SolveBFS(int numSolutions)
        {
            List<(List<(int, int)>, Metrics)> solutions = new List<(List<(int, int)>, Metrics)>();
            Queue<List<(int, int)>> queue = new Queue<List<(int, int)>>();
            HashSet<string> visited = new HashSet<string>();
            var metrics = new Metrics();

            queue.Enqueue(new List<(int, int)>());
            metrics.TotalNodes = 1;
            metrics.NodesInMemory = 1;

            while (queue.Count > 0 && solutions.Count < numSolutions)
            {
                metrics.NodesInMemory = Math.Max(metrics.NodesInMemory, queue.Count);
                var currentBoard = queue.Dequeue();
                metrics.Iterations++;

                if (currentBoard.Count == size)
                {
                    string solutionKey = GetSolutionKey(currentBoard);
                    if (!foundSolutions.Contains(solutionKey))
                    {
                        foundSolutions.Add(solutionKey);
                        solutions.Add((
                            new List<(int, int)>(currentBoard),
                            new Metrics
                            {
                                Iterations = metrics.Iterations,
                                DeadEnds = metrics.DeadEnds,
                                TotalNodes = metrics.TotalNodes,
                                NodesInMemory = metrics.NodesInMemory
                            }
                        ));
                    }
                    continue;
                }

                bool hasValidMove = false;
                int row = currentBoard.Count;

                // Змінюємо порядок перебору стовпців на випадковий
                var columns = Enumerable.Range(0, size).ToList();
                Random rnd = new Random();
                columns = columns.OrderBy(x => rnd.Next()).ToList();

                foreach (int col in columns)
                {
                    if (IsSafe(currentBoard, row, col))
                    {
                        hasValidMove = true;
                        var newBoard = new List<(int, int)>(currentBoard) { (row, col) };
                        string boardKey = GetBoardKey(newBoard);

                        if (!visited.Contains(boardKey))
                        {
                            visited.Add(boardKey);
                            queue.Enqueue(newBoard);
                            metrics.TotalNodes++;
                        }
                    }
                }

                if (!hasValidMove)
                    metrics.DeadEnds++;
            }

            return solutions;
        }

        private string GetSolutionKey(List<(int, int)> board)
        {
            return string.Join(";", board.OrderBy(x => x.Item1).Select(p => $"{p.Item1},{p.Item2}"));
        }

        private string GetBoardKey(List<(int, int)> board)
        {
            return string.Join(";", board.Select(p => $"{p.Item1},{p.Item2}"));
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
