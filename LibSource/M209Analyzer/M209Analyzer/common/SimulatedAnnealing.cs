﻿using M209AnalyzerLib.M209;
using System;

namespace M209AnalyzerLib.Common
{
    public class SimulatedAnnealing
    {
        public static SimulatedAnnealingParameters SAParameters { get; set; }
        public SimulatedAnnealing(SimulatedAnnealingParameters saParameters)
        {
            SAParameters = saParameters;
        }
        public static bool AcceptHexaScore(long newScore, long currLocalScore, int multiplier)
        {

            return Accept(newScore, currLocalScore, 13.75 * multiplier);

        }

        public static bool Accept(double newScore, double currLocalScore, double temperature)
        {

            double diffScore = newScore - currLocalScore;
            if (diffScore > 0)
            {
                return true;
            }
            if (temperature == 0.0)
            {
                return false;
            }
            double ratio = diffScore / temperature;
            return ratio > SAParameters.MinRatio && Math.Pow(Math.E, ratio) > Utils.RandomNextFloat();
            //return ratio > minRatio && Math.Pow(Math.E, ratio) > Utils.random.NextFloat();
        }
    }
}