using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace starteAlkemy.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime DateProject { get; set; }
        public int CategoryId { get; set; }
        public int AdminId { get; set; }
        public bool Active { get; set; }
        public List<LinksMm> ListLinkProject { get; set; }
    }
}