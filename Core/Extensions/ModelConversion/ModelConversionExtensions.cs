using Domain.Commands;
using Domain.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Domain.ClientSideModels;
using Domain.Commands.Task;
using Domain.ViewModel.Task;

namespace Core.Extensions.ModelConversion
{
    public static class ModelConversionExtensions
    {
        public static CreateMemberCommand ToCreateMemberCommand(this MemberVm model)
        {
            var command = new CreateMemberCommand()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Roles = model.Roles,
                Avatar = model.Avatar,
                Email = model.Email
            };
            return command;
        }

        public static MenuItem[] ToMenuItems(this IEnumerable<MemberVm> models)
        {
            return models.Select(m => new MenuItem()
            {
                iconColor = m.Avatar,
                isActive = false,
                label = $"{m.LastName}, {m.FirstName}",
                referenceId = m.Id
            }).ToArray();
        }

        public static UpdateMemberCommand ToUpdateMemberCommand(this MemberVm model)
        {
            var command = new UpdateMemberCommand()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Roles = model.Roles,
                Avatar = model.Avatar,
                Email = model.Email
            };
            return command;
        }


        public static CreateTaskCommand ToCreateTaskCommand(this TaskVM model)
        {
            var command = new CreateTaskCommand()
            {
                Subject = model.Subject,
                IsComplete = model.IsComplete,
                AssignedToId = model.AssignedToId
            };
            return command;
        }

        public static TaskItem[] ToTaskItems (this IEnumerable<TaskVM> models)
        {
            return models.Select(x => new TaskItem()
            {
                Id = x.Id,
                Member = x.AssignedToId,
                IsDone = x.IsComplete,
                Text = x.Subject
            }).ToArray();
        }
    }
}
