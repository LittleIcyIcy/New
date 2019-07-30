using FoodLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodLibrary.Services
{
    public interface ILogService
    {
        Task InitAsync();
        void AddLog(Log addLog);

        void SaveLogAsync();
        List<Log> GetLogs();

        void SetLogs(List<Log> logs);
    }
}
