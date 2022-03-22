using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.TaskOb
{
    public class DeleteTask
    {
        public class Command : IRequest<Unit>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var task = await _context.Tasks.FindAsync(request.Id);

                //if (activity == null) return null;

                _context.Remove(task);

                var result = await _context.SaveChangesAsync() > 0;

                if (result) 
                    return Unit.Value;

                return Unit.Value;
            }
        }
    }
}