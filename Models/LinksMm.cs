using System.ComponentModel.DataAnnotations;

namespace starteAlkemy.Models
{
    public class LinksMm
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Text { get; set; }
        public short Type { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public bool Active { get; set; }
    }
}