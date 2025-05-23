namespace ChoreLore.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns the first date of the week (Sunday) based on the given date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime StartOfWeek(this DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Sunday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        /// <summary>
        /// Returns the last date of the week (Saturday) based on the given date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime EndOfWeek(this DateTime date)
        {
            var startOfWeek = StartOfWeek(date);
            return startOfWeek.AddDays(6);
        }
    }
}
