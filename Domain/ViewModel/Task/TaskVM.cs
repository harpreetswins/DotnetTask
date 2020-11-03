using Domain.Commands.Task;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModel.Task
{
    public class TaskVM
    {
        public Guid id { get; set; }
        public string subject { get; set; }
        public bool isComplete { get; set; }
        public Guid? assignedToId { get; set; }

        public CompleteTaskCommand ToCompleteCommand()
        {
            throw new NotImplementedException();
        }
    }
}
