using FoodLibrary.Models;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FoodLibrary.Services.Impl
{
    public class OneDriveService : IOneDriveService
    {
        private ILastTimeCommitService _lastTimeCommitService;

        private IUserFavorService _userFavorService;

        private IUserChoiceService _userChoiceService;

        private ILogService _logService;

        private IFoodFavorService _foodFavorService;
        public class OneDriveOAuthSettings
        {
            public const string ApplicationId = //"YOUR_APP_ID_HERE";
                "12679add-0fe1-45fc-ab3b-47d43f7cfb3b";

            public const string Scopes = "files.readwrite";
        }

        private const string Server = "OneDrive服务器";
        private string[] scopes = OneDriveOAuthSettings.Scopes.Split(' ');
        private IPublicClientApplication pca;
        private GraphServiceClient graphClient;
        private string _status;
        public event EventHandler StatusChanged;
        public bool flag = false;
        /// <summary>
        /// 云服务初始化
        /// </summary>
        public OneDriveService(ILastTimeCommitService lastTimeCommitService
            , IUserFavorService userFavorService, IUserChoiceService userChoiceService, ILogService logService, IFoodFavorService foodFavorService)
        {
            _userChoiceService = userChoiceService;
            _userFavorService = userFavorService;
            _lastTimeCommitService = lastTimeCommitService;
            _logService = logService;
            _foodFavorService = foodFavorService;
            var builder = PublicClientApplicationBuilder.Create(OneDriveOAuthSettings.ApplicationId);
            pca = builder.Build();
            graphClient = new GraphServiceClient(
                new DelegateAuthenticationProvider(async (requestMessage) =>
                {
                    var accounts = await pca.GetAccountsAsync();
                    var result = await pca
                        .AcquireTokenSilent(scopes, accounts.FirstOrDefault())
                        .ExecuteAsync();
                    requestMessage.Headers.Authorization =
                        new AuthenticationHeaderValue("Bearer",
                            result.AccessToken);
                }));
            
        }
        /// <summary>
        /// 登录界面
        /// </summary>
        public async void SignInAsync()
        {
            try
            {
                var interactiveRequest = pca.AcquireTokenInteractive(scopes);

                await interactiveRequest.ExecuteAsync();
            }
            catch (Exception ex)
            {

            }
            return;

        }
        /// <summary>
        /// 注销操作
        /// </summary>
        public async void SignOutAsync()
        {
            var accounts = await pca.GetAccountsAsync();
            while (accounts.Any())
            {
                await pca.RemoveAsync(accounts.First());
                accounts = await pca.GetAccountsAsync();
            }
        }
        /// <summary>
        /// 检查现在的登陆状态
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SignSituationAsync(bool flagSignIn)
        {
            string accessToken = string.Empty;
            int k;
            try
            {
                var accounts = await pca.GetAccountsAsync();
                if (accounts.Any())
                {
                    var silentAuthResult = await pca
                        .AcquireTokenSilent(scopes, accounts.FirstOrDefault())
                        .ExecuteAsync();
                    accessToken = silentAuthResult.AccessToken;
                }
                k = accounts.Count();
            }
            catch (MsalUiRequiredException)
            {
                return false;
            }
            if(k == 1 && flag ==false && flagSignIn == true)
            {
                flag = true;
                SignOutAsync();
                return false;
            }
            else if(k == 0 & flag == false && flagSignIn == true)
            {
                flag = true;
                return false;
            }
            if(k == 1)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// 保存日志
        /// </summary>
        /// <param name="LogList"></param>
        public async void SaveLogAsync()
        {
            List<Log> LogList = _logService.GetLogs();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(LogList);

            MemoryStream fileStream = new MemoryStream();
            ZipOutputStream zipStream = new ZipOutputStream(fileStream);
            zipStream.SetLevel(3);

            ZipEntry newEntry = new ZipEntry("Log.json");
            newEntry.DateTime = DateTime.Now;
            zipStream.PutNextEntry(newEntry);

            var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            StreamUtils.Copy(jsonStream, zipStream, new byte[1024]);
            jsonStream.Close();
            zipStream.CloseEntry();

            zipStream.IsStreamOwner = false;
            zipStream.Close();
            fileStream.Position = 0;
            try
            {
                await graphClient.Me.Drive.Root
                    .ItemWithPath("/Log.zip").Content.Request()
                    .PutAsync<DriveItem>(fileStream);
            }
            catch (ServiceException ex)
            {
            }
            finally
            {
                fileStream.Close();
            }

            return;
        }
        /// <summary>
        /// 加载云日志
        /// </summary>
        /// <returns></returns>
        public async Task<List<Log>> LoadLogAsync()
        {
            var rootChildren = await graphClient.Me.Drive.Root.Children.Request().GetAsync();

            if (!rootChildren.Any(p => p.Name == "Log.zip"))
            {
                return new List<Log>();
            }

            var fileStream = await graphClient.Me.Drive.Root
                .ItemWithPath("/Log.zip").Content.Request().GetAsync();

            ZipInputStream zipStream = new ZipInputStream(fileStream);
            ZipEntry zipEntry = zipStream.GetNextEntry();

            if (zipEntry == null)
            {
                return new List<Log>();
            }

            byte[] buffer = new byte[1024];
            var jsonStream = new MemoryStream();
            StreamUtils.Copy(zipStream, jsonStream, buffer);
            zipStream.Close();
            fileStream.Close();

            jsonStream.Position = 0;
            var jsonReader = new StreamReader(jsonStream);
            var favoriteList =
                JsonConvert.DeserializeObject<List<Log>>(
                    await jsonReader.ReadToEndAsync());
            jsonReader.Close();
            jsonStream.Close();

            return favoriteList;
        }
        /// <summary>
        /// 保存食物被选择的偏好
        /// </summary>
        /// <param name="FoodWeight"></param>
        public async void SaveFoodWeightAsync()
        {
            List<FoodWeightChange> FoodWeight = _foodFavorService.GetFoodWeightChanges();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(FoodWeight);

            MemoryStream fileStream = new MemoryStream();
            ZipOutputStream zipStream = new ZipOutputStream(fileStream);
            zipStream.SetLevel(3);

            ZipEntry newEntry = new ZipEntry("FoodWeightChange.json");
            newEntry.DateTime = DateTime.Now;
            zipStream.PutNextEntry(newEntry);

            var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            StreamUtils.Copy(jsonStream, zipStream, new byte[1024]);
            jsonStream.Close();
            zipStream.CloseEntry();

            zipStream.IsStreamOwner = false;
            zipStream.Close();
            fileStream.Position = 0;
            try
            {
                await graphClient.Me.Drive.Root
                    .ItemWithPath("/FoodWeightChange.zip").Content.Request()
                    .PutAsync<DriveItem>(fileStream);
            }
            catch (ServiceException ex)
            {
            }
            finally
            {
                fileStream.Close();
            }

            return;
        }
        /// <summary>
        /// 加载食物被选择的偏好
        /// </summary>
        /// <returns></returns>
        public async Task<List<FoodWeightChange>> LoadFoodWeightAsync()
        {
            var rootChildren = await graphClient.Me.Drive.Root.Children.Request().GetAsync();

            if (!rootChildren.Any(p => p.Name == "FoodWeightChange.zip"))
            {
                return new List<FoodWeightChange>();
            }

            var fileStream = await graphClient.Me.Drive.Root
                .ItemWithPath("/FoodWeightChange.zip").Content.Request().GetAsync();

            ZipInputStream zipStream = new ZipInputStream(fileStream);
            ZipEntry zipEntry = zipStream.GetNextEntry();

            if (zipEntry == null)
            {
                return new List<FoodWeightChange>();
            }

            byte[] buffer = new byte[1024];
            var jsonStream = new MemoryStream();
            StreamUtils.Copy(zipStream, jsonStream, buffer);
            zipStream.Close();
            fileStream.Close();

            jsonStream.Position = 0;
            var jsonReader = new StreamReader(jsonStream);
            var favoriteList =
                JsonConvert.DeserializeObject<List<FoodWeightChange>>(
                    await jsonReader.ReadToEndAsync());
            jsonReader.Close();
            jsonStream.Close();

            return favoriteList;
        }

    }
}