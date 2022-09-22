using System;
using System.Threading;
using System.Threading.Tasks;
using DOCOsoft.UserManagement.Application.Contracts.Persistence;
using FluentValidation;

namespace DOCOsoft.UserManagement.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserRepository _repository;
        public CreateUserCommandValidator(IUserRepository repository)
        {
            _repository = repository;

            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(e => e)
                .MustAsync(EmailUnique)
                .WithMessage("User with the same email already exists");
        }

        private async Task<bool> EmailUnique(CreateUserCommand e, CancellationToken token)
        {
            return !(await _repository.IsEmailUnique(e.Email));
        }
    }
}
