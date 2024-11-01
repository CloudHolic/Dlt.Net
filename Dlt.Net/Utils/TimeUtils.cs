namespace Dlt.Net.Utils;

public static class TimeUtils
{
    public static DateTime TimestampToDateTime(long seconds, int microseconds)
    {
        var date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        return date.AddSeconds(seconds).AddMilliseconds(microseconds * 1000);
    }
}
