using System.ComponentModel.DataAnnotations;

namespace TaskManagerCore.Models
{
    public class Tareas
    {
       
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is empty")]
        [StringLength(100, ErrorMessage = "Maximum text size is 100 characters")]
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
