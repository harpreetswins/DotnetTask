using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ClientSideModels
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public Guid? Member { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
    }
}
