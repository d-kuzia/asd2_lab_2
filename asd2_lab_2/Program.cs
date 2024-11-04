using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asd2_lab_2
{
    public class Program
    {
        private static Random random = new Random();

        public static void Main()
        {
            RunExperiments(3);
        }

        public static void RunExperiments(int numExp)
        {
            int bfsStepsSum = 0;
            int aStarStepsSum = 0;
            int bfsSuccessCount = 0;
            int aStarSuccessCount = 0;

            for (int i = 0; i < numExp; i++)
            {
                Console.WriteLine($"Experiment {i + 1}");

                // BFS
                var bfsWatch = Stopwatch.StartNew();
                int bfsSteps = 0;
                var initialBFSState = GenerateRandomInitialStateForBFS(8);
                var nQueens = new BFS_8Queens(8, initialBFSState);
                var bfsSolution = nQueens.SolveBFS();
                bfsWatch.Stop();

                if (bfsSolution != null)
                {
                    bfsSteps = bfsSolution.Count;
                    bfsStepsSum += bfsSteps;
                    bfsSuccessCount++;

                    Console.WriteLine($"BFS Solution found: {bfsSolution != null}, Time: {bfsWatch.ElapsedMilliseconds}ms, Steps: {bfsSteps}");
                    Console.WriteLine("BFS Solution Board:");
                    nQueens.PrintSolution(bfsSolution);
                }
                else
                {
                    Console.WriteLine("BFS Solution not found.");
                }

                // A*
                /*var aStarWatch = Stopwatch.StartNew();
                int aStarSteps = 0;
                var initialState = GenerateRandomInitialState();
                var aStarSolution = AStar_8Queens.Solve(initialState, out aStarSteps);
                aStarWatch.Stop();

                if (aStarSolution != null)
                {
                    aStarStepsSum += aStarSteps;
                    aStarSuccessCount++;
                    Console.WriteLine($"A* Solution found: {aStarSolution != null}, Time: {aStarWatch.ElapsedMilliseconds}ms, Steps: {aStarSteps}");
                    Console.WriteLine("A* Solution Board:");
                    aStarSolution.PrintBoard();
                }
                else
                {
                    Console.WriteLine("A* Solution not found.");
                }*/
            }

            Console.WriteLine("\nAverage Results:");
            Console.WriteLine($"BFS Average Steps: {(bfsSuccessCount > 0 ? bfsStepsSum / bfsSuccessCount : 0)}");
            Console.WriteLine($"A* Average Steps: {(aStarSuccessCount > 0 ? aStarStepsSum / aStarSuccessCount : 0)}");
        }

        public static List<(int, int)> GenerateRandomInitialStateForBFS(int size)
        {
            var queens = new List<(int, int)>();
            for (int i = 0; i < size; i++)
            {
                int row = random.Next(size);
                int col = random.Next(size);
                queens.Add((row, col));
            }
            return queens;
        }

        public static QueenState GenerateRandomInitialState()
        {
            int[] queens = new int[8];
            for (int i = 0; i < 8; i++)
            {
                queens[i] = random.Next(8);
            }
            return new QueenState(queens);
        }
    }
}
