using System.ComponentModel.DataAnnotations;

namespace starteAlkemy.Models
{
    public class HomeImages
    {
        [Key]
        public int Id { get; set; }
        public string Link { get; set; }
        public string Text { get; set; }
        public bool Active { get; set; }
    }
}