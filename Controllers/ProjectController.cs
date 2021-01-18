using starteAlkemy.Models;
using System.Web.Mvc;
using starteAlkemy.Repository.IRepository;
using starteAlkemy.Models.ViewModels;
using System.Net;
using starteAlkemy.Repository;
using starteAlkemy.Filters;
using System.Collections.Generic;
using System;

namespace starteAlkemy.Controllers
{
    public class ProjectController : Controller
    {
        #region Atributes
        private readonly IProjectRepository _projectRepository;
        private static List<LinksMmViewModel> imagesCreateList = new List<LinksMmViewModel>();


        public int maxImage = 6;

        #endregion

        #region Ctor
        public ProjectController()
        {
            _projectRepository = new ProjectRepository(new StartContext());


        }
        #endregion

        #region Index
        //GET: Project
        [AllowAnonymous]
        public ActionResult Index()
        {
            var projectViewModel = new ProjectViewModel();

            List<ProjectViewModel> projects = projectViewModel.ToListEntity(_projectRepository.GetListProject());

            return View("Index", projects);
        }

        #endregion

        #region Details

        //GET : Project/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var projectviewmodel = new ProjectViewModel(_projectRepository.Get(id));
            return View("Details", projectviewmodel);
        }

        #endregion

        #region Create  


        [CustomAuthorize(true)]
        public ActionResult Create()
        {
            var model = new ProjectViewModel();
            model.ProjectAddedDate = DateTime.Now;
            imagesCreateList.Clear();
            return View("Create", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectViewModel model)
        {

            if (ModelState.IsValid)
            {
                if (imagesCreateList.Count > 0)
                {
                    model.LinksMms = imagesCreateList;
                    model.Active = true;
                }
                else
                {
                    var lvm = new LinksMmViewModel
                    {
                        LinkUrl = "https://farm5.staticflickr.com/4363/36346283311_74018f6e7d_o.png"
                    };
                    imagesCreateList.Add(lvm);
                    model.LinksMms = imagesCreateList;
                }
                _projectRepository.Add(model.ToEntity());
                imagesCreateList.Clear();
                ViewBag.Message = "Proyecto creado con exito! ";


            }
            else
            {

            }

            return RedirectToAction("ListProjectToCRUD", "Project");
        }

        #endregion

        #region Edit


        // GET: Home/Edit
        [HttpGet]
        [CustomAuthorize(true)]
        public ActionResult Edit(int id)
        {

            var model = new ProjectViewModel(_projectRepository.Get(id));
            if (model != null)
            {
                imagesCreateList.Clear();
                imagesCreateList = model.LinksMms;
                return View("Edit", model);
            }
            else
            {
                return ViewBag.Message = "Error, el proyecto a editar no existe ";

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(ProjectViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException("El modelo no puede ser nulo.");

            //TODO: ver como valido los data anotations del proyectViewModel
            if (string.IsNullOrWhiteSpace(model.TitleProject))
            {
                throw new ArgumentException("El modelo no es valido.");
            }


            if (ModelState.IsValid)
            {
                 model.LinksMms = imagesCreateList;
                _projectRepository.Edit(model.ToEntity());
                imagesCreateList.Clear();
            }
            else
            {
                throw new ArgumentException("El modelo no es valido.");
            }



            return RedirectToAction("ListProjectToCRUD");
        }

        #endregion

        #region Delete

        [HttpGet]
        [CustomAuthorize(true)]

        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }
            ProjectViewModel model = new ProjectViewModel(_projectRepository.Get(id));
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [CustomAuthorize(true)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            _projectRepository.Delete(id);
            _projectRepository.Save();
            return RedirectToAction("ListProjectToCrud", "Project");
        }

        #endregion

        #region ListProject
        [CustomAuthorize(true)]

        public ActionResult ListProjectToCRUD()
        {
            var projectViewModel = new ProjectViewModel();
            return View(projectViewModel.ToListEntity(_projectRepository.GetListProject()));
        }

        #endregion

        #region  Partial

        [ChildActionOnly]
        [HttpGet]
        public ActionResult PartialImagesGet()
        {
            var partialModel = new LinksMmViewModel();
            return PartialView("PartialArrayImages", partialModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PartialImagesPost(LinksMmViewModel model)
        {
            Random random = new Random();
            if (imagesCreateList.Count < maxImage && imagesCreateList.Count >= 0 && model.LinkUrl != null)
            {
                model.Id = random.Next();
                imagesCreateList.Add(model);
            }
            return PartialView("PartialLinksList", imagesCreateList);
        }

        public ActionResult QuitImage(ProjectViewModel m)
        {

            var itemToDelete = imagesCreateList.Find(x => x.Id == m.Id);
            imagesCreateList.Remove(itemToDelete);
            return PartialView("PartialLinksList", imagesCreateList);
        }

        public PartialViewResult GetLinks()
        {
            return PartialView("PartialLinksList", imagesCreateList);
        }

        #endregion



    }
}