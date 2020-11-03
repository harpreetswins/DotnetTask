using Domain.ClientSideModels;
using Domain.ViewModel.Task;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Abstractions;
using WebClient.Services;

namespace WebClient.Pages
{
    public class MemberTaskBase : ComponentBase
    {
        protected bool isLoaded;
        protected bool showCreator;

        protected IEnumerable<TaskVM> tasks;
        protected List<TaskVM> MyTasks;
        protected List<TaskVM> rightMenuItem = new List<TaskVM>();

        [Inject]
        public ITaskDataService TaskDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            UpdateTasks();
            isLoaded = true;
        }

        void UpdateTasks()
        {
            var result = TaskDataService.Tasks;
            if (result!=null)
            {
                MyTasks = result.ToList();
            }
        }
        public void ReloadPage(Guid Id)
        {
            MyTasks = TaskDataService.Tasks.Where(x => x.assignedToId == Id).ToList();
        }
        public void ReloadPage()
        {
            TaskDataService.LoadTasks();
            UpdateTasks();

        }
        public void MakeItCompleted(Guid id)
        {
            MyTasks.FirstOrDefault(x => x.id == id).isComplete = true;
        }

        public void MakeItAssigned()
        {
            //MyTasks.FirstOrDefault(x => x.id == task.id).assignedToId = task.assignedToId;
        }

        private void TaskDataService_TaskChanged(object sender, EventArgs e)
        {
            UpdateTasks();
            isLoaded = true;

            StateHasChanged();
        }

        protected void onTasksAdd(TaskVM Task)
        {
            TaskDataService.CreateTask(Task);
        }
    }
}
