using System.Collections.Generic;
using MediatR;

namespace DOCOsoft.UserManagement.Application.Features.Users.Queries.GetUsersList
{
    public class GetUsersListQuery: IRequest<List<UserListVm>>
    {

    }
}
