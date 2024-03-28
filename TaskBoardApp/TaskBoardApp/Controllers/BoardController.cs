using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Data;
using TaskBoardApp.Models.Board;
using TaskBoardApp.Models.Task;

namespace TaskBoardApp.Controllers
{
    public class BoardController : Controller
    {
        private readonly TaskAppBoardDbContext data;

        public BoardController(TaskAppBoardDbContext context)
        {
            data = context;
        }

        public async Task<IActionResult> All()
        {
            var boards = await data.Boards
                .Select(b => new BoardViewModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Tasks = b.Tasks
                    .Select(b => new TaskViewModel()
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Description = b.Description,
                        Owner = b.Owner.UserName
                    })

                })
                .ToListAsync();

            return View(boards);

        }
    }
}
