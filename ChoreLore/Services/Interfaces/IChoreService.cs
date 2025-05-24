using ChoreLore.Models;
using ChoreLore.Models.ViewModels;

namespace ChoreLore.Services.Interfaces
{
    public interface IChoreService
    {
        Task<List<QuestViewModel>> GetUserChoresAsync(string userId);
        Task CompleteChoreAsync(int choreId, string userId);
        Task<int> GetChoreCompletionCountAsync(int choreId);
        Task<int> GetChoreCompletionLimitAsync(int choreId, string userId);
        int CalculateLevel(int totalXp);
    }
}
