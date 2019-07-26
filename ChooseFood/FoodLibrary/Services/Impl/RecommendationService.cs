using FoodLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodLibrary.Services.Impl
{
    public class RecommendationService: IRecommendationService
    {
        private IWeatherService _weatherService;
        private ILoadJsonService _loadJsonService;
        private IUserFavorService _userFavorService;
        private IUserChoiceService _userChoiceService;
        private ILogService _logService;
        private IFoodFavorService _foodFavorService;
        private List<FoodWeightChange> userFavorInformationList;
        private List<FoodInformation> foodInformationList;
        private TempWeather weatherStatus;

        public RecommendationService(IWeatherService weatherService, ILoadJsonService loadJsonService
            , IUserFavorService userFavorService,ILogService logService, IFoodFavorService foodFavorService)
        {
            _weatherService = weatherService;
            _loadJsonService = loadJsonService;
            _userFavorService = userFavorService;
            _logService = logService;
            _logService.InitAsync();
            _foodFavorService = foodFavorService;
            _foodFavorService.InitAsync();
        }


        /// <summary>
        /// 初始化信息
        /// </summary>
        public async void InitRecommendationAsync()
        {
            userFavorInformationList = new List<FoodWeightChange>();
            foodInformationList = await _loadJsonService.ReadJsonAsync();
            userFavorInformationList = await _userFavorService.ReadJsonAsync();
            InitWeight(foodInformationList, userFavorInformationList);
            WeatherRoot data = await _weatherService.GetWeatherAsync();
            weatherStatus = new TempWeather();
            weatherStatus.Temperature = double.Parse(data.main.temp).ToTemperature();
            weatherStatus.Humidity = double.Parse(data.main.humidity).ToHumidity();
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
                userFavorInformationList = new List<FoodWeightChange>();
                foodInformationList = await _loadJsonService.ReadJsonAsync();
                userFavorInformationList = await _userFavorService.ReadJsonAsync();
                //userChoiceList = await _userChoiceService.ReadJsonAsync();
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
        public void InitWeight(List<FoodInformation> food_infs, List<FoodWeightChange> userfavor)
        {
            for (int i = 0; i < userfavor.Count; i++)
            {
                String food_name = userfavor[i].FoodName;
                ChangeWeight(food_name, userfavor[i].weightChangeList, false);
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

            int pos = FindNameIndex(food_name);
            for (int i = 0; i < reason.Count; i++)
            {
                if(reason[i] > 0 )
                {
                    foodInformationList[pos].Weight[i] =
                        Convert.ToInt32(Math.Ceiling(Convert.ToDouble(foodInformationList[pos].Weight[i] * (4 / 3)^reason[i])));
                }
                else if(reason[i] < 0)
                {
                    foodInformationList[pos].Weight[i] = 
                        Convert.ToInt32(Math.Ceiling(Convert.ToDouble(foodInformationList[pos].Weight[reason[i]] * (3 / 4)^(Math.Abs(reason[i])))));

                }
                
            }

            if (IsWriteToJson)
            {
                Log log = new Log();
                log.FoodName = food_name;
                log.Date = DateTime.Now;
                log.WeatherList = new List<int>(2);
                log.WeatherList[0] = Int32.Parse(weatherStatus.Temperature.ToString());
                log.WeatherList[1] = Int32.Parse(weatherStatus.Humidity.ToString());
                log.WeightChangeList = reason;
                _logService.AddLog(log);
                _foodFavorService.ChangeWeight(pos,reason);

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
    }
}
