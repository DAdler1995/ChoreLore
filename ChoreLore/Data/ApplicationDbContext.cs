using ChoreLore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChoreLore.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Chore> Chores { get; set; }
        public DbSet<ChoreCompletion> ChoreCompletions { get; set; }
        public DbSet<AccountStatistics> AccountStatistics { get; set; }
    }
}
