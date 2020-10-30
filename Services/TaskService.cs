using AutoMapper;
using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using Domain.Commands.Task;
using Domain.DataModels.Task;
using Domain.Queries.Task;
using Domain.ViewModel;
using Domain.ViewModel.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository,IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }
        public async Task<AssignTaskCommandResult> AssignTaskCommandHandler(AssignTaskCommand command)
        {
            var isSucceed = true;
            var task = await _taskRepository.ByIdAsync(command.TaskId);
            task.AssignedToId = command.MemberId;

            var affectedRecordsCount = await _taskRepository.UpdateRecordAsync(task);
            if (affectedRecordsCount < 1)
            {
                isSucceed = false;
            }
            return new AssignTaskCommandResult()
            {
                Success = isSucceed
            };
        }

        public async Task<CompleteTaskCommandResult> CompleteTaskCommandHandler(CompleteTaskCommand command)
        {
            var isSucceed = true;

            var task = await _taskRepository.ByIdAsync(command.TaskId);
            task.IsComplete = true;

            var affectedRecordsCount = await _taskRepository.UpdateRecordAsync(task);
            if (affectedRecordsCount <1)
            {
                isSucceed = false;
            };
            return new CompleteTaskCommandResult()
            {
                Success = isSucceed
            };
        }

        public Task<CompleteTaskForMemberCommandResult> CompleteTaskForMemberCommandHandler(CompleteTaskForMemberCommand command)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateTaskCommandResult> CreateTaskCommandHandler(CreateTaskCommand command)
        {
            var task = _mapper.Map<Domain.DataModels.Task.Task>(command);
            var persistedTask = await _taskRepository.CreateRecordAsync(task);
            var vm = _mapper.Map<TaskVM>(persistedTask);
            return new CreateTaskCommandResult()
            {
                Payload = vm
            };
        }

        public async Task<GetAllTasksQueryResult> GetAllTaskQueryHandler()
        {
            IEnumerable<TaskVM> vm = new List<TaskVM>();

            var tasks = await _taskRepository.Reset().ToListAsync();

            if (tasks != null && tasks.Any())
                vm = _mapper.Map<IEnumerable<TaskVM>>(tasks);

            return new GetAllTasksQueryResult()
            {
                Payload = vm
            };
        }

        public Task<GetAllTasksOfMemberResult> GetAllTasksOfMember()
        {
            throw new NotImplementedException();
        }
    }
}
