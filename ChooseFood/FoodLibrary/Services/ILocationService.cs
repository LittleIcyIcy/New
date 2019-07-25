using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodLibrary.Services
{
    public interface ILocationService
    {
        Task<Location> GetLocationAsync();
    }

    /// <summary>
    /// 位置信息类。
    /// </summary>
    public class Location
    {
        /// <summary>
        /// 
        /// </summary>
        public double Lat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Lon { get; set; }
    }
}
