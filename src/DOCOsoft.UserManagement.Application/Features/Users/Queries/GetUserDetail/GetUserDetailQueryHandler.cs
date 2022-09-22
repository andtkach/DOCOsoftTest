using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DOCOsoft.UserManagement.Application.Contracts.Persistence;
using DOCOsoft.UserManagement.Domain.Entities;
using MediatR;

namespace DOCOsoft.UserManagement.Application.Features.Users.Queries.GetUserDetail
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailVm>
    {
        private readonly IAsyncRepository<User> _repository;
        private readonly IMapper _mapper;

        public GetUserDetailQueryHandler(IMapper mapper, IAsyncRepository<User> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<UserDetailVm> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<UserDetailVm>(user);
        }
    }
}
