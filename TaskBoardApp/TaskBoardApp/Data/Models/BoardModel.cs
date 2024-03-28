using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Data.Models.Task;

namespace TaskBoardApp.Data.Models.Board
{
    public class BoardModel
    {
       
        public int Id { get; init; } // za da ne se povtarqt

        [Required]
        [MaxLength(DataConstants.Board.BoardMaxLength)]
        public string Name { get; set; } = null!;

        public IEnumerable<TaskModel> Tasks { get; set; } = new List<TaskModel>();  

       
    }
}
