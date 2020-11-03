using Domain.ViewModel.Task;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebClient.Shared.Models;

namespace WebClient.Abstractions
{
    /// <summary>
    /// This Service is currently using the TaskModel Class, and will need to use a shared view
    /// model after the model has been created.  For the moment, this pattern facilitates a client
    /// side storage mechanism to view functionality.  See work completed for the MemberDataService
    /// for an example of expectations.
    /// </summary>
    public interface ITaskDataService
    {
        List<TaskVM> Tasks { get; set; }
        /** events **/
        event EventHandler TasksChanged;
        event EventHandler TaskCreatedSuccess;
        event EventHandler TaskCreatedFailed;
        event EventHandler TaskCompleteSuccess;
        event EventHandler TaskCompletedFailed;
        event EventHandler TaskAssignedSuccess;
        event EventHandler TaskAssignedFailed;

        /** tasks **/
        Task UpdateTask(TaskVM model);
        Task CreateTask(TaskVM model);
        Task AssignTask(Guid TaskId, Guid MemberId);
        Task LoadTasks();
        Task ToggleTask(Guid id);
    }
}