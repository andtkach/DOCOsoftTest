using System;
using System.Threading.Tasks;
using DOCOsoft.UserManagement.Domain.Entities;

namespace DOCOsoft.UserManagement.Application.Contracts.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<bool> IsEmailUnique(string email);
    }
}
