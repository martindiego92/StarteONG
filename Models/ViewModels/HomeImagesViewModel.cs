using System.ComponentModel.DataAnnotations;

namespace starteAlkemy.Models.ViewModels
{
    public class HomeImagesViewModel
    {
        #region Ctor
        public HomeImagesViewModel(){ }

        public HomeImagesViewModel(HomeImages h)
        {
            IdImage = h.Id;
            LinkImage = h.Link;
            Text = h.Text;
            Active = h.Active;
        }
        #endregion

        #region Getter and Setter

        [Key]
        public int IdImage { get; set; }

        [Display(Name = "Link de la imagen")]
        [DataType(DataType.Html)]
        public string LinkImage { get; set; }

        [MaxLength(int.MaxValue, ErrorMessage = "Supero la cantidad máxima de caracteres")]
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Text { get; set; }

        public string LinkVideo { get; set; }
        [Display(Name = "Activo")]

        public bool Active { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Set Viewmodel to Db
        /// </summary>
        /// <returns></returns>
        public HomeImages ToEntity()
        {
            var d = new HomeImages()
            {
                Id = IdImage,
                Active = Active,
                Link = LinkImage,
                Text = Text
            };
            return d;
        }
     
       
        #endregion
    }
}