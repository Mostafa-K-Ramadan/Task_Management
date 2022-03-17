using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TaskDTO
    {
        public int? TaskId { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public DateTime Date { get; set; }
    }
}