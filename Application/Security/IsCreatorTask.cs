using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Security
{
    public class IsCreatorTask : IAuthorizationRequirement
    {

    }

    public class IsCreatorTaskHandler : AuthorizationHandler<IsCreatorTask>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _dataContext;
        public IsCreatorTaskHandler(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsCreatorTask requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Task.CompletedTask;
            
            var taskId = int.Parse(_httpContextAccessor.HttpContext?.Request.RouteValues.SingleOrDefault(x => x.Key == "id").Value?.ToString());

            var task = _dataContext.Tasks
            .AsNoTracking()
            .SingleOrDefault(x => x.TaskId == taskId && x.UserId == userId);
                            
            if (task != null)
                context.Succeed(requirement);

            return Task.CompletedTask;

           /* var attendee = _dbContext.ActivityAttendees
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.AppUserId == userId && x.ActivityId == activityId)
                .Result;*/
        }
    }
    
}