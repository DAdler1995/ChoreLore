using ChoreLore.Data;
using ChoreLore.Extensions;
using ChoreLore.Models;
using ChoreLore.Models.ViewModels;
using ChoreLore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChoreLore.Services
{
    public class ChoreService : IChoreService
    {
        private readonly ApplicationDbContext _db;

        public ChoreService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<QuestViewModel>> GetUserChoresAsync(string userId)
        {
            var firstDateOfWeek = DateTime.UtcNow.StartOfWeek();
            var lastDateOfWeek = DateTime.UtcNow.EndOfWeek();

            // Query only chores for the user, and completion count in the current week
            var chores = await _db.Chores.Include(x => x.Completions).Where(x => x.UserId == userId).ToListAsync();

            var viewModels = new List<QuestViewModel>();
            foreach (var chore in chores)
            {
                var viewModel = new QuestViewModel
                {
                    Quest = chore,
                    CompletionCount = chore.Completions != null ? chore.Completions.Count(c => c.CompletionDate >= firstDateOfWeek && c.CompletionDate <= lastDateOfWeek) : 0 // Count the completions in the current week
                };

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public async Task CompleteChoreAsync(int choreId, string userId)
        {
            var chore = await _db.Chores.FindAsync(choreId);
            if (chore == null || chore.UserId != userId)
            {
                return;
            }

            var user = await _db.Users.FindAsync(userId);
            if (user == null)
            {
                return;
            }

            var completion = new ChoreCompletion
            {
                ChoreId = choreId,
                CompletionDate = DateTime.UtcNow
            };

            user.TotalGold += chore.Gold;

            _db.ChoreCompletions.Add(completion);
            await _db.SaveChangesAsync();
        }

        public async Task<int> GetChoreCompletionCountAsync(int choreId)
        {
            var completionCount = await _db.ChoreCompletions.CountAsync(x => x.ChoreId == choreId);
            return completionCount;
        }

        public async Task<int> GetChoreCompletionLimitAsync(int choreId, string userId)
        {
            var chore = await _db.Chores.FindAsync(choreId);
            if (chore == null || chore.UserId != userId)
            {
                return 0;
            }

            var user = await _db.Users.FindAsync(userId);
            if (user == null)
            {
                return 0;
            }

            return chore.TimesAWeek;
        }

        public int CalculateLevel(int totalXp)
        {
            return (int)Math.Floor(Math.Sqrt(totalXp) * 0.5);
        }
    }
}
