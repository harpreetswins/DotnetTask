using Core.Abstractions.Services;
using Core.Extensions.ModelConversion;
using Domain.Commands.Task;
using Domain.Queries.Task;
using Domain.ViewModel.Task;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebClient.Abstractions;
using WebClient.Shared.Models;

namespace WebClient.Services
{
    public class TaskDataService : ITaskDataService
    {
        private readonly HttpClient httpClient;
        private List<TaskVM> tasks;
        public List<TaskVM> Tasks { get; set; }

        public TaskDataService(IHttpClientFactory clientFactory)
        {
            httpClient = clientFactory.CreateClient("FamilyTaskAPI");
            tasks = new List<TaskVM>();
            Task.Run(async () => { await LoadTasks(); });
        }


        public event EventHandler TasksChanged;
        public event EventHandler TaskCreatedSuccess;
        public event EventHandler TaskCreatedFailed;
        public event EventHandler TaskCompleteSuccess;
        public event EventHandler TaskCompletedFailed;
        public event EventHandler TaskAssignedSuccess;
        public event EventHandler TaskAssignedFailed;


        public async Task LoadTasks()
        {
            Tasks = (await GetAllTasks()).Payload.ToList();
            TasksChanged?.Invoke(this, null);
        }
        /// <summary>
        /// Update current task
        /// </summary>
        /// <param name="model"></param>
        /// <returns>CompleteTaskCommandResult</returns>
        public async Task UpdateTask(TaskVM model)
        {
            try
            {
                var result = await Update(model.ToCompleteCommand());
                if (result != null)
                    TaskCompleteSuccess.Invoke(this, null);
                else
                    TaskCompletedFailed.Invoke(this, null);
            }
            catch (Exception ex)
            {
                TaskCompletedFailed.Invoke(this, null);
            }
        }

        /// <summary>
        /// Create new task
        /// </summary>
        /// <param name="model"></param>
        /// <returns>CreateTaskCommandResult</returns>
        public async Task CreateTask(TaskVM model)
        {
            try
            {
                var updatedModel = model.ToCreateTaskCommand();
                if (!updatedModel.AssignedToId.HasValue)
                {
                    updatedModel.AssignedToId = null;
                }
                var result = await Create(updatedModel);
                Tasks = (await GetAllTasks()).Payload.ToList();
                if (result != null)
                    TaskCreatedSuccess.Invoke(this, null);
                else
                    TaskCreatedFailed.Invoke(this, null);
            }
            catch (Exception ex)
            {
                TaskCreatedFailed.Invoke(this, null);
            }
        }
        /// <summary>
        /// Complete current task
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CompleteTaskCommandResult</returns>
        public async Task ToggleTask(Guid id)
        {
            try
            {
                var isTaskCompleted = await CompleteTask(new CompleteTaskCommand() { TaskId = id });
                if (isTaskCompleted != null)
                    TaskCompleteSuccess.Invoke(this, null);
                else
                    TaskCompletedFailed.Invoke(this, null);
               
            }
            catch (Exception ex)
            {
                TaskCompletedFailed.Invoke(this, null);
            }
          

        }
        /// <summary>
        /// Assign current task to member
        /// </summary>
        /// <param name="TaskId"></param>
        /// <param name="MemberId"></param>
        /// <returns>HttpResponseMessage</returns>
        public async Task AssignTask(Guid TaskId, Guid MemberId)
        {
            try
            {
                var isAssigned = await AssignTaskToMember(new AssignTaskCommand() { TaskId = TaskId, MemberId = MemberId });
                Tasks = (await GetAllTasks()).Payload.ToList();
                if (isAssigned != null)
                    TaskAssignedSuccess.Invoke(this, null);
                else
                    TaskAssignedFailed.Invoke(this, null);
            }
            catch (Exception)
            {
                TaskAssignedFailed.Invoke(this, null);
            }
        }

        private async Task<CompleteTaskCommandResult> CompleteTask(CompleteTaskCommand command)
        {
            return await httpClient.PutJsonAsync<CompleteTaskCommandResult>($"tasks/{command.TaskId}", command);
        }
        private async Task<HttpResponseMessage> AssignTaskToMember(AssignTaskCommand command)
        {
            var httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            return await httpClient.PatchAsync($"tasks/{command.TaskId}", httpContent);
        }
        private async Task<CreateTaskCommandResult> Create(CreateTaskCommand command)
        {
            return await httpClient.PostJsonAsync<CreateTaskCommandResult>("tasks", command);
        }

        private async Task<GetAllTasksQueryResult> GetAllTasks()
        {
            return await httpClient.GetJsonAsync<GetAllTasksQueryResult>("tasks");
        }

        private async Task<CompleteTaskCommandResult> Update(CompleteTaskCommand command)
        {
            return await httpClient.PutJsonAsync<CompleteTaskCommandResult>($"Tasks/{command.TaskId}", command);
        }

    }
}