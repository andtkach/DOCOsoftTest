using System;

namespace DOCOsoft.UserManagement.Application.Features.Users.Queries.GetUsersList
{
    public class UserListVm
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
