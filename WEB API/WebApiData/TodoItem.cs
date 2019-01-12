using System.ComponentModel.DataAnnotations;

namespace WebApiData
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsComplete { get; set; } = false;
    }
}
