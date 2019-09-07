using FoodLibrary.Models;
using FoodLibrary.Services;
using FoodLibrary.Services.Impl;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        //测试推荐系统
        public async System.Threading.Tasks.Task Test1Async()
        {
            //weatherservice
            var weatherRootToReturn = new WeatherRoot();
            weatherRootToReturn.main = new Main();
            weatherRootToReturn.main.temp = "29";
            weatherRootToReturn.main.humidity = "30";
            var weatherServiceMock = new Mock<IWeatherService>();
            weatherServiceMock.Setup(w => w.GetWeatherAsync()).ReturnsAsync(weatherRootToReturn);
            var mockWeatherService = weatherServiceMock.Object;

            // loadjsonservice

            List<FoodInformation> fi = new List<FoodInformation>();
            for (int i = 1; i <= 40; i++)
            {
                FoodInformation food = new FoodInformation();
                food.Name = i.ToString();
                food.Weight = new List<int>();
                for (int j = 0; j < 6; j++)
                {
                    food.Weight.Add(i);
                }
                fi.Add(food);
            }

            var loadJsonServiceMock = new Mock<ILoadJsonService>();
            loadJsonServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(fi);
            var mockLoadJsonServiceMock = loadJsonServiceMock.Object;

            //userchoiceservice
            var userChoiceServiceMock = new Mock<IUserChoiceService>();
            userChoiceServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(new List<Log>());
            var mockUserChoiceService = userChoiceServiceMock.Object;

            //userfacorservice
            var userFavorServiceMock = new Mock<IUserFavorService>();
            userFavorServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(new List<FoodWeightChange>());
            var mockUserFavorService = userFavorServiceMock.Object;
            //log
            var logServiceMock = new Mock<ILogService>();
            logServiceMock.Setup(w => w.GetLogs()).Returns(new List<Log>());
            var mockLogSerivce = logServiceMock.Object;

            //foodFavorSerivce
            var foodFavorSerivce = new Mock<IFoodFavorService>();
            var mockFoodFavorService = foodFavorSerivce.Object;

            IRecommendationService ir = new RecommendationService(mockWeatherService, mockLoadJsonServiceMock,
                 mockUserFavorService, mockLogSerivce, mockFoodFavorService);
            ir.InitRecommendationAsync();
            List<FoodInformation> ff = await ir.ReFlashAsync();
            Assert.AreEqual(ff.Count, 5);

            Assert.Pass();

        }
        [Test]
        //测试权值正向修改
        public async System.Threading.Tasks.Task Test2Async()
        {
            //weatherservice
            var weatherRootToReturn = new WeatherRoot();
            weatherRootToReturn.main = new Main();
            weatherRootToReturn.main.temp = "29";
            weatherRootToReturn.main.humidity = "30";
            var weatherServiceMock = new Mock<IWeatherService>();
            weatherServiceMock.Setup(w => w.GetWeatherAsync()).ReturnsAsync(weatherRootToReturn);
            var mockWeatherService = weatherServiceMock.Object;

            // loadjsonservice

            List<FoodInformation> fi = new List<FoodInformation>();
            for (int i = 1; i <= 40; i++)
            {
                FoodInformation food = new FoodInformation();
                food.Name = i.ToString();
                food.Weight = new List<int>();
                for (int j = 0; j < 6; j++)
                {
                    food.Weight.Add(i);
                }
                fi.Add(food);
            }

            var loadJsonServiceMock = new Mock<ILoadJsonService>();
            loadJsonServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(fi);
            var mockLoadJsonServiceMock = loadJsonServiceMock.Object;

            //userchoiceservice
            var userChoiceServiceMock = new Mock<IUserChoiceService>();
            userChoiceServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(new List<Log>());
            var mockUserChoiceService = userChoiceServiceMock.Object;

            //userfacorservice
            var userFavorServiceMock = new Mock<IUserFavorService>();
            userFavorServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(new List<FoodWeightChange>());
            var mockUserFavorService = userFavorServiceMock.Object;
            //log
            var logServiceMock = new Mock<ILogService>();
            logServiceMock.Setup(w => w.GetLogs()).Returns(new List<Log>());
            var mockLogSerivce = logServiceMock.Object;

            var foodFavorSerivce = new Mock<IFoodFavorService>();
            var mockFoodFavorService = foodFavorSerivce.Object;

            IRecommendationService ir = new RecommendationService(mockWeatherService, mockLoadJsonServiceMock,
                 mockUserFavorService, mockLogSerivce, mockFoodFavorService);
            ir.InitRecommendationAsync();
            List<FoodInformation> ff = await ir.ReFlashAsync();
            List<int> reason = new List<int>();
            for (int i = 0; i <= 5; i++)
            {
                reason.Add(1);
            }
            ir.ChangeWeight("10", reason, false);
            var fd = ir.GetFoodInfs();
            Assert.AreEqual(fd[9].Weight[1], 14);
            //List<FoodInformation> ff = await ir.ReFlashAsync();
        }
        [Test]
        //测试负向修改
        public async System.Threading.Tasks.Task Test3Async()
        {
            //weatherservice
            var weatherRootToReturn = new WeatherRoot();
            weatherRootToReturn.main = new Main();
            weatherRootToReturn.main.temp = "29";
            weatherRootToReturn.main.humidity = "30";
            var weatherServiceMock = new Mock<IWeatherService>();
            weatherServiceMock.Setup(w => w.GetWeatherAsync()).ReturnsAsync(weatherRootToReturn);
            var mockWeatherService = weatherServiceMock.Object;

            // loadjsonservice

            List<FoodInformation> fi = new List<FoodInformation>();
            for (int i = 1; i <= 40; i++)
            {
                FoodInformation food = new FoodInformation();
                food.Name = i.ToString();
                food.Weight = new List<int>();
                for (int j = 0; j < 6; j++)
                {
                    food.Weight.Add(i);
                }
                fi.Add(food);
            }

            var loadJsonServiceMock = new Mock<ILoadJsonService>();
            loadJsonServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(fi);
            var mockLoadJsonServiceMock = loadJsonServiceMock.Object;

            //userchoiceservice
            var userChoiceServiceMock = new Mock<IUserChoiceService>();
            userChoiceServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(new List<Log>());
            var mockUserChoiceService = userChoiceServiceMock.Object;

            //userfacorservice
            var userFavorServiceMock = new Mock<IUserFavorService>();
            userFavorServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(new List<FoodWeightChange>());
            var mockUserFavorService = userFavorServiceMock.Object;
            //log
            var logServiceMock = new Mock<ILogService>();
            logServiceMock.Setup(w => w.GetLogs()).Returns(new List<Log>());
            var mockLogSerivce = logServiceMock.Object;

            var foodFavorSerivce = new Mock<IFoodFavorService>();
            var mockFoodFavorService = foodFavorSerivce.Object;

            IRecommendationService ir = new RecommendationService(mockWeatherService, mockLoadJsonServiceMock,
                 mockUserFavorService, mockLogSerivce, mockFoodFavorService);
            ir.InitRecommendationAsync();
            List<FoodInformation> ff = await ir.ReFlashAsync();
            List<int> reason = new List<int>();
            for (int i = 0; i <= 5; i++)
            {
                reason.Add(-2);
            }
            ir.ChangeWeight("10", reason, false);
            var fd = ir.GetFoodInfs();
            Assert.AreEqual(fd[9].Weight[1], 6);
            //List<FoodInformation> ff = await ir.ReFlashAsync();
        }
        [Test]
        //测试天气获取
        public async System.Threading.Tasks.Task Test4Async()
        {
            Location location = new Location();
            location.Lat = 45;
            location.Lon = 45;
            var locationServiceMock = new Mock<ILocationService>();
            locationServiceMock.Setup(w => w.GetLocationAsync()).ReturnsAsync(location);
            var mockLocationService = locationServiceMock.Object;

            IWeatherService weatherService = new WeatherService(mockLocationService);
            WeatherRoot data = await weatherService.GetWeatherAsync();
            Assert.AreEqual("RU", data.sys.country);
        }
        //测试维护系统删除操作
        [Test]
        public async System.Threading.Tasks.Task Test8Async()
        {
            List<Log> cloudLog = new List<Log>();
            
            for(int i = 0; i < 10; i++)
            {
                Log log = new Log();
                log.FoodName = i.ToString();
                log.Date = DateTime.Now;
                int[] w = { 1, 1 };
                log.WeatherList = new List<int>(w);
                cloudLog.Add(log);
            }
            List<Log> logs = new MaintenanceService(null, null, null, null, null, null,null).DeleteRecord(cloudLog, "1", DateTime.MinValue,4);
            Assert.AreEqual(logs.Count, 9);
        }
        //测试维护系统添加操作1
        [Test]
        public async System.Threading.Tasks.Task Test9Async()
        {
            List<Log> cloudLog = new List<Log>();
            List<Log> localLog = new List<Log>();
            for (int i = 0; i < 10; i++)
            {
                Log log = new Log();
                log.FoodName = i.ToString();
                log.Date = DateTime.Now;
                int[] w = { 1, 1 };
                cloudLog.Add(log);
                log.WeatherList = new List<int>(w);
                Log log1 = new Log();
                log.FoodName = "1";
                log.Date = DateTime.Now;
                localLog.Add(log);
            }
            List<Log> logs = new MaintenanceService(null, null, null, null, null, null, null).InsertRecord(cloudLog, localLog, "1", DateTime.MinValue,4);
            Assert.AreEqual(logs.Count, 20);
        }
        //测试维护系统添加操作2
        [Test]
        public async System.Threading.Tasks.Task Test10Async()
        {
            List<Log> cloudLog = new List<Log>();
            List<Log> localLog = new List<Log>();
            for (int i = 0; i < 10; i++)
            {
                Log log = new Log();
                log.FoodName = i.ToString();
                log.Date = DateTime.Now;
                cloudLog.Add(log);
                Log log1 = new Log();
                log.FoodName = "1";
                log.Date = DateTime.Now;
                localLog.Add(log);
            }
            List<Log> logs = new MaintenanceService(null, null, null, null, null, null, null).InsertRecord(cloudLog, localLog, "1", DateTime.MaxValue,4);
            Assert.AreEqual(logs.Count, 10);
        }
        //测试维护系统同步操作
        [Test]
        public async System.Threading.Tasks.Task Test11Async()
        {
            //weatherservice
            var weatherRootToReturn = new WeatherRoot();
            weatherRootToReturn.main = new Main();
            weatherRootToReturn.main.temp = "29";
            weatherRootToReturn.main.humidity = "30";
            var weatherServiceMock = new Mock<IWeatherService>();
            weatherServiceMock.Setup(w => w.GetWeatherAsync()).ReturnsAsync(weatherRootToReturn);
            var mockWeatherService = weatherServiceMock.Object;

            // loadjsonservice

            List<FoodInformation> fi = new List<FoodInformation>();
            for (int i = 0; i <= 40; i++)
            {
                FoodInformation food = new FoodInformation();
                food.Name = i.ToString();
                food.Weight = new List<int>();
                for (int j = 0; j < 6; j++)
                {
                    food.Weight.Add(i);
                }
                fi.Add(food);
            }

            var loadJsonServiceMock = new Mock<ILoadJsonService>();
            loadJsonServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(fi);
            var mockLoadJsonServiceMock = loadJsonServiceMock.Object;

            //userchoiceservice
            var userChoiceServiceMock = new Mock<IUserChoiceService>();
            userChoiceServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(new List<Log>());
            var mockUserChoiceService = userChoiceServiceMock.Object;

            //userfacorservice
            var userFavorServiceMock = new Mock<IUserFavorService>();
            userFavorServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(new List<FoodWeightChange>());
            var mockUserFavorService = userFavorServiceMock.Object;
            //log
            var logServiceMock = new Mock<ILogService>();
            List<Log> localLog = new List<Log>();
            for(int i = 0; i <100; i++)
            {
                Log log = new Log();
                log.FoodName = (i / 3).ToString();
                log.Date = DateTime.Now;
                int[] t = { i % 3, i % 3 };
                log.WeatherList = new List<int>(t);
                int[] t1 = { 1, 1, 1, 1, 1, 1 };
                log.WeightChangeList = new List<int>(t1);
                localLog.Add(log);

            }
            logServiceMock.Setup(w => w.GetLogs()).Returns(localLog);
            var mockLogSerivce = logServiceMock.Object;

            //foodFavorSerivce
            var foodFavorSerivce = new Mock<IFoodFavorService>();
            var mockFoodFavorService = foodFavorSerivce.Object;

            IRecommendationService ir = new RecommendationService(mockWeatherService, mockLoadJsonServiceMock,
                 mockUserFavorService, mockLogSerivce, mockFoodFavorService);
            ir.InitRecommendationAsync();
            // mockOneDriveService
            var oneDriveService = new Mock<IOneDriveService>();
            List<Log> k = new List<Log>();
            for(int i = 0; i <= 10; i++)
            {
                Log log = new Log();
                log.FoodName = i.ToString();
                log.Date = DateTime.MinValue;
                int[] t = { 0, 0 };
                log.WeatherList = new List<int>(t);
                int[] t2 = {-1,0,1,2,-2,5};
                log.WeightChangeList = new List<int>(t2);
                k.Add(log);
            }
            oneDriveService.Setup(w => w.LoadLogAsync()).ReturnsAsync(k);
            oneDriveService.Setup(w => w.LoadFoodWeightAsync()).ReturnsAsync(new List<FoodWeightChange>());
            var mockOneDriveService = oneDriveService.Object;
            //mockLastTimeCommitService
            var lastTimeCommitService = new Mock<ILastTimeCommitService>();
            DateTime dt = new DateTime(2016, 6, 1);

            lastTimeCommitService.Setup(w => w.ReadJsonAsync()).ReturnsAsync(dt);
            var mockLastTimeCommitService = lastTimeCommitService.Object;
            MaintenanceService maintenanceService = new MaintenanceService(ir, mockUserChoiceService, 
                mockUserFavorService, mockOneDriveService, mockLastTimeCommitService, mockLogSerivce, 
                mockFoodFavorService);
            await maintenanceService.MaintenanceAsync();
        }
        //测试维护同步系统同步2
        [Test]
        public async System.Threading.Tasks.Task Test12Async()
        {
            //weatherservice
            var weatherRootToReturn = new WeatherRoot();
            weatherRootToReturn.main = new Main();
            weatherRootToReturn.main.temp = "29";
            weatherRootToReturn.main.humidity = "30";
            var weatherServiceMock = new Mock<IWeatherService>();
            weatherServiceMock.Setup(w => w.GetWeatherAsync()).ReturnsAsync(weatherRootToReturn);
            var mockWeatherService = weatherServiceMock.Object;

            // loadjsonservice

            List<FoodInformation> fi = new List<FoodInformation>();
            for (int i = 0; i <= 40; i++)
            {
                FoodInformation food = new FoodInformation();
                food.Name = i.ToString();
                food.Weight = new List<int>();
                for (int j = 0; j < 6; j++)
                {
                    food.Weight.Add(i);
                }
                fi.Add(food);
            }

            var loadJsonServiceMock = new Mock<ILoadJsonService>();
            loadJsonServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(fi);
            var mockLoadJsonServiceMock = loadJsonServiceMock.Object;

            //userchoiceservice
            var userChoiceServiceMock = new Mock<IUserChoiceService>();
            userChoiceServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(new List<Log>());
            var mockUserChoiceService = userChoiceServiceMock.Object;

            //userfacorservice
            var userFavorServiceMock = new Mock<IUserFavorService>();
            userFavorServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(new List<FoodWeightChange>());
            var mockUserFavorService = userFavorServiceMock.Object;
            //log
            var logServiceMock = new Mock<ILogService>();
            List<Log> localLog = new List<Log>();
            for (int i = 0; i < 9; i++)
            {
                Log log = new Log();
                log.FoodName = i.ToString();
                log.Date = new DateTime(2016, 6, 1);
                int[] t = { 1,1};
                log.WeatherList = new List<int>(t);
                int[] t1 = { 2, 2, 2, 2, 2, 2 };
                log.WeightChangeList = new List<int>(t1);
                localLog.Add(log);
            }
            logServiceMock.Setup(w => w.GetLogs()).Returns(localLog);
            var mockLogSerivce = logServiceMock.Object;

            //foodFavorSerivce
            var foodFavorSerivce = new Mock<IFoodFavorService>();
            var mockFoodFavorService = foodFavorSerivce.Object;

            IRecommendationService ir = new RecommendationService(mockWeatherService, mockLoadJsonServiceMock,
                 mockUserFavorService, mockLogSerivce, mockFoodFavorService);
            ir.InitRecommendationAsync();
            // mockOneDriveService
            var oneDriveService = new Mock<IOneDriveService>();
            List<Log> k = new List<Log>();
            for (int i = 0; i <= 10; i++)
            {
                Log log = new Log();
                log.FoodName = i.ToString();
                log.Date = new DateTime(2016, 6, 2);
                int[] t = { 1, 1};
                log.WeatherList = new List<int>(t);
                int[] t2 = { 1, 1, 1, 1, 1, 1 };
                log.WeightChangeList = new List<int>(t2);
                k.Add(log);
            }
            oneDriveService.Setup(w => w.LoadLogAsync()).ReturnsAsync(k);
            oneDriveService.Setup(w => w.LoadFoodWeightAsync()).ReturnsAsync(new List<FoodWeightChange>());
            var mockOneDriveService = oneDriveService.Object;
            //mockLastTimeCommitService
            var lastTimeCommitService = new Mock<ILastTimeCommitService>();
            DateTime dt = new DateTime(2016, 4, 1);

            lastTimeCommitService.Setup(w => w.ReadJsonAsync()).ReturnsAsync(dt);
            var mockLastTimeCommitService = lastTimeCommitService.Object;
            MaintenanceService maintenanceService = new MaintenanceService(ir, mockUserChoiceService,
                mockUserFavorService, mockOneDriveService, mockLastTimeCommitService, mockLogSerivce,
                mockFoodFavorService);
            await maintenanceService.MaintenanceAsync();
        }
        //测试维护同步系统同步3
        [Test]
        public async System.Threading.Tasks.Task Test13Async()
        {
            //weatherservice
            var weatherRootToReturn = new WeatherRoot();
            weatherRootToReturn.main = new Main();
            weatherRootToReturn.main.temp = "29";
            weatherRootToReturn.main.humidity = "30";
            var weatherServiceMock = new Mock<IWeatherService>();
            weatherServiceMock.Setup(w => w.GetWeatherAsync()).ReturnsAsync(weatherRootToReturn);
            var mockWeatherService = weatherServiceMock.Object;

            // loadjsonservice

            List<FoodInformation> fi = new List<FoodInformation>();
            for (int i = 0; i <= 40; i++)
            {
                FoodInformation food = new FoodInformation();
                food.Name = i.ToString();
                food.Weight = new List<int>();
                for (int j = 0; j < 6; j++)
                {
                    food.Weight.Add(i);
                }
                fi.Add(food);
            }

            var loadJsonServiceMock = new Mock<ILoadJsonService>();
            loadJsonServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(fi);
            var mockLoadJsonServiceMock = loadJsonServiceMock.Object;

            //userchoiceservice
            var userChoiceServiceMock = new Mock<IUserChoiceService>();
            userChoiceServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(new List<Log>());
            var mockUserChoiceService = userChoiceServiceMock.Object;

            //userfacorservice
            var userFavorServiceMock = new Mock<IUserFavorService>();
            userFavorServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(new List<FoodWeightChange>());
            var mockUserFavorService = userFavorServiceMock.Object;
            //log
            var logServiceMock = new Mock<ILogService>();
            List<Log> localLog = new List<Log>();
            for (int i = 0; i < 9; i++)
            {
                Log log = new Log();
                log.FoodName = i.ToString();
                log.Date = new DateTime(2016, 6, 1);
                int[] t = { 1, 1 };
                log.WeatherList = new List<int>(t);
                int[] t1 = { -1, -1, 2, 2, 2, 2 };
                log.WeightChangeList = new List<int>(t1);
                localLog.Add(log);
            }
            logServiceMock.Setup(w => w.GetLogs()).Returns(localLog);
            var mockLogSerivce = logServiceMock.Object;

            //foodFavorSerivce
            var foodFavorSerivce = new Mock<IFoodFavorService>();
            var mockFoodFavorService = foodFavorSerivce.Object;

            IRecommendationService ir = new RecommendationService(mockWeatherService, mockLoadJsonServiceMock,
                 mockUserFavorService, mockLogSerivce, mockFoodFavorService);
            ir.InitRecommendationAsync();
            // mockOneDriveService
            var oneDriveService = new Mock<IOneDriveService>();
            List<Log> k = new List<Log>();
            for (int i = 0; i <= 10; i++)
            {
                Log log = new Log();
                log.FoodName = i.ToString();
                log.Date = new DateTime(2016, 6, 2);
                int[] t = { 1, 1 };
                log.WeatherList = new List<int>(t);
                int[] t2 = { 1, 1, 1, 1, 1, 1 };
                log.WeightChangeList = new List<int>(t2);
                k.Add(log);
            }
            oneDriveService.Setup(w => w.LoadLogAsync()).ReturnsAsync(k);
            oneDriveService.Setup(w => w.LoadFoodWeightAsync()).ReturnsAsync(new List<FoodWeightChange>());
            var mockOneDriveService = oneDriveService.Object;
            //mockLastTimeCommitService
            var lastTimeCommitService = new Mock<ILastTimeCommitService>();
            DateTime dt = new DateTime(2016, 4, 1);

            lastTimeCommitService.Setup(w => w.ReadJsonAsync()).ReturnsAsync(dt);
            var mockLastTimeCommitService = lastTimeCommitService.Object;
            MaintenanceService maintenanceService = new MaintenanceService(ir, mockUserChoiceService,
                mockUserFavorService, mockOneDriveService, mockLastTimeCommitService, mockLogSerivce,
                mockFoodFavorService);
            await maintenanceService.MaintenanceAsync();
        }
        //测试日志系统的读写操作
        [Test]
        public async System.Threading.Tasks.Task Test5Async()
        {
            var userChoiceServiceMock = new Mock<IUserChoiceService>();
            List<Log> log = new List<Log>();
            for(int i = 0;i < 10; i++)
            {
                Log tmpLog3 = new Log();
                tmpLog3.FoodName = i.ToString();
                int[] p3 = { i, i, i, i, i, i };
                tmpLog3.WeightChangeList = new List<int>(p3);
                log.Add(tmpLog3);
            }
            userChoiceServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(log);
            var mockUserChoiceService = userChoiceServiceMock.Object;
            LogService logService = new LogService(mockUserChoiceService);
            await logService.InitAsync();
            Assert.AreEqual(logService.GetLogs(), log);

            Log tmpLog = new Log();
            tmpLog.FoodName ="10";
            int[] p = { 10, 10, 10, 10, 10, 10 };
            tmpLog.WeightChangeList = new List<int>(p);
            log.Add(tmpLog);
            logService.AddLog(tmpLog);
            Assert.AreEqual(logService.GetLogs(), log);

            Log tmpLog1 = new Log();
            tmpLog1.FoodName = "11";
            int[] p1 = { 11, 11, 11, 11, 11, 11 };
            tmpLog.WeightChangeList = new List<int>(p1);
            log.Add(tmpLog);
            logService.SetLogs(log);
            Assert.AreEqual(logService.GetLogs(), log);

            //logService.SetLogs();
        }
        //测试计算相关度
        [Test]
        public async System.Threading.Tasks.Task Test6Async()
        {
            //weatherservice
            var weatherRootToReturn = new WeatherRoot();
            weatherRootToReturn.main = new Main();
            weatherRootToReturn.main.temp = "29";
            weatherRootToReturn.main.humidity = "30";
            var weatherServiceMock = new Mock<IWeatherService>();
            weatherServiceMock.Setup(w => w.GetWeatherAsync()).ReturnsAsync(weatherRootToReturn);
            var mockWeatherService = weatherServiceMock.Object;

            // loadjsonservice

            List<FoodInformation> fi = new List<FoodInformation>();
            for (int i = 1; i <= 40; i++)
            {
                FoodInformation food = new FoodInformation();
                food.Name = i.ToString();
                food.Weight = new List<int>();
                for (int j = 0; j < 6; j++)
                {
                    food.Weight.Add(i);
                }
                fi.Add(food);
            }

            var loadJsonServiceMock = new Mock<ILoadJsonService>();
            loadJsonServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(fi);
            var mockLoadJsonServiceMock = loadJsonServiceMock.Object;

            //userchoiceservice
            var userChoiceServiceMock = new Mock<IUserChoiceService>();
            userChoiceServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(new List<Log>());
            var mockUserChoiceService = userChoiceServiceMock.Object;

            //userfacorservice
            var userFavorServiceMock = new Mock<IUserFavorService>();
            userFavorServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(new List<FoodWeightChange>());
            var mockUserFavorService = userFavorServiceMock.Object;
            //log
            var logServiceMock = new Mock<ILogService>();
            logServiceMock.Setup(w => w.GetLogs()).Returns(new List<Log>());
            var mockLogSerivce = logServiceMock.Object;

            //foodFavorSerivce
            var foodFavorSerivce = new Mock<IFoodFavorService>();
            var mockFoodFavorService = foodFavorSerivce.Object;

            IRecommendationService ir = new RecommendationService(mockWeatherService, mockLoadJsonServiceMock,
                 mockUserFavorService, mockLogSerivce, mockFoodFavorService);
            ir.InitRecommendationAsync();
            Vector vector = new Vector();
            vector.Humidity = -1;
            vector.Temperature = 1;
            List<Vector> vectors = new List<Vector>();
            vectors.Add(vector);
            int[] k = { 0 };
            List<int> p = new List<int>(k);
            Assert.AreEqual(ir.GetCos(vectors),p);

        }
        //测试修改权重操作
        [Test]
        public async System.Threading.Tasks.Task Test7Async()
        {
            //weatherservice
            var weatherRootToReturn = new WeatherRoot();
            weatherRootToReturn.main = new Main();
            weatherRootToReturn.main.temp = "29";
            weatherRootToReturn.main.humidity = "30";
            var weatherServiceMock = new Mock<IWeatherService>();
            weatherServiceMock.Setup(w => w.GetWeatherAsync()).ReturnsAsync(weatherRootToReturn);
            var mockWeatherService = weatherServiceMock.Object;

            // loadjsonservice

            List<FoodInformation> fi = new List<FoodInformation>();
            for (int i = 1; i <= 40; i++)
            {
                FoodInformation food = new FoodInformation();
                food.Name = i.ToString();
                food.Weight = new List<int>();
                for (int j = 0; j < 6; j++)
                {
                    food.Weight.Add(i);
                }
                fi.Add(food);
            }

            var loadJsonServiceMock = new Mock<ILoadJsonService>();
            loadJsonServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(fi);
            var mockLoadJsonServiceMock = loadJsonServiceMock.Object;

            //userchoiceservice
            var userChoiceServiceMock = new Mock<IUserChoiceService>();
            userChoiceServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(new List<Log>());
            var mockUserChoiceService = userChoiceServiceMock.Object;

            //userfacorservice
            var userFavorServiceMock = new Mock<IUserFavorService>();
            userFavorServiceMock.Setup(w => w.ReadJsonAsync()).ReturnsAsync(new List<FoodWeightChange>());
            var mockUserFavorService = userFavorServiceMock.Object;
            //log
            var logServiceMock = new Mock<ILogService>();
            logServiceMock.Setup(w => w.GetLogs()).Returns(new List<Log>());
            var mockLogSerivce = logServiceMock.Object;

            //foodFavorSerivce
            var foodFavorSerivce = new Mock<IFoodFavorService>();
            var mockFoodFavorService = foodFavorSerivce.Object;

            IRecommendationService ir = new RecommendationService(mockWeatherService, mockLoadJsonServiceMock,
                 mockUserFavorService, mockLogSerivce, mockFoodFavorService);
            ir.InitRecommendationAsync();
            int[] t = { 1, 1, 1, 1, 1, 1};
            List<int> reason = new List<int>(t);
            ir.ChangeWeight("1", reason, false);
            var p = ir.GetFoodInfs();
            Assert.AreEqual(p[1].Weight[1], 2);
        }
    }
}