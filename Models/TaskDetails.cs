using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskListApp.Models
{
    public class TaskDetails
    {
        [Key]
        public string TaskId { get; set; }

        public string TaskName { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Description { get; set; }

        public char Status { get; set; }

        public DateTime EndDate { get; set; }

        public string Comment { get; set; }
    }
}
