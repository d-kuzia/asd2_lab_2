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

            Console.WriteLine("Running BFS Experiments\n");

            int bfsGeneratedStates = 0, bfsMaxStoredStates = 0;
            var nQueens = new BFS_8Queens(8);
            var bfsSolutions = nQueens.SolveBFS(numExp, out bfsGeneratedStates, out bfsMaxStoredStates);

            if (bfsSolutions.Count > 0)
            {
                foreach (var solution in bfsSolutions)
                {
                    bfsStepsSum += solution.steps;
                    bfsSuccessCount++;
                }

                bfsGeneratedStatesSum += bfsGeneratedStates;
                bfsMaxStoredStatesSum += bfsMaxStoredStates;

                for (int i = 0; i < bfsSolutions.Count; i++)
                {
                    var solution = bfsSolutions[i].solution;
                    var steps = bfsSolutions[i].steps;

                    Console.WriteLine($"\nBFS Solution {i + 1}:");
                    Console.WriteLine($"Steps: {steps}");
                    nQueens.PrintSolution(solution);
                }
            }
            else
            {
                bfsDeadEnds++;
                Console.WriteLine("BFS Solutions not found.");
            }

            Console.WriteLine("\n\nRunning A* Experiments\n");

            for (int i = 0; i < numExp; i++)
            {
                Console.WriteLine($"\nA* Solution {i + 1}:");

                int aStarSteps = 0, aStarGeneratedStates = 0, aStarMaxStoredStates = 0;
                var initialState = GenerateRandomInitialState();

                Console.WriteLine("A* Initial State:");
                initialState.PrintBoard();

                var aStarSolution = AStar_8Queens.Solve(initialState, out aStarSteps, out aStarGeneratedStates, out aStarMaxStoredStates);

                if (aStarSolution != null)
                {
                    aStarStepsSum += aStarSteps;
                    aStarSuccessCount++;
                    aStarGeneratedStatesSum += aStarGeneratedStates;
                    aStarMaxStoredStatesSum += aStarMaxStoredStates;

                    Console.WriteLine($"A* Solution found: True, Steps: {aStarSteps}");
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

            Console.WriteLine("BFS Statistics:");
            Console.WriteLine($"- Average Steps: {(bfsSuccessCount > 0 ? bfsStepsSum / bfsSuccessCount : 0)}");
            Console.WriteLine($"- Dead Ends: {bfsDeadEnds}");
            Console.WriteLine($"- Average Generated States: {(bfsSuccessCount > 0 ? bfsGeneratedStatesSum / bfsSuccessCount : 0)}");
            Console.WriteLine($"- Average Max Stored States: {(bfsSuccessCount > 0 ? bfsMaxStoredStatesSum / bfsSuccessCount : 0)}");

            Console.WriteLine("A* Statistics:");
            Console.WriteLine($"- Average Steps: {(aStarSuccessCount > 0 ? aStarStepsSum / aStarSuccessCount : 0)}");
            Console.WriteLine($"- Dead Ends: {aStarDeadEnds}");
            Console.WriteLine($"- Average Generated States: {(aStarSuccessCount > 0 ? aStarGeneratedStatesSum / aStarSuccessCount : 0)}");
            Console.WriteLine($"- Average Max Stored States: {(aStarSuccessCount > 0 ? aStarMaxStoredStatesSum / aStarSuccessCount : 0)}");
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
