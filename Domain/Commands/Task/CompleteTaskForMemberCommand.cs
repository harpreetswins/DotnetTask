using System;

namespace Domain.Commands.Task
{
    public class CompleteTaskForMemberCommand
    {
        public Guid TaskId { get; set; }
        public Guid MemberId { get; set; }
    }
}
