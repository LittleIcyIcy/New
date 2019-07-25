using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FoodLibrary.Services
{
    public interface IWeatherService
    {
        Task<WeatherRoot> GetWeatherAsync();
    }
    [DataContract]
    public class Coord
    {
        [DataMember]
        public string lon { get; set; }
        [DataMember]
        public string lat { get; set; }
    }
    [DataContract]
    public class Weather
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string main { get; set; }
        [DataMember]
        //天气状态
        public string description { get; set; }
        [DataMember]
        public string icon { get; set; }

        public static implicit operator Weather(WeatherRoot v)
        {
            throw new NotImplementedException();
        }
    }
    [DataContract]
    public class Main
    {
        [DataMember]
        //要用
        public string temp { get; set; }
        [DataMember]
        public string pressure { get; set; }
        [DataMember]
        //要用
        public string humidity { get; set; }
        [DataMember]
        public string temp_min { get; set; }
        [DataMember]
        public string temp_max { get; set; }
        [DataMember]
        public string sea_level { get; set; }
        [DataMember]
        public string grnd_level { get; set; }
    }
    [DataContract]
    public class Wind
    {
        [DataMember]
        public string speed { get; set; }
        [DataMember]
        public string deg { get; set; }
    }
    [DataContract]
    public class Clouds
    {
        [DataMember]
        public string all { get; set; }
    }
    [DataContract]
    public class Sys
    {
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public string sunrise { get; set; }
        [DataMember]
        public string sunset { get; set; }
    }
    [DataContract]
    public class WeatherRoot
    {
        [DataMember]
        public Coord coord { get; set; }
        [DataMember]
        public List<Weather> weather { get; set; }
        //public string base  { get; set; }
        [DataMember]


        public Main main { get; set; }
        [DataMember]
        public Wind wind { get; set; }
        [DataMember]
        public Clouds clouds { get; set; }
        [DataMember]
        public string dt { get; set; }
        [DataMember]
        public Sys sys { get; set; }
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string cod { get; set; }

        public static implicit operator string(WeatherRoot v)
        {
            throw new NotImplementedException();
        }
    }
}
