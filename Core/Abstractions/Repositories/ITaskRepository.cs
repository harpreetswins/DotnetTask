using Domain.DataModels;
using Domain.DataModels.Task;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Abstractions.Repositories
{
    public interface ITaskRepository : IBaseRepository<Guid, Task, ITaskRepository>
    {
    }
}
