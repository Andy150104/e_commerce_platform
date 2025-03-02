namespace Auth.Models;

public partial class VwEmailTemplateVerifyUser
{
    public int Id { get; set; }

    public string EmailBody { get; set; } = null!;

    public string EmailTitle { get; set; } = null!;

    public string ScreenName { get; set; } = null!;
}
