using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Data.Models.Board;
using TaskBoardApp.Data;

namespace TaskBoardApp.Models.Task
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Owner { get; set; } = null!;

      
    }
}
