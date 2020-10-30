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
using System.Threading.Tasks;
using WebClient.Abstractions;
using WebClient.Shared.Models;

namespace WebClient.Services
{
    public class TaskDataService : ITaskDataService
    {
        private readonly HttpClient httpClient;
        public TaskDataService(IHttpClientFactory clientFactory)
        {
            httpClient = clientFactory.CreateClient("FamilyTaskAPI");

        }

        private IEnumerable<TaskVM> tasks;

        public IEnumerable<TaskVM> Tasks => tasks;


        public TaskVM SelectedTask { get; private set; }

        public IEnumerable<TaskVM> TaskList { get; set; }

        public event EventHandler TasksUpdated;
        public event EventHandler TaskSelected;

        public async Task LoadTasks()
        {
            TaskList = (await GetAllTasks()).Payload;
        }

        public void SelectTask(Guid id)
        {
            SelectedTask = Tasks.SingleOrDefault(t => t.Id == id);
            TasksUpdated?.Invoke(this, null);
        }

        public async Task ToggleTask(Guid id)
        {
            await CompleteTask(new CompleteTaskCommand() { TaskId = id });
            TasksUpdated?.Invoke(this, null);
        }
        private async Task<CompleteTaskCommandResult> CompleteTask(CompleteTaskCommand command)
        {
            return await httpClient.PutJsonAsync<CompleteTaskCommandResult>($"tasks/{command.TaskId}", command);
        }
        private async Task<CreateTaskCommandResult> Create(CreateTaskCommand command)
        {
            return await httpClient.PostJsonAsync<CreateTaskCommandResult>("tasks", command);
        }

        private async Task<GetAllTasksQueryResult> GetAllTasks()
        {
            return await httpClient.GetJsonAsync<GetAllTasksQueryResult>("tasks");
        }

        public async Task AddTask(TaskVM model)
        {
            await Create(model.ToCreateTaskCommand());
            //Tasks.Add(model);
            LoadTasks();
            TasksUpdated?.Invoke(this, null);
        }
    }
}