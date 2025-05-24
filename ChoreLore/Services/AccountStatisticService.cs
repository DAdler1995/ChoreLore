using ChoreLore.Data;
using ChoreLore.Models;

namespace ChoreLore.Services
{
    public class AccountStatisticService
    {
        private readonly ApplicationDbContext _db;

        public AccountStatisticService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<AccountStatistics?> GetAccountStatisticsAsync(string userId)
        {
            return await _db.AccountStatistics.FindAsync(userId);
        }

        public async Task CreateNewUserStatisticEntryAsync(string userId)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user == null)
            {
                return; // User not found
            }

            var existingStats = await _db.AccountStatistics.FindAsync(userId);
            if (existingStats != null)
            {
                return; // User statistics already exist
            }

            var newStats = new AccountStatistics
            {
                UserId = userId,
                TotalGoldEarned = 0,
                TotalGoldSpent = 0
            };
            _db.AccountStatistics.Add(newStats);
            await _db.SaveChangesAsync();
        }
    }
}
