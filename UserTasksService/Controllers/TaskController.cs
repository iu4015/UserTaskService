﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Threading.Tasks;
using UserTasksService.Models;

namespace UserTasksService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IRepository repository;

        public TaskController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<IncomingUserTask>> Get(int Id)
        {
            try
            {
                var result = await repository.GetUserTaskAsync(1, Id);

                if (result == null)
                    return BadRequest();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IncomingUserTask[]>> Get()
        {
            try
            {
                var result = await repository.GetAllUserTasksAsync(1);

                if (result == null)
                    return BadRequest();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");

            }
        }

        [HttpPost]
        public async Task<ActionResult<UserTask>> Post(IncomingUserTask task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userTask = new UserTask()
                    {
                        TaskDate = DateTime.ParseExact(task.TaskNumber.Substring(0, 8), "yyyyMMdd", null, DateTimeStyles.None),
                        Number = Convert.ToInt32(task.TaskNumber.Substring(9, 4)),
                        Date = task.Date,
                        Status = task.Status,
                        Comment = task.Comment,
                        UserId = 1
                    };
                    repository.Add(userTask);
                    await repository.SaveChangesAsync();
                return Ok(userTask);
                }
                else
                    return BadRequest();
            }
            catch (Exception)
            {

                return BadRequest();
            }
            
        }
    }
}
