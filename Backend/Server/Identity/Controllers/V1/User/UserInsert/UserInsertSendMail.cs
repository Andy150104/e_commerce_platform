using Microsoft.EntityFrameworkCore;
using Server.Controllers;
using server.Logics.Commons;
using Server.Models;
using Server.Models.Helper;

namespace Server.Identity.Controllers.V1.User;

/// <summary>
/// UserInsertSendMail - Send mail to verify the user registration
/// </summary>
public static class UserInsertSendMail
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
    public static bool SendMailVerifyInformation(AppDbContext context, string userName, string email, string key, List<DetailError> detailErrors)
    {
        // Get the mail template
        var mailTemplate = context.VwEmailTemplateVerifyUsers.AsNoTracking().FirstOrDefault();
        var mailTitle = mailTemplate.EmailTitle.Replace("${title}", mailTemplate.EmailTitle);
        
        var encodedKey = Uri.EscapeDataString(key);
        // Replace the variables in the mail template
        var replacements = new Dictionary<string, string>
        {
            { "${user_name}", userName },
            { "${email}", email },
            { "${verification_link}", encodedKey }
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