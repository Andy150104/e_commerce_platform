using Client.Models;
using Client.Repositories;

namespace Client.Services;

public class EmailTemplateService : IEmailTemplateService
{
    private readonly IEmailTemplateRepository _emailTemplateRepository;

    public EmailTemplateService(IEmailTemplateRepository emailTemplateRepository)
    {
        _emailTemplateRepository = emailTemplateRepository;
    }

    public VwEmailTemplateOrderScreen GetEmailTemplateByOrderScreen()
    {
        return _emailTemplateRepository.GetEmailTemplateOrderScreen();
    }
}