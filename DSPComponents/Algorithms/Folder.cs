﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Folder : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputFoldedSignal { get; set; }

        public override void Run()
        {
            List<float> samples = new List<float>();
            List<int> indices = new List<int>();
            int N = InputSignal.Samples.Count;
            for (int i = N - 1; i >= 0; i--)
            {
                samples.Add(InputSignal.Samples[i]);
                indices.Add(InputSignal.SamplesIndices[i] * -1);
            }

            OutputFoldedSignal = new Signal(samples, indices, !InputSignal.Periodic);
        }
    }
}
