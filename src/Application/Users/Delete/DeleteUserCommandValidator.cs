﻿using FluentValidation;

namespace Application.Users.Delete;
internal sealed class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.Password).NotEmpty();
    }
}
