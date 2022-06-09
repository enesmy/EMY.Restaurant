using EMY.Papel.Restaurant.Core.Application.Repositories.MailListRepositories;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.MailListRepositories
{
    public class MailListReadRepository : ReadRepository<MailList>, IMailListReadRepository
    {
        public MailListReadRepository(DbContext context) : base(context)
        {
        }
    }
}
