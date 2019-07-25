using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using FoodLibrary.Services;

namespace ChooseFood.Services.Impl
{
    class LocationService:ILocationService
    {
        public async Task<Location> GetLocationAsync()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            if (accessStatus != GeolocationAccessStatus.Allowed) throw new Exception();

            var geolocator = new Geolocator { DesiredAccuracyInMeters = 0 };
            var position = await geolocator.GetGeopositionAsync();

            return new Location { Lat = position.Coordinate.Latitude, Lon = position.Coordinate.Longitude };
        }
    }
}
