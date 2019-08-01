using System;
using System.Collections.Generic;
using System.Text;

namespace FoodLibrary.Models
{
    public class TempWeather
    {
        public Temperature Temperature; //{ get; set; }
        public Humidity Humidity; //{ get; set; }
    }
    public enum Temperature
    {
        Low = 0,
        Medium = 1,
        Hot = 2,
    }
    public enum Humidity
    {
        Low = 0,
        Medium = 1,
        High = 2
    }
    public static class DoubleMethods
    {
        public static Temperature ToTemperature(this Double d)
        {
            if (d < 0)
            {
                return Temperature.Low;
            }
            else if (d > 30)
            {
                return Temperature.Hot;
            }
            else
            {
                return Temperature.Medium;
            }
        }
        public static Humidity ToHumidity(this Double d)
        {
            if (d < 30)
            {
                return Humidity.Low;
            }
            else if (d < 60)
            {
                return Humidity.Medium;
            }
            else
            {
                return Humidity.High;
            }
        }
    }


    public static class IntMethods
    {
        public static int TToInt(this Double d)
        {
            if (d < 0)
            {
                return 0;
            }
            else if (d > 30)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        public static int HToInt(this Double d)
        {
            if (d < 30)
            {
                return 0;
            }
            else if (d < 60)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}
