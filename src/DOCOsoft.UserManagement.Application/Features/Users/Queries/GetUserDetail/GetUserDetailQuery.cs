using System;
using MediatR;

namespace DOCOsoft.UserManagement.Application.Features.Users.Queries.GetUserDetail
{
    public class GetUserDetailQuery: IRequest<UserDetailVm>
    {
        public Guid Id { get; set; }
    }
}
