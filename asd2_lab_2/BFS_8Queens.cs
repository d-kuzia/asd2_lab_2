using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asd2_lab_2
{
    public class BFS_8Queens
    {
        public static QueenState Solve()
        {
            Queue<QueenState> queue = new Queue<QueenState>();
            QueenState initialState = new QueenState(new int[8]);
            queue.Enqueue(initialState);

            while (queue.Count > 0)
            {
                QueenState current = queue.Dequeue();
                if (current.IsGoal())
                {
                    return current;
                }

                foreach (var next in current.GenerateNextStates())
                {
                    queue.Enqueue(next);
                }
            }

            return null;
        }
    }
}
