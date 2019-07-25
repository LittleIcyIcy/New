using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodLibrary.Models;
using FoodLibrary.Services;
using LitJson;
using Newtonsoft.Json;

namespace ChooseFood.Services.Impl
{
    class UserChoiceService:IUserChoiceService
    {
        /// <summary>
        /// 读取user_choice文件
        /// </summary>
        /// <returns></returns>
        //what?????
        public async Task<List<FoodChoice>> ReadJsonAsync()
        {
            List<FoodChoice> user_choice = new List<FoodChoice>();
            await Task.Run(async () =>
            {
                Windows.Storage.StorageFolder storageFolder =
                    Windows.Storage.ApplicationData.Current.LocalFolder;
                try
                {
                    Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync("user_choice.json");

                    String text = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);

                    user_choice = JsonMapper.ToObject<List<FoodChoice>>(text);
                }
                catch (Exception ex)
                {

                }
            });
            return user_choice;
        }
        /// <summary>
        /// 保存用户选择过的菜品信息
        /// </summary>
        /// <param name="userChoice"></param>
        public async void SaveJsonAsync(List<FoodChoice> userChoice)
        {
            String json = JsonConvert.SerializeObject(userChoice.ToArray());
            try
            {
                Windows.Storage.StorageFolder storageFolder =
                    Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile writeFile =
                    await storageFolder.CreateFileAsync("user_choice.json",
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
