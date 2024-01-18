namespace Infrastructure.Settings;

public class CorsSettings
{
    public string? PolicyName { get; set; }
    public string[]? Origins { get; set; }
    public string[]? Methods { get; set; }
    public string[]? Headers { get; set; }
}