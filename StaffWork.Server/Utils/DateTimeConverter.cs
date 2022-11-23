namespace StaffWork.Server.Utils
{
    public static class DateTimeConverter
    {
        public static string? DateTimeToFormattedString(DateTime? dt)
        {
            if (dt.HasValue)
            {
                return dt.Value.ToString("yyyy-MM-ddTHH:mm:ssZ");
            }
            return null;
        }
    }
}
