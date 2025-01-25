namespace server.Identity.Controllers;

public class AuthenticationRequest
{
    /// <summary>
    ///  UserName
    /// </summary>
    public string UserName { get; set; }
    
    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; }
}