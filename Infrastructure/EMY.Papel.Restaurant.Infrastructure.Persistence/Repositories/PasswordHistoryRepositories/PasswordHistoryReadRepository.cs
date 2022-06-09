using EMY.Papel.Restaurant.Core.Application.Repositories.PasswordHistoryRepositories;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.PasswordHistoryRepositories
{
    public class PasswordHistoryReadRepository : ReadRepository<PasswordHistory>, IPasswordHistoryReadRepository
    {
        public PasswordHistoryReadRepository(DbContext context) : base(context)
        {
        }
    }
}
