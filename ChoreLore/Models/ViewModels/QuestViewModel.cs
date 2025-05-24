namespace ChoreLore.Models.ViewModels
{
    public class QuestViewModel
    {
        public Chore Quest { get; set; }
        public int CompletionCount { get; set; } // Count of completions in the current week
        public bool Completed
        {
            get
            {
                return CompletionCount >= Quest.TimesAWeek;
            }
        }
    }
}
