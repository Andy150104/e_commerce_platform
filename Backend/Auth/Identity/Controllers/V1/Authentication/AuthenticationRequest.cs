namespace Auth.Identity.Controllers;

public class AuthenticationRequest
{
    /// <summary>
    ///  UserName
    /// </summary>
    public string UserNameOrEmail { get; set; }
    
    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; }
}