using ChoreLore.Data;
using ChoreLore.Extensions;
using ChoreLore.Models;
using ChoreLore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ChoreLore.Services
{
    public class ChoreService
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
            var chore = await _db.Chores.Include(x => x.Completions).FirstOrDefaultAsync(x => x.Id == choreId);
            if (chore == null || chore.UserId != userId || chore.Completions == null)
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

            _db.ChoreCompletions.Add(completion);
            await _db.SaveChangesAsync();

            // reward gold when quest is fully completed for the week
            var firstDateOfWeek = DateTime.UtcNow.StartOfWeek();
            var lastDateOfWeek = DateTime.UtcNow.EndOfWeek();
            if (chore.Completions.Count(c => c.CompletionDate >= firstDateOfWeek && c.CompletionDate <= lastDateOfWeek) == chore.TimesAWeek)
            {
                var goldManager = new GoldManager(_db, userId);
                await goldManager.AddGoldAsync(chore.Gold);
            }
        }

        public async Task CreateChoreAsync(Chore newChore)
        {
            _db.Chores.Add(newChore);
            await _db.SaveChangesAsync();
        }
    }
}
