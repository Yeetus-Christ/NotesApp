using System.ComponentModel.DataAnnotations;

namespace NotesApp.Models
{
    public class Note
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
