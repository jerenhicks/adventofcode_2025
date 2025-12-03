using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


    public class BatteryBank
    {
        private List<int> batteries;
        public int largestCapacity = 0;

        public BatteryBank(List<int> batteries)
        {
            this.batteries = batteries;
        }

        public void calculateLargestCapacity()
        {
            int largestTensNumber = 0;
            int totalCount = 0;
            List<int> combinations = new List<int>();
            for (int i = 0; i < batteries.Count; i++)
            {
                for (int j = i + 1; j < batteries.Count; j++)
                {
                    combinations.Add((batteries[i] * 10) + batteries[j]);
                }

            }

            int largestNumber = combinations.Max();
            largestCapacity =  largestNumber;
        }
        
    }
