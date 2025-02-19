using Microsoft.EntityFrameworkCore;
using Server.Controllers;
using server.Logics.Commons;
using Server.Models;
using Server.Models.Helper;

namespace Server.Identity.Controllers.V1.User;

/// <summary>
/// FPSUpdatePasswordSendMail - Send mail to verify the user  
/// </summary>
public static class FPSUpdatePasswordSendMail
{
    /// <summary>
    /// Send mail to verify the user registration 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="userName"></param>
    /// <param name="email"></param>
    /// <param name="key"></param>
    /// <param name="detailErrors"></param>
    /// <returns></returns>
    public static bool SendMailOTPInformation(AppDbContext context, string userName, string email, string key, List<DetailError> detailErrors)
    {
        // Get the mail template
        var mailTemplate = context.VwEmailTemplateVerifyOtps.AsNoTracking().FirstOrDefault(e => e.ScreenName  == "OTP");
        var mailTitle = mailTemplate.EmailTitle.Replace("${title}", mailTemplate.EmailTitle);

        // Replace the variables in the mail template
        var encodedKey = Uri.EscapeDataString(key);
        var replacements = new Dictionary<string, string>
        {
            { "${key}", $"http://localhost:3000/ForgotPass/verify?key={encodedKey}" },
        };

        // Replace the variables in the mail template
        var mailBody = mailTemplate.EmailBody;
        foreach (var replacement in replacements)
        {
            mailBody = mailBody.Replace(replacement.Key, replacement.Value);
        }
        
        // Send the mail
        var mailInfo = new EmailTemplate
        {
            Title = mailTitle,
            Body = mailBody,
        };
        return SendMailLogic.SendMail(mailInfo, email, context, detailErrors);
    }
}