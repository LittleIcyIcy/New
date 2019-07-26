using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodLibrary.Services
{
    public interface ILastTimeCommitService
    {
        Task<DateTime> ReadJsonAsync();
        void SaveJsonAsync(DateTime lastDateTime);
    }
}
