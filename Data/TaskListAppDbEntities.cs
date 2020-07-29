using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskListApp.Models;

namespace TaskListApp.Data
{
    public class TaskListAppDbEntities : DbContext
    {
        public TaskListAppDbEntities()
        {
        }

        public TaskListAppDbEntities (DbContextOptions<TaskListAppDbEntities> options)
            : base(options)
        {
        }

        public DbSet<TaskListApp.Models.TaskDetails> TaskDetails { get; set; }
    }
}
