using Client.Models;

namespace Client.Services;

public interface IEmailTemplateService
{
    VwEmailTemplateOrderScreen GetEmailTemplateByOrderScreen();
}