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

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecificTask(int id)
        {
            return Ok(await Mediator!.Send(new GetById.Query{Id = id}));
        } 

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllTask()
        {
            return Ok(await Mediator!.Send(new GetAll.Query()));
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> PostNewTask(TaskDTO task)
        {
            return Ok(await Mediator!.Send(new CreateTask.Command{Task = task}));
        } 
        
        [HttpPut]
        public async Task<IActionResult> UpdateTaska(TaskDTO task)
        {
            return Ok(await Mediator!.Send(new UpdateTask.Command{Task = task}));
        }  

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            return Ok(await Mediator!.Send(new DeleteTask.Command{Id = id}));
        } 
          
    }
}