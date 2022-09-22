using System.Linq;
using System.Threading.Tasks;
using DOCOsoft.UserManagement.Application.Contracts.Persistence;
using DOCOsoft.UserManagement.Domain.Entities;

namespace DOCOsoft.UserManagement.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DocoSoftDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> IsEmailUnique(string email)
        {
            return Task.FromResult(_dbContext.Users.Any(e => e.Email.Equals(email)));
        }
    }
}
