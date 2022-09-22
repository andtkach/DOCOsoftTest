using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DOCOsoft.UserManagement.Application.Contracts.Persistence;
using DOCOsoft.UserManagement.Application.Exceptions;
using DOCOsoft.UserManagement.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DOCOsoft.UserManagement.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserCommandHandler> _logger;


        public CreateUserCommandHandler(IMapper mapper, IUserRepository repository, ILogger<CreateUserCommandHandler> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateUserCommandValidator(_repository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);
            var user = _mapper.Map<User>(request);
            user = await _repository.AddAsync(user);
            return user.UserId;
        }
    }
}