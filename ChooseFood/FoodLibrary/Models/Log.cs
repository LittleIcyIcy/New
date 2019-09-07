using System;
using System.Collections.Generic;
using System.Text;

namespace FoodLibrary.Models
{
    public class Log
    {
        public string FoodName { get; set; }

        public List<int> WeightChangeList { get; set; }

        public List<int> WeatherList { get; set; }

        public DateTime Date { get; set; }
        public List<string> WeatherInformationList { get; set; }

        public int GetFavor()
        {
            int sum = 0;
            for (int i = 0; i < WeightChangeList.Count; i++)
            {
                sum = sum + WeightChangeList[i];
            }

            if (sum == 0)
            {
                return 0;
            }
            else if (sum > 0)
            {
                return 1;
            }

            return 0;
        }
    }
}
