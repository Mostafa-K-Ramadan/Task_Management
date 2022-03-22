using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.TaskOb
{
    public class GetById
    {
         public class Query : IRequest<TaskDTO> { 

            public int Id {get; set;} 
        }

        public class Handler : IRequestHandler<Query, TaskDTO>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<TaskDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var task = await _context.Tasks
                    .ProjectTo<TaskDTO>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.TaskId == request.Id);

                return task;
            }
        }
    }
}