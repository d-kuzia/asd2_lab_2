using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace asd2_lab_2
{
    public class QueenState
    {
        public int[] Queens { get; set; }
        public int Heuristic { get; set; }

        public QueenState(int[] queens)
        {
            Queens = queens;
            Heuristic = CalculateHeuristic();
        }

        public int CalculateHeuristic()
        {
            int conflicts = 0;
            for (int i = 0; i < Queens.Length; i++)
            {
                for (int j = i + 1; j < Queens.Length; j++)
                {
                    if (Queens[i] == Queens[j] || Math.Abs(Queens[i] - Queens[j]) == Math.Abs(i - j))
                    {
                        conflicts++;
                    }
                }
            }
            return conflicts;
        }

        public bool IsGoal() => Heuristic == 0;

        public List<QueenState> GenerateNextStates()
        {
            var nextStates = new List<QueenState>();
            for (int i = 0; i < Queens.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Queens[i] != j)
                    {
                        int[] newQueens = (int[])Queens.Clone();
                        newQueens[i] = j;
                        nextStates.Add(new QueenState(newQueens));
                    }
                }
            }
            return nextStates;
        }

        public void PrintBoard()
        {
            for (int i = 0; i < Queens.Length; i++)
            {
                for (int j = 0; j < Queens.Length; j++)
                {
                    if (Queens[i] == j)
                        Console.Write("Q ");
                    else
                        Console.Write(". ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
