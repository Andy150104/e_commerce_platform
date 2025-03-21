using Client.Logics.Commons;
using Client.Models;
using Client.Models.Helper;
using Client.Services;

namespace Client.Controllers.V1.ThirdParty;

public class MomoOrderSendMail
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="codeGHN"></param>
    /// <returns></returns>
    public static bool SendMailGhnCode(IEmailTemplateService service, string codeGHN, string email, AppDbContext context, List<DetailError> detailErrors)
    {
        // Get the mail template
        var mailTemplate = service.GetEmailTemplateByOrderScreen();
        var mailTitle = mailTemplate.Title.Replace("${title}", mailTemplate.Title);
        
        var replacements = new Dictionary<string, string>
        {
            { "${key}", codeGHN },
        };

        // Replace the variables in the mail template
        var mailBody = mailTemplate.Body;
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