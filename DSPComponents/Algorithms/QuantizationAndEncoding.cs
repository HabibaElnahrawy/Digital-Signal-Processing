﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class QuantizationAndEncoding : Algorithm
    {
        public int InputLevel { get; set; }
        public int InputNumBits { get; set; }
        public Signal InputSignal { get; set; }
        public Signal OutputQuantizedSignal { get; set; }
        public List<int> OutputIntervalIndices { get; set; }
        public List<string> OutputEncodedSignal { get; set; }
        public List<float> OutputSamplesError { get; set; }
        public override void Run()
        {
            OutputEncodedSignal = new List<string>();
            OutputIntervalIndices = new List<int>();
            OutputSamplesError = new List<float>();
            List<float> start = new List<float>();
            List<float> end = new List<float>();
            List<float> Mid_Point = new List<float>();
            List<float> Quantized_Signal = new List<float>();
            float min = InputSignal.Samples.Min();
            float V = min;
            int Counter = 0;
            float max = InputSignal.Samples.Max();
            if (InputLevel == 0)
            {
                double Signal_Bits = Convert.ToDouble(InputNumBits);
                InputLevel = Convert.ToInt32(Math.Pow(2, Signal_Bits));
            }
            float Delta = (max - min) / InputLevel;
            if (InputNumBits == 0)
            {
                double Signal_Bits = Math.Log(Convert.ToDouble(InputLevel), 2);
                InputNumBits = Convert.ToInt32(Signal_Bits);

            }
            while (V < max)
            {
                start.Add(V);
                V += Delta;
                end.Add(V);
                Mid_Point.Add((start[Counter] + end[Counter]) / 2);
                Counter++;
            }
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                for (int j = 0; j < InputLevel; j++)
                {
                    if (InputSignal.Samples[i] >= start[j] && InputSignal.Samples[i] < end[j] + 0.0001)
                    {
                        OutputIntervalIndices.Add(j + 1);
                        Quantized_Signal.Add((float)Math.Round((Decimal)Mid_Point[j], 3, MidpointRounding.AwayFromZero));
                        OutputSamplesError.Add(Mid_Point[j] - InputSignal.Samples[i]);
                    }
                }
            }
            for (int i = 0; i < OutputIntervalIndices.Count; i++)
            {
                string Output_Signal = Convert.ToString(OutputIntervalIndices[i] - 1, 2);
                while (Output_Signal.Length < InputNumBits)
                {
                    Output_Signal = Output_Signal.Insert(0, "0");
                }
                OutputEncodedSignal.Add(Output_Signal);
            }
            OutputQuantizedSignal = new Signal(Quantized_Signal, false);
        }
    }
}
