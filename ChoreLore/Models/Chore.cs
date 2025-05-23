using ChoreLore.Data;

namespace ChoreLore.Models
{
    public class Chore
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string SimpleName { get; set; }
        public required int TimesAWeek { get; set; }
        public required int Gold { get; set; }
        public string Description { get; set; } = "";

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public IEnumerable<ChoreCompletion>? Completions { get; set; }
    }

    public class ChoreCompletion
    {
        public int Id { get; set; }
        public int? ChoreId { get; set; }
        public Chore? Chore { get; set; }
        public DateTime CompletionDate { get; set; } = DateTime.UtcNow;
    }
}
