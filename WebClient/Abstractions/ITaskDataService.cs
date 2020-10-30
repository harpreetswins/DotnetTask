﻿using Domain.ViewModel.Task;
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
        IEnumerable<TaskVM> Tasks { get; }
        IEnumerable<TaskVM> TaskList { get; set; }
        TaskVM SelectedTask { get; }

        event EventHandler TasksUpdated;
        event EventHandler TaskSelected;

        void SelectTask(Guid id);
        Task ToggleTask(Guid id);
        Task AddTask(TaskVM model);

        Task LoadTasks();
    }
}