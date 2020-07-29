using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskListApp.Data;
using TaskListApp.Models;

namespace TaskListApp.Controllers
{
    [Route("api/Task")]
    [ApiController]
    public class TaskDetailsController : ControllerBase
    {
        private readonly TaskListAppDbEntities _context;

        public TaskDetailsController(TaskListAppDbEntities context)
        {
            _context = context;
        }

        // GET: api/TaskDetails
        //[HttpGet]
        [Route("AllTaskDetails")]
        public async Task<ActionResult<IEnumerable<TaskDetails>>> GetTaskDetails()
        {
            return await _context.TaskDetails.ToListAsync();
        }

        // GET: api/TaskDetails/5
        //[HttpGet("{id}")]
        [Route("GetTaskDetailsById/{id}")]
        public async Task<ActionResult<TaskDetails>> GetTaskDetails(string id)
        {
            var taskDetails = await _context.TaskDetails.FindAsync(id);

            if (taskDetails == null)
            {
                return NotFound();
            }

            return taskDetails;
        }

        // PUT: api/TaskDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        [Route("UpdateTaskDetails/{id}")]
        public async Task<IActionResult> PutTaskDetails(string id, TaskDetails taskDetails)
        {
            if (id != taskDetails.TaskId)
            {
                return BadRequest();
            }

            _context.Entry(taskDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TaskDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        [Route("InsertTaskDetails")]
        public async Task<ActionResult<TaskDetails>> PostTaskDetails(TaskDetails taskDetails)
        {
            taskDetails.TaskId = Guid.NewGuid().ToString();
            taskDetails.CreatedAt = DateTime.UtcNow.Date;
               
            _context.TaskDetails.Add(taskDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskDetails", new { id = taskDetails.TaskId }, taskDetails);
        }

        // DELETE: api/TaskDetails/5
        //[HttpDelete("{id}")]
        [Route("DeleteTaskDetails/{id}")]
        public async Task<ActionResult<TaskDetails>> DeleteTaskDetails(string id)
        {
            var taskDetails = await _context.TaskDetails.FindAsync(id);
            if (taskDetails == null)
            {
                return NotFound();
            }

            _context.TaskDetails.Remove(taskDetails);
            await _context.SaveChangesAsync();

            return taskDetails;
        }

        private bool TaskDetailsExists(string id)
        {
            return _context.TaskDetails.Any(e => e.TaskId == id);
        }
    }
}
