using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using UserTasksService.Data;
using UserTasksService.Models;

namespace UserTasksService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITasksRepository taskrepository;

        public TaskController(ITasksRepository repository)
        {
            this.taskrepository = repository;
        }

        private int GetUsedId()
        {
            try
            {
                return Convert.ToInt32(User.Claims.SingleOrDefault(claim => claim.Type == AcountController.ClaimsUserId).Value);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [Authorize]
        [HttpGet("{Id}")]
        public async Task<ActionResult<IncomingUserTask>> Get(int Id)
        {
            try
            {
                var result = await taskrepository.GetTaskAsync(Id);

                if (result == null || GetUsedId() != result.UserId)
                    return BadRequest();

                TaskNumber taskNumber = new TaskNumber(result.Number, result.TaskDate);

                return Ok(
                    new IncomingUserTask()
                    {
                        Comment = result.Comment,
                        Date = result.Date,
                        Status = result.Status,
                        TaskNumber = taskNumber.ToString()
                    });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Repository failure");
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IncomingUserTask[]>> Get()
        {
            try
            {
                var result = await taskrepository.GetAllUserTasksAsync(GetUsedId());

                if (result == null)
                    return BadRequest();

                List<IncomingUserTask> userTasks = new List<IncomingUserTask>(result.Length);
                
                foreach (UserTask task in result)
                { 
                    var taskNumber = new TaskNumber(task.Number, task.TaskDate);
                    
                    userTasks.Add(
                        new IncomingUserTask()
                        {
                            Comment = task.Comment,
                            Date = task.Date,
                            Status = task.Status,
                            TaskNumber = task.ToString()
                        });
                }

                return Ok(userTasks.ToArray());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Repository failure");

            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<IncomingUserTask>> Post(IncomingUserTask task)
        {
            if (ModelState.IsValid)
            {
                var taskNumber = new TaskNumber(task.TaskNumber);
                
                if (taskNumber.IsValid)
                {
                    var userTask = new UserTask()
                    {
                        TaskDate = taskNumber.Date,
                        Number = taskNumber.Number,
                        Date = task.Date,
                        Status = task.Status,
                        Comment = task.Comment,
                        UserId = GetUsedId()
                    };
                    
                    try
                    {
                        taskrepository.Add(userTask);
                        await taskrepository.SaveChangesAsync();
                        return Ok(task);
                    }
                    catch (Exception)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, "Repository failure");
                    }
                }
                else
                    return BadRequest();
            }
            else
                return BadRequest();
        }
    }
}
