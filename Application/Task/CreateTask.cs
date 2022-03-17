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



namespace Application.Task
{
    public class CreateTask
    {
        
        public class Command : IRequest<string>
        {
            public TaskDTO Task { get; set; } 

        }

        public class Handler : IRequestHandler<Command, string>
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

            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                AppUser user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == _userAccessor.GetUserId());              
                
                if (user == null)
                    return "Account Loading Failed";

                var task = _mapper.Map<Domain.Task>(request.Task);

                task.User = user;
                task.UserId = user.Id;

                //_dbContext.Branches.Add(request.Branch);

                //var result = await _dbContext.SaveChangesAsync() > 0;

                if (task != null)
                    return "Task Created";
                
                return user.Email;
            }

        }
    }
}