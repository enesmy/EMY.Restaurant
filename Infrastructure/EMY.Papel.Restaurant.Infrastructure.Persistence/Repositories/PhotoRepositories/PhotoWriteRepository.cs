using EMY.Papel.Restaurant.Core.Application.Repositories.PhotoRepositories;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.PhotoRepositories
{
    public class PhotoWriteRepository : WriteRepository<Photo>, IPhotoWriteRepository
    {
        public PhotoWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
