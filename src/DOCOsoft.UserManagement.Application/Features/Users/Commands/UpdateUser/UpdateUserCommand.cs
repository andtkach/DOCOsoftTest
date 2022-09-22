using System;
using MediatR;

namespace DOCOsoft.UserManagement.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand: IRequest
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
