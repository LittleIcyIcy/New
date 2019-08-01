using System;
using System.Collections.Generic;
using System.Text;

namespace FoodLibrary.Models
{
    public class FoodInformation
    {
        public string Name { get; set; }
        public List<int> Weight { get; set; }

        public List<string> Title { get; set; }
        public string Url { get; set; }
        public string FoodElements { get; set; }
        public string FoodImage { get; set; }
    }
}
