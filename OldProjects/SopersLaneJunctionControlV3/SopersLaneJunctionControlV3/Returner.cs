using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParamincsSNMPcontrol
{
    public class Returner
    {
        public List<int[]> CyclePlan = new List<int[]>();
        public double[] TimeSinceReleased = new double[12];
        public int[] PreviousStage = new int[1];
    }
}
