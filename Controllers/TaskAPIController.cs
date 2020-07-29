using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskListApp.Data;
using TaskListApp.Models;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace TaskListApp.Controllers
{
    [Route("Api/Task")]
    public class TaskAPIController : ApiController
    {
        TaskListAppDbEntities objEntity = new TaskListAppDbEntities();

        [HttpGet]
        [Route("AllTaskDetails")]
        public IQueryable<TaskDetails> GetTask()
        {
            try
            {
                return objEntity.TaskDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetTaskDetailsById/{tasksId}")]
        public IHttpActionResult GetTaskById(string tasksId)
        {
            TaskDetails objTask = new TaskDetails();
            //int ID = Convert.ToInt32(tasksId);
            try
            {
                objTask = objEntity.TaskDetails.Find(tasksId);
                if (objTask == null)
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                throw;
            }

            return Ok(objTask);
        }

        [HttpPost]
        [Route("InsertTaskDetails")]
        public IHttpActionResult PostTask(TaskDetails data)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                objEntity.TaskDetails.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(data);
        }

        [HttpPut]
        [Route("UpdateTaskDetails")]
        public IHttpActionResult PutEmaployeeMaster(TaskDetails task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                TaskDetails objTask = new TaskDetails();
                objTask = objEntity.TaskDetails.Find(task.TaskId);
                if (objTask != null)
                {
                    objTask.TaskName = task.TaskName;
                    objTask.CreatedAt = task.CreatedAt;
                    objTask.Description = task.Description;
                    objTask.Status = task.Status;
                    objTask.EndDate = task.EndDate;
                    objTask.Comment = task.Comment;

                }
                int i = this.objEntity.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
            return Ok(task);
        }

        [HttpDelete]
        [Route("DeleteTaskDetails")]
        public IHttpActionResult DeleteTaskDelete(int id)
        {
            //int TaskId = Convert.ToInt32(id);  
            TaskDetails task = objEntity.TaskDetails.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            objEntity.TaskDetails.Remove(task);
            objEntity.SaveChanges();

            return Ok(task);
        }
    }
}
