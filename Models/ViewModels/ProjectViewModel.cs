using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace starteAlkemy.Models.ViewModels
{
    public class ProjectViewModel
    {
        #region Ctor
        public ProjectViewModel()
        {
            var list = new List<LinksMmViewModel>();
        }

        public ProjectViewModel(Project p)
        {
            Id = p.Id;
            TitleProject = p.Title;
            DescProject = p.Description;
            ContentProject = p.Content;
            ProjectAddedDate = p.DateProject;
            CategoryId = p.CategoryId;
            AdminId = p.AdminId;
            Active = p.Active;
            LinksMms = p.ListLinkProject.Select(l => new LinksMmViewModel(l)).ToList();
        }

        #endregion

        #region Getters/Setters

        [Key]
        public int Id { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "El título es obligatorio")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Debe tener al menos 3 caracteres")]
        [StringLength(23, MinimumLength = 3, ErrorMessage = "La cantidad máxima de caracteres son 23")]
        public string TitleProject { get; set; }

        [MaxLength(int.MaxValue, ErrorMessage = "Supero la cantidad máxima de caracteres")]
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string DescProject { get; set; }

        [MaxLength(int.MaxValue, ErrorMessage = "Supero la cantidad máxima de caracteres")]
        [Display(Name = "Contenido")]
        [DataType(DataType.MultilineText)]

        [Required(ErrorMessage = "El contenido es obligatorio")]
        public string ContentProject { get; set; }

        [Display(Name = "Fecha de creación")]

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ProjectAddedDate { get; set; }

        public int IdHome { get; set; }
        public List<LinksMmViewModel> LinksMms { get; set; }
        public LinksMmViewModel link { get; set; }
        public bool Active { get; set; }
        public int CategoryId { get; set; }
        public int AdminId { get; set; }

        #endregion

        #region Methods

        public List<ProjectViewModel> ToListEntity(List<Project> listProject)
        {
            List<ProjectViewModel> modelList = new List<ProjectViewModel>();
            foreach (var item in listProject)
            {
                var model = new ProjectViewModel();
                model.Id = item.Id;
                model.TitleProject = item.Title;
                model.DescProject = item.Description;
                model.ContentProject = item.Content;
                model.ProjectAddedDate = item.DateProject;
                model.CategoryId = item.CategoryId;
                model.AdminId = item.AdminId;
                model.Active = item.Active;
                if (item.ListLinkProject != null)
                {
                    model.LinksMms = item.ListLinkProject.Select(h => new LinksMmViewModel
                    {
                        Id = h.Id,
                        Active = h.Active,
                        ProjectId = model.Id,
                        LinkText = h.Text,
                        LinkTitle = h.Title,
                        LinkType = h.Type,
                        LinkUrl = h.Url,
                        UserId = h.UserId
                    }).ToList();
                }
                modelList.Add(model);
            }
            return modelList;
        }

        public Project ToEntity()
        {
            Project p = new Project();
            p.Id = Id;
            p.Title = TitleProject;
            p.Description = DescProject;
            p.Content = ContentProject;
            p.DateProject = ProjectAddedDate;
            if (LinksMms != null)
            {
                p.ListLinkProject = LinksMms.Select(h => new LinksMm
                {
                    Id = h.Id,
                    Active = h.Active,
                    ProjectId = p.Id,
                    Text = h.LinkText,
                    Title = h.LinkTitle,
                    Type = h.LinkType,
                    Url = h.LinkUrl,
                    UserId = h.UserId
                }).ToList();
            }
            return p;
        }

        #endregion
    }
}