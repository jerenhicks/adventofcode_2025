using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;


    public class NeoBatteryBank
    {
        private List<Battery> batteries;
        public double largestCapacity = 0;

        public NeoBatteryBank(List<Battery> batteries)
        {
            this.batteries = batteries;
        }

        public void calculateLargestCapacity()
        {
            //go through the batteries and find the first instance of the largest capcity
            var indexOfLargest = 0;
            var maxDigits = 12;

            List<Battery> result = new List<Battery>();
            int toPick = 12;
            for (int i = 0; i < batteries.Count; i++) {
                while (result.Count > 0 &&
                       result.Count + batteries.Count - i > toPick &&
                       batteries[i].capacity > result[result.Count - 1].capacity) {
                    result.RemoveAt(result.Count - 1);
                }
                if (result.Count < toPick) {
                    result.Add(batteries[i]);
                }
            }
            largestCapacity = double.Parse(string.Join("", result.Select(b => b.capacity.ToString())));
        }
        
    }
