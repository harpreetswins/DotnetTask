using Domain.ViewModel;
using Domain.ViewModel.Task;
using System.Collections.Generic;

namespace Domain.Queries.Task
{
    public class GetAllTasksOfMemberResult
    {
        public IEnumerable<TaskVM> Payload { get; set; }
    }
}
