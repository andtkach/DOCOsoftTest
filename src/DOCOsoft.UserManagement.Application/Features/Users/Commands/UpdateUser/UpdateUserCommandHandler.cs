using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DOCOsoft.UserManagement.Application.Contracts.Persistence;
using DOCOsoft.UserManagement.Application.Exceptions;
using DOCOsoft.UserManagement.Domain.Entities;
using MediatR;

namespace DOCOsoft.UserManagement.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IAsyncRepository<User> _repository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IMapper mapper, IAsyncRepository<User> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _repository.GetByIdAsync(request.UserId);

            if (userToUpdate == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            var validator = new UpdateUserCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            _mapper.Map(request, userToUpdate, typeof(UpdateUserCommand), typeof(User));
            await _repository.UpdateAsync(userToUpdate);
            return Unit.Value;
        }
    }
}