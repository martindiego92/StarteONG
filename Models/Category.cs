using System.ComponentModel.DataAnnotations;

namespace starteAlkemy.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}