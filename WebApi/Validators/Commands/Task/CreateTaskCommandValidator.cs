using Domain.Commands.Task;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Validators.Commands.Task
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.Subject).NotEmpty();
            RuleFor(x => x.AssignedToId).NotEmpty();
        }
    }
}
