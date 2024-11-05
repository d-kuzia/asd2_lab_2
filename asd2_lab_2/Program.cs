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
            RunExperiments(20);
        }

        public static void RunExperiments(int numExp)
        {
            int bfsStepsSum = 0, aStarStepsSum = 0;
            int bfsSuccessCount = 0, aStarSuccessCount = 0;
            int bfsDeadEnds = 0, aStarDeadEnds = 0;
            int bfsGeneratedStatesSum = 0, aStarGeneratedStatesSum = 0;
            int bfsMaxStoredStatesSum = 0, aStarMaxStoredStatesSum = 0;

            for (int i = 0; i < numExp; i++)
            {
                Console.WriteLine($"Experiment {i + 1}");

                // BFS
                var bfsWatch = Stopwatch.StartNew();
                int bfsSteps = 0, bfsGeneratedStates = 0, bfsMaxStoredStates = 0;
                var nQueens = new BFS_8Queens(8);
                var bfsSolution = nQueens.SolveBFS(out bfsGeneratedStates, out bfsMaxStoredStates);
                bfsWatch.Stop();

                if (bfsSolution != null)
                {
                    bfsSteps = bfsSolution.Count;
                    bfsStepsSum += bfsSteps;
                    bfsSuccessCount++;
                    bfsGeneratedStatesSum += bfsGeneratedStates;
                    bfsMaxStoredStatesSum += bfsMaxStoredStates;

                    Console.WriteLine($"BFS Solution found: True, Time: {bfsWatch.ElapsedMilliseconds}ms, Steps: {bfsSteps}");
                    Console.WriteLine("BFS Solution Board:");
                    nQueens.PrintSolution(bfsSolution);
                }
                else
                {
                    bfsDeadEnds++;
                    Console.WriteLine("BFS Solution not found.");
                }

                // A*
                var aStarWatch = Stopwatch.StartNew();
                int aStarSteps = 0, aStarGeneratedStates = 0, aStarMaxStoredStates = 0;
                var initialState = GenerateRandomInitialState();
                var aStarSolution = AStar_8Queens.Solve(initialState, out aStarSteps, out aStarGeneratedStates, out aStarMaxStoredStates);
                aStarWatch.Stop();

                if (aStarSolution != null)
                {
                    aStarStepsSum += aStarSteps;
                    aStarSuccessCount++;
                    aStarGeneratedStatesSum += aStarGeneratedStates;
                    aStarMaxStoredStatesSum += aStarMaxStoredStates;

                    Console.WriteLine($"A* Solution found: True, Time: {aStarWatch.ElapsedMilliseconds}ms, Steps: {aStarSteps}");
                    Console.WriteLine("A* Solution Board:");
                    aStarSolution.PrintBoard();
                }
                else
                {
                    aStarDeadEnds++;
                    Console.WriteLine("A* Solution not found.");
                }
            }

            Console.WriteLine("\nAverage Results:");
            Console.WriteLine($"BFS Average Steps: {(bfsSuccessCount > 0 ? bfsStepsSum / bfsSuccessCount : 0)}");
            Console.WriteLine($"BFS Dead Ends: {bfsDeadEnds}");
            Console.WriteLine($"BFS Average Generated States: {(bfsSuccessCount > 0 ? bfsGeneratedStatesSum / bfsSuccessCount : 0)}");
            Console.WriteLine($"BFS Average Max Stored States: {(bfsSuccessCount > 0 ? bfsMaxStoredStatesSum / bfsSuccessCount : 0)}");

            Console.WriteLine($"A* Average Steps: {(aStarSuccessCount > 0 ? aStarStepsSum / aStarSuccessCount : 0)}");
            Console.WriteLine($"A* Dead Ends: {aStarDeadEnds}");
            Console.WriteLine($"A* Average Generated States: {(aStarSuccessCount > 0 ? aStarGeneratedStatesSum / aStarSuccessCount : 0)}");
            Console.WriteLine($"A* Average Max Stored States: {(aStarSuccessCount > 0 ? aStarMaxStoredStatesSum / aStarSuccessCount : 0)}");
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
