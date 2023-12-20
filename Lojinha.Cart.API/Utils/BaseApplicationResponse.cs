public class BaseApplicationResponse
{
    public virtual string Title { get; set; } = "Cart API Error";

    public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
}