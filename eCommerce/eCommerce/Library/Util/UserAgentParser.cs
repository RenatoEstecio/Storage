namespace Library.Util
{
    public static class UserAgentParser
    {
        public static string ParseOperatingSystem(string? userAgent)
        {
            if (string.IsNullOrWhiteSpace(userAgent))
                return "Unknown";

            if (userAgent.Contains("Windows", StringComparison.OrdinalIgnoreCase))
                return "Windows";
            if (userAgent.Contains("Android", StringComparison.OrdinalIgnoreCase))
                return "Android";
            if (userAgent.Contains("iPhone", StringComparison.OrdinalIgnoreCase) || userAgent.Contains("iPad", StringComparison.OrdinalIgnoreCase))
                return "iOS";
            if (userAgent.Contains("Mac OS", StringComparison.OrdinalIgnoreCase))
                return "macOS";
            if (userAgent.Contains("Linux", StringComparison.OrdinalIgnoreCase))
                return "Linux";

            return "Unknown";
        }
    }
}
