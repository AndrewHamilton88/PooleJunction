﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParamincsSNMPcontrol
{
    class FixedVariables
    {
        public int StartingSeeds = 10;
        public int StepsClimbed = 50;
        public int MutationsAroundAPoint = 50;
        
        public static int NumberOfStages = 8;
        public static int NumberOfPhases = 12;
        public static int MinimumGreenTime = 7;
        public static int IntergreenTime = 5;
        public static int MaximumGreenTime = 40;
        public static int IntergreenStageNumber = 99;
        public static int MaxCycleTime = 120;
        public int IntergreenTimeVariable = 5;
        public static int MaxTimeSinceReleased = 120;
        public static double MaxTimeSinceReleasedWeighting = 0.75;
        public static double DistanceFromJunction = 500;
        public static int MaxGreenTimeForStage3 = 25;
        public static int MaxGreenTimeForStage4 = 25;
        public static int MaxGreenTimeForStage2 = 45;
        public static double ArrivalRateValue = 4.0;
        public static double MaxNumberOfVehiclesAtJunction = 1000;

        public static double UnopposedFlow = 0.458;
        public static double StraightFlow = 0.528;
        public static double OpposedFlow = 0.033;

        public static double MinNumberOfVehiclesAtJunction = 40;
        public static int MaxGreenTimeLowFlow = 20;
        public static int MaxGreenTimeNormalFlow = 40;

        //Four Stage Model
        public double[] Stage1 = { 2, 2, 3};  //double [] will contain [0] = queue length, [1] = arrival rate, [2] = discharge rate
        public double[] Stage2 = { 3, 2, 3};
        public double[] Stage3 = { 5, 3, 4};
        public double[] Stage4 = { 8, 1, 3};

        //4 Stage Active Phases for Poole
        public int[] Stage1Phases = { 1, 2, 3, 9 };     //These are the active phases when the corresponding stage is called
        public int[] Stage2Phases = { 6, 7, 8, 9 };  //I've included phase '9' even though it's not signallised but it does release this phase when this stage is called
        public int[] Stage3Phases = { 4, 5, 6 };
        public int[] Stage4Phases = { 10, 11, 12 };

        //12 Phase Model
        public double[] Phase1 = { 0.66667, 0.66667, 1 };  //double [] will contain [0] = queue length, [1] = arrival rate, [2] = discharge rate
        public double[] Phase2 = { 0.66667, 0.66667, 1 };
        public double[] Phase3 = { 0.66667, 0.66667, 1 };
        public double[] Phase4 = { 1, 0.66667, 1 };
        public double[] Phase5 = { 1, 0.66667, 1 };
        public double[] Phase6 = { 1, 0.66667, 1 };
        public double[] Phase7 = { 1.66667, 1, 1.33333 };
        public double[] Phase8 = { 1.66667, 1, 1.33333 };
        public double[] Phase9 = { 1.66667, 1, 1.33333 };
        public double[] Phase10 = { 2.66667, 0.33333, 1 };
        public double[] Phase11 = { 2.66667, 0.33333, 1 };
        public double[] Phase12 = { 2.66667, 0.33333, 1 };


        //8 Stage Model for Poole - Re-arranged to match 4 stage solution with extra options on 22/05/14
        public int[] Stage1Phases8Stage = { 1, 2, 3, 9 };     //These are the active phases when the corresponding stage is called
        public int[] Stage2Phases8Stage = { 6, 7, 8, 9 };
        public int[] Stage3Phases8Stage = { 4, 5, 6 };
        public int[] Stage4Phases8Stage = { 10, 11, 12 };
        public int[] Stage5Phases8Stage = { 5, 6, 10, 11, 12 };
        public int[] Stage6Phases8Stage = { 6, 9, 10, 11, 12 };
        public int[] Stage7Phases8Stage = { 4, 6, 9 };
        public int[] Stage8Phases8Stage = { 1, 2, 3, 8, 9 };


        //17 Stage Model
        /*public int[] Stage1Phases17Stage = { 1, 2, 3, 12 };     //These are the active phases when the corresponding stage is called
        public int[] Stage2Phases17Stage = { 3, 4, 5, 6 };
        public int[] Stage3Phases17Stage = { 6, 7, 8, 9 };
        public int[] Stage4Phases17Stage = { 9, 10, 11, 12 };
        public int[] Stage5Phases17Stage = { 1, 3, 6, 12 };
        public int[] Stage6Phases17Stage = { 3, 4, 6, 9 };
        public int[] Stage7Phases17Stage = { 3, 9, 10, 12 };
        public int[] Stage8Phases17Stage = { 6, 7, 9, 12 };
        public int[] Stage9Phases17Stage = { 1, 6, 7, 12 };
        public int[] Stage10Phases17Stage = { 3, 4, 9, 10 };
        public int[] Stage11Phases17Stage = { 2, 3, 8, 9 };
        public int[] Stage12Phases17Stage = { 5, 6, 11, 12 };
        public int[] Stage13Phases17Stage = { 2, 3, 9, 12 };
        public int[] Stage14Phases17Stage = { 3, 5, 6, 12 };
        public int[] Stage15Phases17Stage = { 3, 6, 8, 9 };
        public int[] Stage16Phases17Stage = { 6, 9, 11, 12 };
        public int[] Stage17Phases17Stage = { 3, 6, 9, 12 };*/


        //2 Stage Model
        public int[] Stage1Phases2Stage = { 1, 2, 3, 7, 8, 9 };     //These are the active phases when the corresponding stage is called
        public int[] Stage2Phases2Stage = { 4, 5, 6, 10, 11, 12 };


        //Time Since Phases were Released Variable
        public double TimeSincePhase1 = 0;
        public double TimeSincePhase2 = 0;
        public double TimeSincePhase3 = 0;
        public double TimeSincePhase4 = 0;
        public double TimeSincePhase5 = 0;
        public double TimeSincePhase6 = 0;
        public double TimeSincePhase7 = 0;
        public double TimeSincePhase8 = 0;
        public double TimeSincePhase9 = 0;
        public double TimeSincePhase10 = 0;
        public double TimeSincePhase11 = 0;
        public double TimeSincePhase12 = 0;

    }
}
