﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asd2_lab_2
{
    public class AStar_8Queens
    {
        public static QueenState Solve(QueenState initialState, out int steps, out int generatedStates, out int maxStoredStates)
        {
            PriorityQueue<QueenState, int> openList = new PriorityQueue<QueenState, int>();
            HashSet<string> closedList = new HashSet<string>();

            openList.Enqueue(initialState, initialState.Heuristic);
            steps = 0;
            generatedStates = 0;
            maxStoredStates = 0;

            while (openList.Count > 0)
            {
                maxStoredStates = Math.Max(maxStoredStates, openList.Count);
                QueenState current = openList.Dequeue();
                steps++;

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
                        generatedStates++;
                    }
                }
            }

            return null;
        }
    }
}
