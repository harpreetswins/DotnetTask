using Domain.Commands.Task;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Validators.Commands.Task
{
    public class CompleteTaskForMemberCommandValidator:AbstractValidator<CompleteTaskForMemberCommand>
    {
        public CompleteTaskForMemberCommandValidator()
        {
            RuleFor(x => x.TaskId);
            RuleFor(x => x.MemberId);
        }
    }
}
