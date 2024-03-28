using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Data.Models.Board;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
 

namespace TaskBoardApp.Data.Models.Task
{
    public class TaskModel
    {
        public int Id { get; set; }
 
        [MaxLength(DataConstants.Task.TaskMaxLength)]
        [Required]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DataConstants.Task.DescriotionMaxLength)]
        public string Description { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public int BoardId { get; set; }

        public BoardModel? Board { get; set; }

        [Required]
        public string OwnerId { get; set; } = null!;

        public IdentityUser Owner { get; set; } = null!;
    }
}
