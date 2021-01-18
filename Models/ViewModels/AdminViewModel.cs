using System.ComponentModel.DataAnnotations;

namespace starteAlkemy.Models.ViewModels
{
    public class AdminViewModel
    {
        public AdminViewModel(){}

        public AdminViewModel(Admin a)
        {
            Id = a.Id;
            FullName = a.FullName;
            Email = a.Email;
            Password = a.Password;
            Active = a.Active;
        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "Nombre completo")]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [MaxLength(100)]
        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password of administrator")]
        public string Password { get; set; }

        [Display(Name = "Activo")]
        public bool Active { get; set; }

        public Admin ToEntity()
        {
            var entity = new Admin();
            entity.Id = Id;
            entity.FullName = FullName;
            entity.Email = Email;
            entity.Password = Password;
            entity.Active = Active;

            return entity;
        }
    
    }
}