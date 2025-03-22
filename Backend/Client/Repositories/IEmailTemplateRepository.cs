using Client.Models;

namespace Client.Repositories;

public interface IEmailTemplateRepository
{
    VwEmailTemplateOrderScreen GetEmailTemplateOrderScreen();
}