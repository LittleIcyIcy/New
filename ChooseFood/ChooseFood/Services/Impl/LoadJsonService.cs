using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodLibrary.Models;
using FoodLibrary.Services;
using LitJson;
using Windows.Storage;

namespace ChooseFood.Services.Impl
{
    class LoadJsonService:ILoadJsonService
    {
        public async Task<List<FoodInformation>> ReadJsonAsync()
        {
            List<FoodInformation> root = new List<FoodInformation>();
            await Task.Run(async () =>
            {
                var uri = "ms-appx:///Assets/test_data.json";
                //var uri = "ms-appx:///Assets/StoreLogo.png";
                var uri1 = new Uri(uri);
                StorageFile file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(uri1);
                String text = await Windows.Storage.FileIO.ReadTextAsync(file);
                root = JsonMapper.ToObject<List<FoodInformation>>(text);
            });
            return root;
        }
    }
}
