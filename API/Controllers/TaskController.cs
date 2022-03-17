using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Task;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{

    public class TaskController : GenericController
    {
        private readonly DataContext _dbContext;

        public TaskController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllTask()
        {
            return Ok(await Mediator!.Send(new GetAll.Query()));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostNewTask(TaskDTO task)
        {
            return Ok(await Mediator!.Send(new CreateTask.Command{Task = task}));
        } 
        
        /*
        [HttpGet]
        public async Task<IActionResult> GetAllTask()
        {
            return Ok(await Mediator!.Send(new GetAll.Query()));
        }  
        [HttpGet]
        public async Task<IActionResult> GetAllTask()
        {
            return Ok(await Mediator!.Send(new GetAll.Query()));
        }  
        [HttpGet]
        public async Task<IActionResult> GetAllTask()
        {
            return Ok(await Mediator!.Send(new GetAll.Query()));
        }
        */   
    }
}