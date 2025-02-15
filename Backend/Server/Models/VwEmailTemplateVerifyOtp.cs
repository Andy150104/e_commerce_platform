namespace Server.Models
{
    public class VwEmailTemplateVerifyOTP
    {
        public int Id { get; set; }
        public string EmailBody { get; set; }
        public string EmailTitle { get; set; }
        public string ScreenName { get; set; }
    }
}
