using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace starteAlkemy.Models.ViewModels
{
    public class HomeViewModel
    {
        #region Ctor
        public HomeViewModel()
        {

            ListLinks = new List<HomeImagesViewModel>();
            ListProjects = new List<ProjectViewModel>();
            LinksMm = new List<LinksMmViewModel>();


        }

        public HomeViewModel(Home h)
        {
            if (h != null)
            {
                Id = h.Id;

                ListLinks = h.ListImage.Select(l => new HomeImagesViewModel(l)).ToList();
                MercadoPagoId = h.MercadoPagoId;
                WelcomeText = h.WelcomeText;
                ListProjects = h.ListProject.Select(l => new ProjectViewModel(l)).ToList();
                ImageMain = h.ImageMain;
                Cbu = h.Cbu;
                Alias = h.Alias;
                NumeroDeCuenta = h.NumeroDeCuenta;
                BackgroundOurMission = h.BackgroundOurMission;
                BackgroundDonate = h.BackgroundDonate;
                BackgroundProject = h.BackgroundProject;

                TitleOurMission = h.TitleOurMission;
                DescOurMission = h.DescOurMission;

                TitleItemOneOurMission = h.TitleItemOneOurMission;
                TitleItemTwoOurMission = h.TitleItemTwoOurMission;
                TitleItemThreeOurMission = h.TitleItemThreeOurMission;

                DescItemOneOurMission = h.DescItemOneOurMission;
                DescItemTwoOurMission = h.DescItemTwoOurMission;
                DescItemThreeOurMission = h.DescItemThreeOurMission;
    }


        }

        #endregion

        #region Getter and Setter

        [Key]
        public int Id { get; set; }

        [Display(Name = "Texto de bienvenida")]
        [Required(ErrorMessage = "El texto es obligatorio")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Debe tener al menos 3 caracteres")]
        [StringLength(maximumLength:23, MinimumLength = 3, ErrorMessage = "Cantidad de caracteres inválida")]
        public string WelcomeText { get; set; }

        [Display(Name = "ID de Mercadopago")]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string MercadoPagoId { get; set; }

        [Display(Name = "CBU")]
        public string Cbu { get; set; }
        [Display(Name = "Alias")]
        public string Alias { get; set; }
        [Display(Name = "Numero de Cuenta")]
        public string NumeroDeCuenta { get; set; }

        public List<HomeImagesViewModel> ListLinks { get; set; }
        public List<ProjectViewModel> ListProjects { get; set; }
        public List<LinksMmViewModel> LinksMm { get; set; }

        [Display(Name = "Imagen Principal")]
        public string ImageMain { get; set; }
        [Display(Name = "Fondo de Nuestra Mision")]
        public string BackgroundOurMission { get; set; }
        [Display(Name = "Fondo de Donaciones")]
        public string BackgroundDonate { get; set; }
        [Display(Name = "Fondo de Ultimos Proyectos")]
        public string BackgroundProject { get; set; }
        [Display(Name = "Titulo Principal de Nuestra Mision")]
        [Required(ErrorMessage = "El texto es obligatorio")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Debe tener al menos 3 caracteres")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Cantidad de caracteres inválida")]
        public string TitleOurMission { get; set; }
        [Display(Name = "Descripcion Principal de Nuestra Mision")]
        [Required(ErrorMessage = "El texto es obligatorio")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Debe tener al menos 3 caracteres")]
        [StringLength(maximumLength: 300, MinimumLength = 3, ErrorMessage = "Cantidad de caracteres inválida")]
        public string DescOurMission { get; set; }
        [Display(Name = "Titulo item 1 de Nuestra Mision")]
        [Required(ErrorMessage = "El texto es obligatorio")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Debe tener al menos 3 caracteres")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Cantidad de caracteres inválida")]
        public string TitleItemOneOurMission { get; set; }
        [Display(Name = "Titulo item 2 de Nuestra Mision")]
        [Required(ErrorMessage = "El texto es obligatorio")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Debe tener al menos 3 caracteres")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Cantidad de caracteres inválida")]
        public string TitleItemTwoOurMission { get; set; }
        [Display(Name = "Titulo item 3 de Nuestra Mision")]
        [Required(ErrorMessage = "El texto es obligatorio")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Debe tener al menos 3 caracteres")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Cantidad de caracteres inválida")]
        public string TitleItemThreeOurMission { get; set; }
        [Display(Name = "Descripcion item 1 de Nuestra Mision")]
        [Required(ErrorMessage = "El texto es obligatorio")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Debe tener al menos 3 caracteres")]
        [StringLength(maximumLength: 300, MinimumLength = 3, ErrorMessage = "Cantidad de caracteres inválida")]
        public string DescItemOneOurMission { get; set; }
        [Display(Name = "Descripcion item 2 de Nuestra Mision")]
        [Required(ErrorMessage = "El texto es obligatorio")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Debe tener al menos 3 caracteres")]
        [StringLength(maximumLength: 300, MinimumLength = 3, ErrorMessage = "Cantidad de caracteres inválida")]
        public string DescItemTwoOurMission { get; set; }
        [Display(Name = "Descripcion item 3 de Nuestra Mision")]
        [Required(ErrorMessage = "El texto es obligatorio")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Debe tener al menos 3 caracteres")]
        [StringLength(maximumLength: 300, MinimumLength = 3, ErrorMessage = "Cantidad de caracteres inválida")]
        public string DescItemThreeOurMission { get; set; }
        public bool Active { get; set; }

        #endregion

        #region Methods
        public Home ToEntity()
        {
            Home d = new Home()
            {
                Id = Id,
                ListImage = ListLinks.Select(h => new HomeImages { Active = h.Active, Id = h.IdImage, Link = h.LinkImage, Text = h.Text }).ToList(),
                MercadoPagoId = MercadoPagoId,
                WelcomeText = WelcomeText,
                ImageMain = ImageMain,
                NumeroDeCuenta = NumeroDeCuenta,
                Alias = Alias,
                Cbu = Cbu,
                BackgroundDonate = BackgroundDonate,
                BackgroundOurMission = BackgroundOurMission,
                BackgroundProject = BackgroundProject,
                DescItemOneOurMission = DescItemOneOurMission,
                DescItemTwoOurMission = DescItemTwoOurMission,
                DescItemThreeOurMission = DescItemThreeOurMission,
                TitleItemOneOurMission = TitleItemOneOurMission,
                TitleItemTwoOurMission = TitleItemTwoOurMission,
                TitleItemThreeOurMission = TitleItemThreeOurMission,
                DescOurMission = DescOurMission,
                TitleOurMission = TitleOurMission

            };
            return d;
        }
        #endregion

    }
}