using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Task
    {

        public int TaskId { get; set; }

        [Required]
        [StringLength(1024)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(160)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        public AppUser User { get; set; } = null!;
        public string UserId { get; set; } = string.Empty;
    }
}