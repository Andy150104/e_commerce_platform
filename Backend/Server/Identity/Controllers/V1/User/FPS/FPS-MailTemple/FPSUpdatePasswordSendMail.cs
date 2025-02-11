using Microsoft.EntityFrameworkCore;
using Server.Controllers;
using server.Logics.Commons;
using Server.Models;
using Server.Models.Helper;

namespace Server.Identity.Controllers.V1.User;

/// <summary>
/// UserInsertSendMail - Send mail to verify the user registration
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
    public static bool SendMailOTPInformation(AppDbContext context, string userName, string email, string OTP, List<DetailError> detailErrors)
    {
        // Get the mail template
        var mailTemplate = context.VwEmailTemplateVerifyOTPs.AsNoTracking().FirstOrDefault(e => e.ScreenName  == "OTP");
        var mailTitle = mailTemplate.EmailTitle.Replace("${title}", mailTemplate.EmailTitle);
        
        // Replace the variables in the mail template
        var replacements = new Dictionary<string, string>
        {
            { "${OTP}", OTP },
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