using Domain.Commands.Task;
using Domain.Queries.Task;
using System.Threading.Tasks;

namespace Core.Abstractions.Services
{
    public interface ITaskService
    {
        Task<CreateTaskCommandResult> CreateTaskCommandHandler(CreateTaskCommand command);
        Task<AssignTaskCommandResult> AssignTaskCommandHandler(AssignTaskCommand command);
        Task<CompleteTaskCommandResult> CompleteTaskCommandHandler(CompleteTaskCommand command);
        Task<CompleteTaskForMemberCommandResult> CompleteTaskForMemberCommandHandler(CompleteTaskForMemberCommand command);

        Task<GetAllTasksQueryResult> GetAllTaskQueryHandler();
        Task<GetAllTasksOfMemberResult> GetAllTasksOfMember();
    }
}
