namespace BaseApp.ViewModels;
public class CallerInfo
{
    public string? LzUserId { get; set; }   
    public string? UserName { get; set; }
    public string? Email { get; set; }  
    public string? Asset { get; set; }
    public string? Table { get; set; }
    public string? SessionId { get; set; }   
    public List<string>? Permissions { get; set; }
}
