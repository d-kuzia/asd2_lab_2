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
        public static void Main()
        {
            RunExperiments(20);
        }

        public static void RunExperiments(int numExp)
        {
            int bfsStepsSum = 0;
            int aStarStepsSum = 0;

            for (int i = 0; i < numExp; i++)
            {
                Console.WriteLine($"Experiment {i + 1}");

                //BFS
                var bfsWatch = Stopwatch.StartNew();
                var bfsSolution = BFS_8Queens.Solve();
                bfsWatch.Stop();
                Console.WriteLine($"BFS Solution found: {bfsSolution != null}, Time: {bfsWatch.ElapsedMilliseconds}ms");

                //A star
                var aStarWatch = Stopwatch.StartNew();
                var aStarSolution = AStar_8Queens.Solve();
                aStarWatch.Stop();
                Console.WriteLine($"A* Solution found: {aStarSolution != null}, Time: {aStarWatch.ElapsedMilliseconds}ms");

                if (bfsSolution !=  null)
                {
                    bfsStepsSum += bfsSolution.Heuristic;
                }
                if (aStarSolution != null)
                {
                    aStarStepsSum += aStarSolution.Heuristic;
                }

                Console.WriteLine($"BFS Average Steps: {bfsStepsSum / numExp}");
                Console.WriteLine($"A* Average Steps: {aStarStepsSum / numExp}");
            }
        }
    }
}
