using System;
using MediatR;

namespace DOCOsoft.UserManagement.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommand: IRequest
    {
        public Guid UserId { get; set; }
    }
}
