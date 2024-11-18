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
            Console.WriteLine("Running BFS Experiments\n");
            var nQueens = new BFS_8Queens(8);
            var bfsSolutions = nQueens.SolveBFS(numExp);

            if (bfsSolutions.Count > 0)
            {
                double bfsAvgIterations = 0, bfsAvgDeadEnds = 0,
                       bfsAvgTotalNodes = 0, bfsAvgNodesInMemory = 0;

                for (int i = 0; i < bfsSolutions.Count; i++)
                {
                    var solution = bfsSolutions[i].solution;
                    var metrics = bfsSolutions[i].metrics;

                    bfsAvgIterations += metrics.Iterations;
                    bfsAvgDeadEnds += metrics.DeadEnds;
                    bfsAvgTotalNodes += metrics.TotalNodes;
                    bfsAvgNodesInMemory += metrics.NodesInMemory;

                    Console.WriteLine($"\nBFS Solution {i + 1}:");
                    /*Console.WriteLine($"Iterations: {metrics.Iterations}");
                    Console.WriteLine($"Dead Ends: {metrics.DeadEnds}");
                    Console.WriteLine($"Total Nodes: {metrics.TotalNodes}");
                    Console.WriteLine($"Nodes in Memory: {metrics.NodesInMemory}");*/
                    nQueens.PrintSolution(solution);
                }

                // Виводимо середні значення для BFS
                int count = bfsSolutions.Count;
                /*Console.WriteLine("\nBFS Average Statistics:");
                Console.WriteLine($"Average Iterations: {bfsAvgIterations / count:F2}");
                Console.WriteLine($"Average Dead Ends: {bfsAvgDeadEnds / count:F2}");
                Console.WriteLine($"Average Total Nodes: {bfsAvgTotalNodes / count:F2}");
                Console.WriteLine($"Average Nodes in Memory: {bfsAvgNodesInMemory / count:F2}");*/
            }
            else
            {
                Console.WriteLine("BFS Solutions not found.");
            }

            Console.WriteLine("\nRunning A* Experiments\n");
            double aStarAvgIterations = 0, aStarAvgDeadEnds = 0,
                   aStarAvgTotalNodes = 0, aStarAvgNodesInMemory = 0;
            int aStarSuccessCount = 0;

            for (int i = 0; i < numExp; i++)
            {
                Console.WriteLine($"\nA* Solution {i + 1}:");

                var initialState = GenerateRandomInitialState();
                Console.WriteLine("Initial State:");
                initialState.PrintBoard();

                Metrics metrics;
                var aStarSolution = AStar_8Queens.Solve(initialState, out metrics);

                if (aStarSolution != null)
                {
                    aStarSuccessCount++;
                    aStarAvgIterations += metrics.Iterations;
                    aStarAvgDeadEnds += metrics.DeadEnds;
                    aStarAvgTotalNodes += metrics.TotalNodes;
                    aStarAvgNodesInMemory += metrics.NodesInMemory;

                    Console.WriteLine("Solution found:");
                    /*Console.WriteLine($"Iterations: {metrics.Iterations}");
                    Console.WriteLine($"Dead Ends: {metrics.DeadEnds}");
                    Console.WriteLine($"Total Nodes: {metrics.TotalNodes}");
                    Console.WriteLine($"Nodes in Memory: {metrics.NodesInMemory}");*/
                    aStarSolution.PrintBoard();
                }
                else
                {
                    Console.WriteLine("Solution not found.");
                }
            }

            if (aStarSuccessCount > 0)
            {
                /*Console.WriteLine("\nA* Average Statistics:");
                Console.WriteLine($"Average Iterations: {aStarAvgIterations / aStarSuccessCount:F2}");
                Console.WriteLine($"Average Dead Ends: {aStarAvgDeadEnds / aStarSuccessCount:F2}");
                Console.WriteLine($"Average Total Nodes: {aStarAvgTotalNodes / aStarSuccessCount:F2}");
                Console.WriteLine($"Average Nodes in Memory: {aStarAvgNodesInMemory / aStarSuccessCount:F2}");*/
            }
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
