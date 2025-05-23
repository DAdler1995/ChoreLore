using ChoreLore.Data;
using ChoreLore.Models;
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

        public async Task<List<Chore>> GetUserChoresAsync(string userId)
        {
            return await _db.Chores.Where(x => x.UserId == userId && x.IsActive).ToListAsync();
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

            user.TotalXP += chore.XP;
            user.Level = CalculateLevel(user.TotalXP);

            var completion = new ChoreCompletion
            {
                ChoreId = choreId,
                CompletionDate = DateTime.UtcNow
            };

            chore.IsActive = false;

            _db.ChoreCompletions.Add(completion);
            await _db.SaveChangesAsync();
        }

        public int CalculateLevel(int totalXp)
        {
            return (int)Math.Floor(Math.Sqrt(totalXp) * 0.5);
        }
    }
}
