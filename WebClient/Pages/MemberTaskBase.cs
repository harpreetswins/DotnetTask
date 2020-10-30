using Domain.ClientSideModels;
using Domain.ViewModel.Task;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Abstractions;

namespace WebClient.Pages
{
    public class MemberTaskBase:ComponentBase
    {
        protected bool isLoaded;
        protected bool showCreator;

        protected List<TaskVM> members = new List<TaskVM>();
        protected List<TaskItem> leftMenuItem = new List<TaskItem>();

        [Inject]
        public ITaskDataService MemberDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {

        }
    }
}
