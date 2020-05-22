﻿using System.Collections.Generic;

namespace TravelingSalesmanProblem.Models
{
    public class Graph
    {
        public const int Infinity = 1000000;
        public const int I = Infinity;
        
        public List<List<int>> Edges { get; } = new List<List<int>>
        {
            // new List<int>{+I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I, +I},
            // new List<int>{+0, +1, +2, +3, +4, +5, +6, +7, +8, +9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26},

            new List<int>{+I, +5, +5, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +I},    // 0
            new List<int>{+5, +I, +I, +5, +5, +5, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +I},    // 1
            new List<int>{+5, +I, +I, +1, +I, +I, +5, +5, +5, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +I},    // 2
            new List<int>{+I, +5, +1, +I, +I, +I, +I, +I, +I, +5, +5, +5, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I},    // 3
            new List<int>{+I, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +I, +5, +5, +5, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I},    // 4
            new List<int>{+I, +5, +I, +I, +I, +I, +1, +I, +I, +1, +1, +I, +I, +I, +I, +5, +5, +5, +I, +5, +I, +I, +I, +I, +I, +I, +I},    // 5
            new List<int>{+I, +I, +5, +I, +I, +1, +I, +I, +I, +1, +I, +5, +I, +I, +I, +I, +I, +I, +5, +5, +5, +I, +I, +I, +I, +I, +I},    // 6
            new List<int>{+I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +5, +5, +5, +I, +I, +I},    // 7
            new List<int>{+I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +1, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +5, +5, +5},    // 8
            new List<int>{+5, +5, +5, +5, +I, +1, +1, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I},    // 9
            new List<int>{+I, +I, +I, +5, +5, +5, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I},    // 10
            new List<int>{+I, +I, +I, +5, +I, +I, +5, +5, +5, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I},    // 11
            new List<int>{+I, +I, +I, +I, +5, +I, +I, +I, +1, +5, +5, +5, +I, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I},    // 12
            new List<int>{+I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +5, +I, +5, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I},    // 13
            new List<int>{+I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +I, +5, +5, +5, +1, +I, +I, +I, +5, +I, +I, +I, +I},    // 14
            new List<int>{+I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +5, +5, +5, +I, +I, +5, +I, +I, +I},    // 15
            new List<int>{+I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +5, +5, +5, +I, +I, +I},    // 16
            new List<int>{+I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +1, +1, +I, +5, +5, +5, +5},    // 17
            new List<int>{+5, +5, +5, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +1, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I},    // 18
            new List<int>{+I, +I, +I, +5, +5, +5, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I},    // 19
            new List<int>{+I, +I, +I, +I, +I, +I, +5, +5, +5, +I, +I, +I, +I, +I, +I, +5, +I, +1, +I, +I, +I, +1, +I, +I, +5, +I, +I},    // 20
            new List<int>{+I, +I, +I, +I, +I, +I, +I, +5, +I, +5, +5, +5, +I, +I, +I, +I, +5, +1, +I, +I, +1, +I, +I, +I, +I, +5, +I},    // 21
            new List<int>{+I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +5, +5, +5, +I, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +I},    // 22
            new List<int>{+I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +5, +5, +5, +I, +I, +I, +I, +I, +I, +1, +5, +I},    // 23
            new List<int>{+I, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +5, +5, +5, +I, +I, +1, +I, +I, +5},    // 24
            new List<int>{+I, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +5, +5, +5, +I, +I, +5},    // 25
            new List<int>{+I, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +I, +I, +5, +I, +I, +I, +I, +I, +I, +5, +5, +I},    // 26
        };
    }
}