using ChoreLore.Data;
using ChoreLore.Models;

namespace ChoreLore.Services
{
    public class AccountStatisticService
    {
        private readonly ApplicationDbContext _db;
        private readonly string _userId;

        public AccountStatisticService(ApplicationDbContext db, string userId)
        {
            _db = db;
            _userId = userId;
        }

        public async Task<AccountStatistics?> GetAccountStatisticsAsync()
        {
            return await _db.AccountStatistics.FindAsync(_userId);
        }

        public async Task CreateNewUserStatisticEntryAsync()
        {
            var user = await _db.Users.FindAsync(_userId);
            if (user == null)
            {
                return; // User not found
            }

            var existingStats = await _db.AccountStatistics.FindAsync(_userId);
            if (existingStats != null)
            {
                return; // User statistics already exist
            }

            var newStats = new AccountStatistics
            {
                UserId = _userId,
                TotalGoldEarned = 0,
                TotalGoldSpent = 0
            };
            _db.AccountStatistics.Add(newStats);
            await _db.SaveChangesAsync();
        }
    }
}
