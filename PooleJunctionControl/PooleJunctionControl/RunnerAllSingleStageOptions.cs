using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParamincsSNMPcontrol
{
    class RunnerAllSingleStageOptions
    {
        FixedVariables FV = new FixedVariables();

        SingleStageInitialGenerator SSIG = new SingleStageInitialGenerator();
        Initialising_Genome IG = new Initialising_Genome();
        Performance Perf = new Performance();
        Queue_Lengths Queue = new Queue_Lengths();
        FinalFunction FF = new FinalFunction();
        Mutate MU = new Mutate();

        int NumberOfStages = FixedVariables.NumberOfStages;
        int NumberOfTimeSteps = FixedVariables.MaxCycleTime;
        int MaxCycleTime = FixedVariables.MaxCycleTime;
        int MinimumGreen = FixedVariables.MinimumGreenTime;
        int MaxGreenTime = FixedVariables.MaximumGreenTime;
        int IntergreenTime = FixedVariables.IntergreenTime;
        int IntergreenStageNumber = FixedVariables.IntergreenStageNumber;
        int MaxTimeSinceReleased = FixedVariables.MaxTimeSinceReleased;
        double MaxTimeSinceReleasedWeighting = FixedVariables.MaxTimeSinceReleasedWeighting;

        int[] IntergreenAndLength = new int[2]; //This is the Intergreen Number followed by the Stage Length

        List<double[]> ListOfStages = new List<double[]>();

        public int CyclePlanLength(List<int[]> CyclePlan)
        {
            int Returner = 0;
            foreach (int[] item in CyclePlan)
            {
                Returner += item[1];            //This combines the stage length and the intergrenn length together (ie. total cycle plan length
            }
            return Returner;
        }

        public double WeightingTimeSinceReleased(int SelectedStage, double[] TimeSinceReleased, List<int[]> PhaseList, List<double[]> CurrentRoadState)
        {
            double Returner = 0.0;
            bool CurrentQueue = false;

            int[] ActivePhases = PhaseList[SelectedStage - 1];
            double TempMaxTime = 0.0;
            foreach (int Phase in ActivePhases)
            {
                if (TimeSinceReleased[Phase - 1] > TempMaxTime)
                {
                    TempMaxTime = TimeSinceReleased[Phase - 1];
                }
                if (CurrentRoadState[Phase - 1][0] > 0)                 //This checks if there is any demand for the phase
                {
                    CurrentQueue = true;
                }
            }
            if (TempMaxTime >= MaxTimeSinceReleased && CurrentQueue == true)   //This says that if there is any demand for the phase and it exceeds the time limit then it will be allowed to have the weighting
            {
                Returner = MaxTimeSinceReleasedWeighting;
            }
            else
            {
                Returner = 1.0;             //This weighting will always be one if the time elapsed has not exceeded the threshold so that the bid is normally calculated.
            }

            return Returner;
        }

        private void UpdateDischargeRates8Stage(List<double[]> CurrentRoadState, int StageNumber)
        {
            if (StageNumber == 1 || StageNumber == 2 || StageNumber == 3 || StageNumber == 4 || StageNumber == 7)
            {
                for (int i = 0; i < 12; i++)
                {
                    if (i == 1 || i == 4 || i == 7 || i == 10)      //If straight flow
                    {
                        CurrentRoadState[i][2] = FixedVariables.StraightFlow;
                    }
                    else
                    {
                        CurrentRoadState[i][2] = FixedVariables.UnopposedFlow;
                    }
                }
            }
            if (StageNumber == 5 || StageNumber == 6)
            {
                for (int i = 0; i < 12; i++)
                {
                    if (i == 1 || i == 4 || i == 7 || i == 10)      //If straight flow
                    {
                        CurrentRoadState[i][2] = FixedVariables.StraightFlow;
                    }
                    else if (i == 9)
                    {
                        CurrentRoadState[i][2] = FixedVariables.OpposedFlow;
                    }
                    else
                    {
                        CurrentRoadState[i][2] = FixedVariables.UnopposedFlow;
                    }
                }
            }
            if (StageNumber == 8)
            {
                for (int i = 0; i < 12; i++)
                {
                    if (i == 1 || i == 4 || i == 7 || i == 10)      //If straight flow
                    {
                        CurrentRoadState[i][2] = FixedVariables.StraightFlow;
                    }
                    else if (i == 0)
                    {
                        CurrentRoadState[i][2] = FixedVariables.OpposedFlow;
                    }
                    else
                    {
                        CurrentRoadState[i][2] = FixedVariables.UnopposedFlow;
                    }
                }
            }
        }


        /// <summary>
        /// This function will return a single stage followed by an intergreen time. The performance measure is "Lowest Delay divided by entire cycle length".
        /// This is because if we return least delay for a variable length single stage then we are not making a fair comparison as one answer could be 20 seconds
        /// long and another answer would be 12 seconds long - so the least delay would not make sense. But "delay / cycle time" is a fair comparison.
        /// </summary>
        /// <param name="StartingSeeds"></param>
        /// <param name="StepsClimbed"></param>
        /// <param name="MutationsAroundAPoint"></param>
        /// <param name="CurrentRoadState"></param>
        /// <param name="PreviousStageNumber"></param>
        /// <returns></returns>
        public List<int[]> RunAlgorithm(List<double[]> CurrentRoadState, int PreviousStageNumber, List<int[]> PhaseList, double[] TimeSinceReleased)
        {
            List<int[]> BestCyclePlan = new List<int[]>();
            double LeastDelay = 9999999999;
            double LeastDelayPerSecond = 9999999999;

            IntergreenAndLength[0] = IntergreenStageNumber;
            IntergreenAndLength[1] = IntergreenTime;
            
            for (int StageNumber = 1; StageNumber < NumberOfStages + 1; StageNumber++)
			{
                if (StageNumber != PreviousStageNumber)
	            {
                    //UpdateDischargeRates8Stage(CurrentRoadState, StageNumber);      //Note this only works for the 8 stage solution!!
                    for (int StageLength = MinimumGreen; StageLength < MaxGreenTime + 1; StageLength++)
			        {
                        if (StageNumber == 3 && StageLength > FixedVariables.MaxGreenTimeForStage3)
                        {
                            break;
                        }

                        if (StageNumber == 4 && StageLength > FixedVariables.MaxGreenTimeForStage4)
                        {
                            break;
                        }

                        if (StageNumber == 2 && StageLength > FixedVariables.MaxGreenTimeForStage2)
                        {
                            break;
                        }
                        if ((StageNumber == 5 || StageNumber == 6) && StageLength > FixedVariables.MaxGreenTimeForStage3)
                        {
                            break;
                        }
                        /*if (false)
                        {
                            
                        }*/
                        else
                        {
                            List<int[]> CyclePlan = new List<int[]>();
                            CyclePlan.Clear();

                            int[] StageAndLength = new int[2];      //This is the Stage Number followed by the Stage Length
                            StageAndLength[0] = StageNumber;
                            StageAndLength[1] = StageLength;

                            CyclePlan.Add(StageAndLength);
                            CyclePlan.Add(IntergreenAndLength);

                            int InitialCyclePlanLength = 0;
                            InitialCyclePlanLength = CyclePlanLength(CyclePlan);

                            double InitialDelay = FF.RunnerFunction(CyclePlan, LeastDelay, CurrentRoadState, PhaseList);

                            double WeightingFactor = WeightingTimeSinceReleased(StageNumber, TimeSinceReleased, PhaseList, CurrentRoadState);       //This ensures that the delay is adjusted if the stage has not been released for a long time

                            double InitialDelayPerSecond = (InitialDelay * WeightingFactor) / InitialCyclePlanLength;

                            if (InitialDelayPerSecond < LeastDelayPerSecond)                                          //This just checks to see if the initial seed is the best cycle plan
                            {
                                LeastDelay = InitialDelay;
                                BestCyclePlan = CyclePlan;
                                LeastDelayPerSecond = InitialDelayPerSecond;
                            }
                        }

			        }
                }
			}
            return BestCyclePlan;
        }



    }
}
