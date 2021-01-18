using System.ComponentModel.DataAnnotations;

namespace starteAlkemy.Models.ViewModels
{
    public class CategoryViewModel
    {
        public CategoryViewModel(){}

        public CategoryViewModel(Category C)
        {
            Id = C.Id;
            Title = C.Title;
        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "El título es obligatorio")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Debe tener al menos 3 caracteres")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "Cantidad de caracteres inválida")]
        public string Title { get; set; }

        public Category ToEntity()
        {
            Category entity = new Category();
            entity.Id = Id;
            entity.Title = Title;

            return entity;
        }
    }
}