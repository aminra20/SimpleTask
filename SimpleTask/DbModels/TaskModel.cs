using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleTask.DbModels
{
    public class TaskModel
    {
        public string Id { get; set; }
        [Required (ErrorMessage ="Please Enter {0}")]
        [Display(Name="Task Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter {0} format yyyy/MM/dd")]
        [Display(Name = "Task Due date")]
        public DateTime DueDate { get; set; }

        public Helper.TaskStatus Status { get; set; }
    }
}
