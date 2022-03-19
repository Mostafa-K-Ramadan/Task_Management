using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Task
{
    public class UpdateTask
    {
        public class Command : IRequest<Unit>
        {
            public TaskDTO Task { get; set; }
        }
        
        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly DataContext _dbContext;
            private readonly IMapper _mapper;
            public Handler(DataContext dbContext, IMapper mapper)
            {
                _mapper = mapper;
                _dbContext = dbContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var task = await _dbContext.Tasks.FindAsync(request.Task.TaskId);
                
                var objTask = _mapper.Map<Domain.Task>(request.Task);

                objTask.UserId = task.UserId;

                 _mapper.Map(objTask, task);
                
                var result = await _dbContext.SaveChangesAsync() > 0;
                Console.WriteLine(result);

                if (result)
                    {
                    return Unit.Value;
                    }
                
                return Unit.Value;
            }
        }
    }
}

