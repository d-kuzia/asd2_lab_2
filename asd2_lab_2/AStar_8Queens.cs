using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asd2_lab_2
{
    public class AStar_8Queens
    {
        public static QueenState Solve(QueenState initialState, out Metrics metrics)
        {
            metrics = new Metrics();
            PriorityQueue<QueenState, int> openList = new PriorityQueue<QueenState, int>();
            HashSet<string> closedList = new HashSet<string>();

            openList.Enqueue(initialState, initialState.Heuristic);
            metrics.TotalNodes = 1;
            metrics.NodesInMemory = 1;

            while (openList.Count > 0)
            {
                metrics.NodesInMemory = Math.Max(metrics.NodesInMemory, openList.Count + closedList.Count);
                QueenState current = openList.Dequeue();
                metrics.Iterations++;

                if (current.IsGoal())
                    return current;

                closedList.Add(string.Join(",", current.Queens));

                var nextStates = current.GenerateNextStates();
                if (nextStates.Count == 0)
                    metrics.DeadEnds++;

                foreach (var next in nextStates)
                {
                    if (!closedList.Contains(string.Join(",", next.Queens)))
                    {
                        openList.Enqueue(next, next.Heuristic);
                        metrics.TotalNodes++;
                    }
                }
            }

            return null;
        }
    }
}
