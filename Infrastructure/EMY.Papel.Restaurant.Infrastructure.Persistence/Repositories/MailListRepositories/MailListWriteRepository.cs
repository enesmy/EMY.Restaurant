using EMY.Papel.Restaurant.Core.Application.Repositories.MailListRepositories;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.MailListRepositories
{
    public class MailListWriteRepository : WriteRepository<MailList>, IMailListWriteRepository
    {
        public MailListWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
