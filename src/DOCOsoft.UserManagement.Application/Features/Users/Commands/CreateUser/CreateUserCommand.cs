using System;
using MediatR;

namespace DOCOsoft.UserManagement.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand: IRequest<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
