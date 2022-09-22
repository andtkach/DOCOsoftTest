using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DOCOsoft.UserManagement.Application.Contracts.Persistence;
using DOCOsoft.UserManagement.Application.Exceptions;
using DOCOsoft.UserManagement.Domain.Entities;
using MediatR;

namespace DOCOsoft.UserManagement.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IAsyncRepository<User> _repository;
        private readonly IMapper _mapper;
        
        public DeleteUserCommandHandler(IMapper mapper, IAsyncRepository<User> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userToDelete = await _repository.GetByIdAsync(request.UserId);
            if (userToDelete == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }
            await _repository.DeleteAsync(userToDelete);
            return Unit.Value;
        }
    }
}
