using AutoMapper;
using Domain.Commands.Task;
using Domain.DataModels.Task;
using Domain.ViewModel.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.AutoMapper
{
    public class TaskProfile:Profile
    {
        public TaskProfile()
        {
            CreateMap<CreateTaskCommand, Domain.DataModels.Task.Task>();
            CreateMap<Domain.DataModels.Task.Task, TaskVM>();
        }
    }
}
