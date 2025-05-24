using ChoreLore.Data;
using ChoreLore.Models;

namespace ChoreLore.Services
{
    public class GoldManager
    {
        private readonly ApplicationDbContext _db;
        private readonly string _userId;

        public GoldManager(ApplicationDbContext db, string userId)
        {
            _db = db;
            _userId = userId;
        }   

        public async Task AddGoldAsync(int amount)
        {
            var stats = await _db.AccountStatistics.FindAsync(_userId);
            if (stats == null)
            {
                return;
            }

            var user = await _db.Users.FindAsync(_userId);
            if (user == null)
            {
                return;
            }

            stats.TotalGoldEarned += amount;
            user.TotalGold += amount;

            await _db.SaveChangesAsync();
        }

        public async Task RemoveGoldAsync(int amount)
        {
            var user = await _db.Users.FindAsync(_userId);
            if (user == null || user.TotalGold < amount)
            {
                return; // User not found or insufficient gold
            }

            user.TotalGold -= amount;
            await _db.SaveChangesAsync();
        }











        private async Task<AccountStatistics> CreateNewUserStatisticEntry()
        {
            var stats = await _db.AccountStatistics.FindAsync(_userId);
            if (stats != null)
            {
                return stats;
            }

            var newUserStats = new AccountStatistics { UserId = _userId,
                TotalGoldEarned = 0,
                TotalGoldSpent = 0
            };

            _db.AccountStatistics.Add(newUserStats);
            await _db.SaveChangesAsync();

            return newUserStats;
        }
        private async Task<AccountStatistics> GetAccountStatistics()
        {
            var stats = await _db.AccountStatistics.FindAsync(_userId);
            if (stats == null)
            {
                stats = await CreateNewUserStatisticEntry();
            }

            return stats;
        }
    }
}
