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
    class UserFavorService: IUserFavorService
    {
        /// <summary>
        /// 用于加载用户倾向的json文件
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserFavorInformation>> ReadJsonAsync()
        {
            List<UserFavorInformation> userChoiceInformation = new List<UserFavorInformation>();
            await Task.Run(async () =>
            {
                Windows.Storage.StorageFolder storageFolder =
                    Windows.Storage.ApplicationData.Current.LocalFolder;
                try
                {
                    Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync("user_favor.json");

                    String text = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);

                    userChoiceInformation = JsonMapper.ToObject<List<UserFavorInformation>>(text);
                }
                catch (Exception ex)
                {

                }

            });
            return userChoiceInformation;
        }

        /// <summary>
        /// 保存用户的倾向性json
        /// </summary>
        /// <param name="userChoiceInformation"></param>
        public async void SaveJsonAsync(List<UserFavorInformation> userChoiceInformation)
        {
            String json = JsonConvert.SerializeObject(userChoiceInformation.ToArray());
            try
            {
                Windows.Storage.StorageFolder storageFolder =
                    Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile writeFile =
                    await storageFolder.CreateFileAsync("user_favor.json",
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
