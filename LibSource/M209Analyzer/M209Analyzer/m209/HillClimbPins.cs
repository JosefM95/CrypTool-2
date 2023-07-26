﻿using M209AnalyzerLib.Enums;

namespace M209AnalyzerLib.M209
{
    public class HillClimbPins
    {
        private static int MAX_COUNT;
        private static int MIN_COUNT;
        public static double HillClimb(Key key, EvalType evalType, bool singleIteration, M209AttackManager attackManager, LocalState localState)
        {

            localState.BestScore = key.Eval(evalType);
            localState.BestScore = attackManager.Evaluate(evalType, key.Decryption, key.CribArray, localState);
            localState.BestPins = key.Pins.CreateCopy();

            /*final*/
            MAX_COUNT = key.Pins.MaxCount();
            /*final*/
            MIN_COUNT = key.Pins.MinCount();

            int round = 0;
            do
            {
                localState.Improved = false;

                Toggle1PinOn1Wheel(key, evalType, attackManager, localState);

                Inverse1Wheel(key, evalType, attackManager, localState);

                if (localState.Improved)
                {
                    continue;
                }

                Toggle2PinsOn1Wheel(key, evalType, attackManager, localState);

                if (localState.Improved)
                {
                    continue;
                }

                InverseWheelBitmap(key, evalType, attackManager, localState);

                round++;
            } while (localState.Improved && !singleIteration);


            key.Pins.Set(localState.BestPins);
            return localState.BestScore;

        }

        public static void Toggle1PinOn1Wheel(Key key, EvalType evalType, M209AttackManager attackManager, LocalState localState)
        {
            double newEval;
            int count;

            for (int wheel = 1; wheel <= Key.WHEELS; wheel++)
            {
                for (int pin = 0; pin < Key.WHEELS_SIZE[wheel]; pin++)
                {
                    key.Pins.Toggle(wheel, pin);
                    count = key.Pins.Count();
                    if (count <= MIN_COUNT || count >= MAX_COUNT || key.Pins.LongSeq(wheel, pin))
                    {
                        key.Pins.Toggle(wheel, pin);
                        continue;
                    }
                    key.UpdateDecryption(wheel, pin);

                    newEval = attackManager.Evaluate(evalType, key.Decryption, key.CribArray, localState);
                    if (newEval > localState.BestScore)
                    {
                        localState.BestScore = newEval;
                        key.Pins.Get(localState.BestPins);
                        localState.Improved = true;
                        attackManager.AddNewBestListEntry(localState.BestScore, key, key.Decryption);
                        //ReportManager.ReportResult(task, roundLayers, layers + 1, key, bestLocal, "HC P. x1");
                    }
                    else
                    {
                        key.Pins.Toggle(wheel, pin);
                        key.UpdateDecryption(wheel, pin);
                    }
                }

                if (localState.Improved)
                {
                    wheel--;
                    localState.Improved = false;
                }
            }
        }

        public static void Inverse1Wheel(Key key, EvalType evalType, M209AttackManager attackManager, LocalState localState)
        {
            double newEval;
            int count;

            for (int wheel = 1; wheel <= Key.WHEELS; wheel++)
            {

                key.Pins.Inverse(wheel);
                count = key.Pins.Count();
                if (count <= MIN_COUNT || count >= MAX_COUNT)
                {
                    key.Pins.Inverse(wheel);
                    continue;
                }

                newEval = attackManager.Evaluate(evalType, key.Decryption, key.CribArray, localState);
                if (newEval > localState.BestScore)
                {
                    localState.BestScore = newEval;
                    key.Pins.Get(localState.BestPins);
                    localState.Improved = true;
                    attackManager.AddNewBestListEntry(localState.BestScore, key, key.Decryption);
                    //ReportManager.ReportResult(task, roundLayers, layers + 1, key, bestLocal, "HC P. xw");
                }
                else
                {
                    key.Pins.Inverse(wheel);
                }
            }
        }

        public static void Toggle2PinsOn1Wheel(Key key, EvalType evalType, M209AttackManager attackManager, LocalState localState)
        {
            double newEval;

            for (int wheel = 1; wheel <= Key.WHEELS; wheel++)
            {
                for (int pin1 = 0; pin1 < Key.WHEELS_SIZE[wheel]; pin1++)
                {
                    for (int pin2 = pin1 + 1; pin2 < Key.WHEELS_SIZE[wheel]; pin2++)
                    {
                        if (key.Pins.Compare(wheel, pin1, pin2))
                        {
                            continue;
                        }
                        // Because the two places have a different value, we do not check the count.

                        key.Pins.Toggle(wheel, pin1, pin2);

                        if (key.Pins.LongSeq(wheel, pin1, pin2))
                        {
                            key.Pins.Toggle(wheel, pin1, pin2);
                            continue;
                        }
                        key.UpdateDecryption(wheel, pin1, pin2);

                        newEval = attackManager.Evaluate(evalType, key.Decryption, key.CribArray, localState);

                        if (newEval > localState.BestScore)
                        {
                            localState.BestScore = newEval;
                            key.Pins.Get(localState.BestPins);
                            localState.Improved = true;
                            attackManager.AddNewBestListEntry(localState.BestScore, key, key.Decryption);
                            //ReportManager.ReportResult(task, roundLayers, layers + 1, key, bestLocal, "HC P. x2");
                        }
                        else
                        {
                            key.Pins.Toggle(wheel, pin1, pin2);
                            key.UpdateDecryption(wheel, pin1, pin2);
                        }
                    }
                }
            }
        }

        public static void InverseWheelBitmap(Key key, EvalType evalType, M209AttackManager attackManager, LocalState localState)
        {
            int bestV = -1;
            double bestVscore = 0;
            for (int v = 0; v <= 63; v++)
            {
                key.Pins.InverseWheelBitmap(v);
                double score = attackManager.Evaluate(evalType, key.Decryption, key.CribArray, localState);
                if (score > bestVscore)
                {
                    bestVscore = score;
                    bestV = v;
                }
                key.Pins.InverseWheelBitmap(v);
            }
            if (bestVscore > localState.BestScore)
            {
                localState.BestScore = bestVscore;
                key.Pins.InverseWheelBitmap(bestV);
                key.Pins.Get(localState.BestPins);
                localState.Improved = true;
                attackManager.AddNewBestListEntry(localState.BestScore, key, key.Decryption);
                //ReportManager.ReportResult(attackManager.TaskNr, attackManager.RoundLayers, attackManager.Layers + 1, key, attackManager.BestLocal, "HC P. xn");
            }
        }

    }
}
