using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebApiJWS.Models;

namespace WebApiJWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly EFTodoDBContext db;

        private TodoHab todoHab;
        public ValuesController(EFTodoDBContext context)
        {
            db = context;
            todoHab = new TodoHab();
        }

        [Authorize]
        [Route("getlogin")]
        public IActionResult GetLogin()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }

        [Authorize]
        [Route("getlist")]
        public IActionResult GetList()
        {
            //var response = new
            //{
            //    list = db.TodoItems
            //};
            return new ObjectResult(db.TodoItems);
        }

        [Authorize(Roles = "admin")]
        [Route("getrole")]
        public IActionResult GetRole()
        {
            return Ok("Ваша роль: администратор");
        }

        [Authorize(Roles = "admin")]
        [HttpPost("Item")]
        public async Task<IActionResult>  addItem(RequestTodoItem requestTodoItem)
        {

            TodoItem todoItem = new TodoItem()
            {
                IsComplete = false,
                TaskDescription = requestTodoItem.TaskDescription,
            };
          await  db.TodoItems.AddAsync(todoItem);
         await   db.SaveChangesAsync();
            return new ObjectResult(todoItem);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("Item")]
        public async Task<IActionResult> ChangeItem(TodoItem todoItem)
        {

             db.TodoItems.FirstOrDefault(t => t.Id == todoItem.Id).IsComplete = todoItem.IsComplete;

            await db.SaveChangesAsync();
          // await todoHab.Send("fff");
          
            return new ObjectResult(todoItem);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("Item")]
        public async Task<IActionResult> DeleteItem(TodoItem todoItem)
        {
            db.TodoItems.Remove(todoItem);
            await db.SaveChangesAsync();
            return Ok(true);
        }
    }
}
