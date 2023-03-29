using System;
using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4).When(x => x.Model.Name != string.Empty);
            RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.DateOfBirth.Date).NotEmpty().LessThan(DateTime.Now.Date.AddDays(1));//.When(x => x.Model.DateOfBirth > DateTime.Now.Date)
        }
    }
}