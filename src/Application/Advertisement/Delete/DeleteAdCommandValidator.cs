using FluentValidation;

namespace Application.Advertisement.Delete;
internal class DeleteAdCommandValidator : AbstractValidator<DeleteAdCommand>
{
    public DeleteAdCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
