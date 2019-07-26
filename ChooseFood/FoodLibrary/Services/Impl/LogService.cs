using System;
using System.Collections.Generic;
using System.Text;
using FoodLibrary.Models;

namespace FoodLibrary.Services.Impl
{
    public class LogService: ILogService
    {
        private IUserChoiceService _userChoiceService;

        public List<Log> LogList;
        public LogService(IUserChoiceService userChoiceService)
        {
            _userChoiceService = userChoiceService;
        }

        public async System.Threading.Tasks.Task InitAsync()
        {
            LogList = await _userChoiceService.ReadJsonAsync();
        }
        public void AddLog(Log addLog)
        {
            LogList.Add(addLog);
        }

        public async void SaveLogAsync()
        {
            _userChoiceService.SaveJsonAsync(LogList);
        }
    }
}
