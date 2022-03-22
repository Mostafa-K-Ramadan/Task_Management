using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;



namespace Application.TaskOb
{
    public class CreateTask
    {
        
        public class Command : IRequest<TaskDTO>
        {
            public TaskDTO Task { get; set; } 

        }

        public class Handler : IRequestHandler<Command, TaskDTO>
        {
            private readonly DataContext _dbContext;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;
            public Handler(DataContext dbContext,IUserAccessor userAccessor, IMapper mapper)
            {
                _mapper = mapper;
                _dbContext = dbContext;
                _userAccessor = userAccessor;
            }

            public async Task<TaskDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                AppUser user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == _userAccessor.GetUserId());              
                
                if (user == null)
                    return null;

                var task = _mapper.Map<Domain.Task>(request.Task);

                task.User = user;
                task.UserId = user.Id;

                _dbContext.Tasks.Add(task);
                var result = await _dbContext.SaveChangesAsync() > 0;

                if (result)
                    return _mapper.Map<TaskDTO>(task);
                
                return null;
            }

        }
    }
}