using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using System.Xml.Linq;
using TaskBoardApp.Data;
using TaskBoardApp.Models;

namespace TaskBoardApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly TaskAppBoardDbContext data;

        public HomeController(TaskAppBoardDbContext context)
        {
            data = context;
        }

        
        //public async Task<IActionResult> Index()
        //{
        //    var taskBoards = data.Boards.Select(b => b.Name).Distinct();

        //    var tasksCounts = new List<HomeBoardModel>();

        //    foreach (var boardName in taskBoards)
        //    {
        //        var tasksInBoard = await data.Tasks.Where(t => t.Board.Name == boardName).CountAsync();

        //        tasksCounts.Add(new HomeBoardModel
        //        {
        //            BoardName = boardName,
        //            TasksCount = tasksInBoard
        //        });
        //    }

        //    var userTasksCount = -1;

        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //        if (currentUserId != null)
        //        {
        //            userTasksCount = await data.Tasks.Where(t => t.OwnerId == currentUserId).CountAsync();
        //        }
        //    }

        //    var homeModel = new HomeViewModel
        //    {
        //        AllTasksCount = await data.Tasks.CountAsync(),
        //        BoardWithTasksCount = tasksCounts,
        //        UserTasksCount = userTasksCount
        //    };

        //    return View(homeModel);
        //}



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}