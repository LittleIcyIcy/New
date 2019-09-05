using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodLibrary.Services;
using LitJson;
using Newtonsoft.Json;

namespace ChooseFood.Services.Impl
{
    class LastTimeCommitService : ILastTimeCommitService
    {
        public async Task<DateTime> ReadJsonAsync()
        {
            DateTime dateTime = new DateTime();
            await Task.Run(async () =>
            {
                Windows.Storage.StorageFolder storageFolder =
                    Windows.Storage.ApplicationData.Current.LocalFolder;
                try
                {
                    Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync("lasttimecommit.json");

                    String text = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);

                    var dt = JsonMapper.ToObject<List<DateTime>>(text);
                    dateTime = dt[0];
                }
                catch (Exception ex)
                {

                }
            });
            return dateTime;
        }
        /// <summary>
        /// 保存用户选择过的菜品信息
        /// </summary>
        /// <param name="userChoice"></param>
        public async void SaveJsonAsync(DateTime lastDateTime)
        {
            List<DateTime> dt = new List<DateTime>();
            dt.Add(lastDateTime);
            String json = JsonConvert.SerializeObject(dt);
            try
            {
                Windows.Storage.StorageFolder storageFolder =
                    Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile writeFile =
                    await storageFolder.CreateFileAsync("lasttimecommit.json",
                        Windows.Storage.CreationCollisionOption.ReplaceExisting);
                await Windows.Storage.FileIO.WriteTextAsync(writeFile, json);
            }
            catch (Exception ex)
            {

            }
            return;
        }
    }
}
