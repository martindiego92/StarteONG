using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace starteAlkemy.Models
{
    public class Home
    {
        [Key]
        public int Id { get; set; }
        public List<Project> ListProject { get; set; }
        public List<HomeImages> ListImage { get; set; }
        public string WelcomeText { get; set; }
        public string MercadoPagoId { get; set; }
        public string Cbu { get; set; }
        public string Alias { get; set; }
        public string NumeroDeCuenta { get; set; }
        public string ImageMain { get; set; }

        public string BackgroundOurMission { get; set; }
        public string BackgroundDonate { get; set; }
        public string BackgroundProject { get; set; }
        public string TitleOurMission { get; set; }
        public string DescOurMission { get; set; }

        public string TitleItemOneOurMission { get; set; }
        public string TitleItemTwoOurMission { get; set; }
        public string TitleItemThreeOurMission { get; set; }

        public string DescItemOneOurMission { get; set; }
        public string DescItemTwoOurMission { get; set; }
        public string DescItemThreeOurMission { get; set; }

    }
}