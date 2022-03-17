using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Task
{
    public class GetAll
    {
        
        public class Query : IRequest<List<Domain.Task>>
        {
        }

        public class Handler : IRequestHandler<Query, List<Domain.Task>>
        {
            private readonly DataContext _dbContext;

            //private readonly IUserAccessor _userAccessor;
            public Handler(DataContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<List<Domain.Task>> Handle(Query request, CancellationToken cancellationToken)
            {
                /*var userId = _userAccessor.GetUserId();

                var branches = await _dbContext.Branches.Where(x => x.UserId == userId).ToListAsync();

                if (branches.Count == 0)
                    return Response<List<Branch>>.MakeResponse(true, "No Branches in System", 204); */

                return await _dbContext.Tasks.ToListAsync();
            }

        }
    }
}