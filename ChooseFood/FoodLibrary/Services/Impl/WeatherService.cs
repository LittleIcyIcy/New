using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace FoodLibrary.Services.Impl
{
    class WeatherService:IWeatherService
    {
        private ILocationService _locationService;
        public WeatherService(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<WeatherRoot> GetWeatherAsync()
        {
            Location location = await _locationService.GetLocationAsync();

            var http = new HttpClient();

            var respone = await http.GetAsync("http://api.openweathermap.org/data/2.5/weather?lat=" + location.Lat + "&lon=" + location.Lon + "&units=metric&APPID=97875f07fa6a1e8e6148c75d60e88422");

            var result = await respone.Content.ReadAsByteArrayAsync();

            var serializer = new DataContractJsonSerializer(typeof(WeatherRoot));

            var ss = System.Text.Encoding.ASCII.GetString(result);

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(ss));

            var data = (WeatherRoot)serializer.ReadObject(ms);

            return data;
        }
    }
}
