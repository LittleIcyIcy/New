using FoodLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodLibrary.Services.Impl
{
    class RecommendationService: IRecommendationService
    {
        private IWeatherService _weatherService;
        private ILoadJsonService _loadJsonService;
        //private ISaveJsonService _saveJsonService;
        private IUserFavorService _userFavorService;
        private IUserChoiceService _userChoiceService;

        private List<UserFavorInformation> userFavorInformationList;
        private List<FoodChoice> userChoiceList;
        private List<FoodInformation> foodInformationList;

        private TempWeather weatherStatus;

        public RecommendationService(IWeatherService weatherService, ILoadJsonService loadJsonService
            , IUserChoiceService userChoiceService, IUserFavorService userFavorService)
        {
            _weatherService = weatherService;
            _loadJsonService = loadJsonService;
            //_saveJsonService = saveJsonService;
            _userFavorService = userFavorService;
            _userChoiceService = userChoiceService;
        }

        public List<FoodChoice> GetFoodChoices()
        {
            FoodChoice tempFoodChoice = new FoodChoice();
            tempFoodChoice.Date = DateTime.Today;
            tempFoodChoice.FoodName = "沙琪玛";
            userChoiceList.Add(tempFoodChoice);
            return userChoiceList;
        }

        /// <summary>
        /// 通过当前的菜品状态进行推荐。
        /// </summary>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<List<FoodInformation>> ReFlashAsync()
        {
            // 如果没录入了文件
            if (foodInformationList == null)
            {
                userFavorInformationList = new List<UserFavorInformation>();
                foodInformationList = await _loadJsonService.ReadJsonAsync();

                userFavorInformationList = await _userFavorService.ReadJsonAsync();
                userChoiceList = await _userChoiceService.ReadJsonAsync();
                InitWeight(foodInformationList, userFavorInformationList);
            }

            //获得食物的种数
            //int len = foodInformationList.Capacity;
            if (weatherStatus == null)
            {
                WeatherRoot data = await _weatherService.GetWeatherAsync();

                weatherStatus = new TempWeather();
                weatherStatus.Temperature = double.Parse(data.main.temp).ToTemperature();
                weatherStatus.Humidity = double.Parse(data.main.humidity).ToHumidity();
            }

            //获取随机的食物
            int changeWeightPosition = (3 * (int)weatherStatus.Temperature) + (int)weatherStatus.Humidity;

            List<Vector> foodVector = getFoodVector((int)weatherStatus.Temperature, (int)weatherStatus.Humidity);

            List<int> cos = GetCos(foodVector);

            int numberFoodToReturn = 5;

            int[] arr = new int[numberFoodToReturn]; // change
            int numOfGet = 0;
            while (numOfGet < numberFoodToReturn)
            {
                int getFoodNum = GetOneFoodNum(cos);
                //该食物已经获取
                if (Array.IndexOf(arr, getFoodNum) != -1)
                {
                    continue;
                }
                arr.SetValue(getFoodNum, numOfGet++);
            }

            List<FoodInformation> get_foodInformation = new List<FoodInformation>();
            for (int i = 0; i < numberFoodToReturn; i++)
            {
                get_foodInformation.Add(foodInformationList[arr[i]]);
            }

            return get_foodInformation;
        }

        public List<int> GetCos(List<Vector> foodVector)
        {
            List<int> cos = new List<int>();
            for (int i = 0; i < foodVector.Count; i++)
            {
                if (foodVector[i].Temperature == 0 && foodVector[i].Humidity == 0)
                {
                    cos.Add(-1);
                    continue;
                }
                int a = foodVector[i].Temperature;
                int b = foodVector[i].Humidity;
                int up = a + b;
                double low = Math.Sqrt(Convert.ToDouble(a * a) + (Convert.ToDouble(b * b)) + 2);
                double doubleCos = up / low;
                doubleCos = doubleCos * Math.Sqrt(Convert.ToDouble(a * a) + (Convert.ToDouble(b * b)));
                double ans = Math.Round(doubleCos * 100, 0);
                int intCos = Convert.ToInt32(ans);
                cos.Add(intCos);
            }
            return cos;
        }

        public List<Vector> getFoodVector(int temperature, int humidity)
        {
            List<Vector> foodVector = new List<Vector>();
            for (int i = 0; i < foodInformationList.Count; i++)
            {
                Vector vector = new Vector();
                vector.Temperature = foodInformationList[i].Weight[temperature];
                vector.Humidity = foodInformationList[i].Weight[3 + humidity];
                foodVector.Add(vector);
            }
            return foodVector;
        }

        /// <summary>
        /// 初始化每个事物的权值
        /// </summary>
        /// <param name="food_infs"></param>
        /// <param name="userfavor"></param>
        public void InitWeight(List<FoodInformation> food_infs, List<UserFavorInformation> userfavor)
        {
            for (int i = 0; i < userfavor.Count; i++)
            {
                String food_name = userfavor[i].Name;
                ChangeWeight(food_name, userfavor[i].reason, false);
            }
            return;
        }

        /// <summary>
        /// 根据权重获得一个随机的菜品编号
        /// </summary>
        /// <param name="statue"></param>
        /// <returns></returns>
        public int GetOneFoodNum(List<int> cos)
        {

            int sum = 0;
            int[] arr = new int[foodInformationList.Count];
            for (int i = 0; i < foodInformationList.Count; i++)
            {
                if (cos[i] == -1)
                {
                    arr[i] = 0;
                }
                else
                {
                    arr[i] = cos[i];
                }
                sum += arr[i];
            }
            int number_rand = Rand(sum);

            int sum_temp = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                sum_temp += arr[i];
                if (number_rand <= sum_temp)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 在0-sum之间获得一个数字
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private int Rand(int n)
        {
            Random rd = new Random();
            return rd.Next(0, n);
        }

        /// <summary>
        /// 返回所有当前的food_inf
        /// </summary>
        /// <returns></returns>
        public List<FoodInformation> GetFoodInfs() => foodInformationList;

        /// <summary>
        /// 修改权值
        /// </summary>
        /// <param name="food_name"></param>
        /// <param name="reason"></param>
        public void ChangeWeight(String food_name, List<int> reason, bool IsWriteToJson)
        {
            // reason 0 冷
            // reason 1 一般
            // reason 2 热
            // reason 3 干
            // reason 4 正常
            // reason 5 温
            // reason 6 没有原因
            // reason 7 喜欢
            int pos = FindNameIndex(food_name);
            if (reason[7] == 1)
            {
                for (int i = 0; i < foodInformationList[pos].Weight.Count - 2; i++)
                {
                    if (reason[i] == 1)
                    {
                        foodInformationList[pos].Weight[i] = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(foodInformationList[pos].Weight[i] * 4 / 3)));
                    }

                }
            }
            else if (reason[6] == 1)
            {
                for (int i = 1; i < foodInformationList[pos].Weight.Count - 2; i++)
                {
                    if (reason[i] == 1)
                    {
                        foodInformationList[pos].Weight[i] = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(foodInformationList[pos].Weight[reason[i]] * 4 / 3)));
                    }
                }
            }
            if (IsWriteToJson)
            {
                UserFavorInformation favor = new UserFavorInformation();
                favor.Name = food_name;
                favor.reason = reason;
                AddUserFavor(favor);
            }
        }
        /// <summary>
        /// 查找某一个食物在list当中的位置
        /// </summary>
        /// <param name="food_name"></param>
        /// <returns></returns>
        private int FindNameIndex(String food_name)
        {
            string name = food_name;
            int j = foodInformationList.Count;
            for (int i = 0; i < foodInformationList.Count; i++)
            {
                if (foodInformationList[i].Name.Equals(food_name))
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// 添加一条用户的倾向信息
        /// </summary>
        /// <param name="userfavor"></param>
        public void AddUserFavor(UserFavorInformation userfavor)
        {
            userFavorInformationList.Add(userfavor);
            return;
        }
        /// <summary>
        /// 添加一条用户的选择食物信息
        /// </summary>
        /// <param name="userchoice"></param>
        public void AddUserChoice(String userchoice)
        {
            FoodChoice foodChoice = new FoodChoice();
            foodChoice.FoodName = userchoice;
            foodChoice.Date = DateTime.Now;
            userChoiceList.Add(foodChoice);
            return;
        }
        /// <summary>
        /// 保存用户的倾向信息
        /// </summary>
        public void SaveUserFavor()
        {
            _userFavorService.SaveJsonAsync(userFavorInformationList);
            return;
        }
        /// <summary>
        /// 保存用户选择过的信息
        /// </summary>
        public void SaveUserChoice()
        {
            _userChoiceService.SaveJsonAsync(userChoiceList);
            return;
        }
    }
}
