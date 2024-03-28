﻿using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace TaskBoardApp.Models.Task
{
    public class TaskFormModel
    {
        [Required]
        [StringLength(Data.DataConstants.Task.TaskMaxLength,MinimumLength = Data.DataConstants.Task.TaskMinLength,
            ErrorMessage = "Title should be at least {2} characters long.")]
        public string Title { get; set; } =  null!;

        [Required]
        [StringLength(Data.DataConstants.Task.DescriotionMaxLength, MinimumLength = Data.DataConstants.Task.DescriotionMinLength,
            ErrorMessage = "Description should be at least {2} characters long.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Board")]
        public int BoardId { get; set; }

        public IEnumerable<TaskBoardModel> Boards { get; set; } = null!;
    }
}
