using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Data.Models.Task;
using TaskBoardApp.Data;
using TaskBoardApp.Models.Task;

namespace TaskBoardApp.Models.Board
{
    public class BoardViewModel
    {
        public int Id { get; init; } // za da ne se povtarqt

       
        public string Name { get; set; } = null!;

        public IEnumerable<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
    }
}
