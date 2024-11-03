using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asd2_lab_2
{
    public class BFS_8Queens
    {
        public static QueenState Solve(QueenState initialState, int maxDepth, out int steps)
        {
            Queue<(QueenState, int)> queue = new Queue<(QueenState, int)>();
            queue.Enqueue((initialState, 0));
            steps = 0;

            while (queue.Count > 0)
            {
                var (current, depth) = queue.Dequeue();
                steps++;

                Console.WriteLine($"Current state: {string.Join(",", current.Queens)}, Heuristic: {current.Heuristic}, Depth: {depth}");

                if (current.IsGoal())
                {
                    Console.WriteLine("Goal found!");
                    return current;
                }

                if (depth < maxDepth)
                {
                    foreach (var next in current.GenerateNextStates())
                    {
                        queue.Enqueue((next, depth + 1));
                    }
                }
            }

            return null;
        }
    }
}
