using Client.Models;
using Client.Models.Helper;

namespace Client.Repositories;

public class EmailTemplateRepository : IEmailTemplateRepository
{
    private readonly AppDbContext _context;

    public EmailTemplateRepository(AppDbContext context)
    {
        _context = context;
    }

    public VwEmailTemplateOrderScreen GetEmailTemplateOrderScreen()
    {
        return _context.VwEmailTemplateOrderScreens.FirstOrDefault();
    }
}