using System;
using System.Collections.Generic;
using System.Text;

namespace FoodLibrary.Models
{
    public class FoodWeightChange
    {
        public string FoodName { get; set; }
        public List<int> weightChangeList { get; set; }
    }
}
