namespace Server.Models;

public partial class VwEmailTemplateVerifyUser
{
    public int Id { get; set; }
    public string EmailBody { get; set; }
    public string EmailTitle { get; set; }
    public string ScreenName { get; set; }
}