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
    public class GetAll
    {
        
        public class Query : IRequest<List<TaskDTO>>
        {
        }

        public class Handler : IRequestHandler<Query, List<TaskDTO>>
        {
            private readonly DataContext _dbContext;
            private readonly IMapper _mapper;

            //private readonly IUserAccessor _userAccessor;
            public Handler(DataContext dbContext, IMapper mapper)
            {
                _mapper = mapper;
                _dbContext = dbContext;
            }

            public async Task<List<TaskDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                /*var userId = _userAccessor.GetUserId();

                var branches = await _dbContext.Branches.Where(x => x.UserId == userId).ToListAsync();

                if (branches.Count == 0)
                    return Response<List<Branch>>.MakeResponse(true, "No Branches in System", 204); */

                // for same user 
                return await _dbContext.Tasks
                                .ProjectTo<TaskDTO>(_mapper.ConfigurationProvider)
                                .ToListAsync();
            }
        }
    }
}