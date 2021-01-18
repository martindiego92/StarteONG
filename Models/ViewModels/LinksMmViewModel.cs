using System.ComponentModel.DataAnnotations;

namespace starteAlkemy.Models.ViewModels
{
    public class LinksMmViewModel
    {
        #region Ctor
        public LinksMmViewModel() { }

        public LinksMmViewModel(LinksMm l)
        {
            Id = l.Id;
            LinkUrl = l.Url;
            LinkTitle = l.Title;
            LinkType = l.Type;
            LinkText = l.Text;
            ProjectId = l.ProjectId;
            UserId = l.UserId;
            Active = l.Active;
        }

        public LinksMmViewModel(string LinkTitle, string LinkUrl, string LinkText)
        {
            this.LinkTitle = LinkTitle;
            this.LinkUrl = LinkUrl;
            this.LinkText = LinkText;
        }
        #endregion

        #region Getter and Setter

        [Key]
        public int Id { get; set; }

        [Display(Name = "Título")]
        // [Required(ErrorMessage = "El título es obligatorio")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Debe tener al menos 3 caracteres")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "Cantidad de caracteres inválida")]
        public string LinkTitle { get; set; }

        [Display(Name = "Link de la imagen")]
        [DataType(DataType.Html)]
        public string LinkUrl { get; set; }

        [MaxLength(int.MaxValue, ErrorMessage = "Supero la cantidad máxima de caracteres")]
        [Display(Name = "Descripción")]
        //[Required(ErrorMessage = "La descripción es obligatoria")]
        public string LinkText { get; set; }

        [Display(Name = "Activo")]
        public bool Active { get; set; }

        public short LinkType { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }

        #endregion

        #region Methods
        public LinksMm ToEntity()
        {
            LinksMm entitymm = new LinksMm()
            {
                Id = Id,
                Url = LinkUrl,
                Text = LinkText,
                Title = LinkTitle,
                ProjectId = ProjectId,
                Type = LinkType,
                Active = Active,
                UserId = UserId
            };

            return entitymm;
        }
        #endregion

    }
}