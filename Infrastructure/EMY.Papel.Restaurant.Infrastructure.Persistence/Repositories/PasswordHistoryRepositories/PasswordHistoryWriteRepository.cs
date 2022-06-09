using EMY.Papel.Restaurant.Core.Application.Repositories.PasswordHistoryRepositories;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.PasswordHistoryRepositories
{
    public class PasswordHistoryWriteRepository : WriteRepository<PasswordHistory>, IPasswordHistoryWriteRepository
    {
        public PasswordHistoryWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
