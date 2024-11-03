using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asd2_lab_2
{
    public class AStar_8Queens
    {
        public static QueenState Solve()
        {
            PriorityQueue<QueenState, int> openList = new PriorityQueue<QueenState, int>();
            HashSet<string> closedList = new HashSet<string>();

            QueenState initialState = new QueenState(new int[8]);
            openList.Enqueue(initialState, initialState.Heuristic);

            while (openList.Count > 0)
            {
                QueenState current = openList.Dequeue();
                if (current.IsGoal())
                {
                    return current;
                }

                closedList.Add(string.Join(",", current.Queens));

                foreach (var next in current.GenerateNextStates())
                {
                    if (!closedList.Contains(string.Join(",", next.Queens)))
                    {
                        openList.Enqueue(next, next.Heuristic);
                    }
                }
            }

            return null;
        }
    }
}
