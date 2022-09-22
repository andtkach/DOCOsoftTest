using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DOCOsoft.UserManagement.Application.Contracts.Persistence;
using DOCOsoft.UserManagement.Domain.Entities;
using MediatR;

namespace DOCOsoft.UserManagement.Application.Features.Users.Queries.GetUsersList
{
    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, List<UserListVm>>
    {
        private readonly IAsyncRepository<User> _repository;
        private readonly IMapper _mapper;

        public GetUsersListQueryHandler(IMapper mapper, IAsyncRepository<User> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<UserListVm>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var allUsers = (await _repository.ListAllAsync()).OrderBy(x => x.FirstName);
            return _mapper.Map<List<UserListVm>>(allUsers);
        }
    }
}
