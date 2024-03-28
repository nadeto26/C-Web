using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskBoardApp.Data;
using TaskBoardApp.Data.Models.Task;
using TaskBoardApp.Models.Task;

namespace TaskBoardApp.Controllers
{
    public class TaskController : Controller
    {
       
        private readonly TaskAppBoardDbContext data;

        public TaskController(TaskAppBoardDbContext context)
        {
            data = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Create()
        {
            TaskFormModel taskFormModel = new TaskFormModel()
            {
                Boards = GetBoards()
            };

            return View(taskFormModel);
        }

        [Authorize]
        private IEnumerable<TaskBoardModel> GetBoards()

           => data.Boards.Select(b => new TaskBoardModel
           {
               Id = b.Id,
               Name = b.Name
           });

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(TaskFormModel taskModel)
        {
            if(!GetBoards().Any(b=>b.Id==taskModel.BoardId)) 
            {
                ModelState.AddModelError(nameof(taskModel.BoardId), "Boards does not exist");
            }

            string currentUserId = GetUserId();

            if(!ModelState.IsValid)
            {
                taskModel.Boards = GetBoards();

                return View(taskModel);
            }

            TaskModel task = new TaskModel()
            {
               Title = taskModel.Title,
               Description = taskModel.Description,
               CreatedOn = DateTime.Now,
               BoardId = taskModel.BoardId,
               OwnerId = currentUserId
            };

            await data.Tasks.AddAsync(task);
            var boards = data.Boards;


            return RedirectToAction("All","Board");
        }

        [Authorize]
        public async Task<ActionResult> Details(int id)
        {
            var task = await data.Tasks
                .Where(t => t.Id == id)
                .Select(t => new TaskDetailsViewModel()
                {
                    Id=t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreatedOn = t.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                    Board = t.Board.Name,
                    Owner = t.Owner.UserName
                })
                .FirstOrDefaultAsync();

            if(task == null)
            {
                return  BadRequest();
            }
            return View(task);
        }

        [Authorize]
        private string GetUserId()
        => User.FindFirstValue(ClaimTypes.NameIdentifier);

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await data.Tasks
                .FindAsync(id);
                
            if(task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();

            if(currentUserId!= task.OwnerId)
            {
                return Unauthorized();
            }

            TaskFormModel model = new TaskFormModel()
            {
                Title = task.Title,
                Description = task.Description,
                BoardId = task.BoardId,
                Boards = GetBoards()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult>Edit (int id, TaskFormModel taskModel)
        {
            var task = await data.Tasks .FindAsync(id);

            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();
            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            if(GetBoards().Any(b=>b.Id==taskModel.BoardId))
            {
                ModelState.AddModelError(nameof(taskModel.BoardId), "Board does not exist!");
            }

            if(!ModelState.IsValid)
            {
                taskModel.Boards = GetBoards();

                return View(taskModel);
            }

            task.Title = taskModel.Title;
            task.Description = taskModel.Description;
            task.BoardId = taskModel.BoardId;

            await data.SaveChangesAsync();
            return RedirectToAction("All", "Board");
        }

        [HttpGet]

        public async Task<IActionResult> Delete(int id)
        {
            var task = await data.Tasks.FindAsync(id);

            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();
            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            TaskViewModel taskViewModel = new TaskViewModel()
            {
                Id= task.Id,
                Title = task.Title,
                Description = task.Description,
            };

            return View(taskViewModel);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(TaskViewModel taskModel)
        {
            var task = await data.Tasks.FindAsync(taskModel.Id);

            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();
            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            data.Remove(task);
            await data.SaveChangesAsync();
            return RedirectToAction("All", "Board");
            

        }


    }
}
