using EMY.Papel.Restaurant.Core.Application.Repositories.PhotoRepositories;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.PhotoRepositories
{
    public class PhotoReadRepository : ReadRepository<Photo>, IPhotoReadRepository
    {
        public PhotoReadRepository(DbContext context) : base(context)
        {
        }
    }
}
