using System;

namespace Domain.Commands.Task
{
    public class CompleteTaskCommand
    {
        public Guid TaskId { get; set; }
    }
}
