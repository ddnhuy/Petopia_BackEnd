using FluentValidation;

namespace Application.Users.Update;
internal sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(c => c.UserDTO.UserName).NotEmpty();
        RuleFor(c => c.UserDTO.FirstName).NotEmpty();
        RuleFor(c => c.UserDTO.LastName).NotEmpty();
        RuleFor(c => c.UserDTO.Email).NotEmpty().EmailAddress();
    }
}
