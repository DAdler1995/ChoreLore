using ChoreLore.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChoreLore.Models
{
    public class AccountStatistics
    {
        [Key, ForeignKey("User")]
        public required string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int TotalGoldEarned { get; set; }
        public int TotalGoldSpent { get; set; }
    }
}
