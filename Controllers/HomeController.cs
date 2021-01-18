using starteAlkemy.Filters;
using starteAlkemy.Models;
using starteAlkemy.Models.ViewModels;
using starteAlkemy.Repository;
using starteAlkemy.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace starteAlkemy.Controllers
{
    #region
    public class HomeController : Controller
    {
        #region Atributes
        private readonly IHomeRepository _homeRepository;

        public static List<HomeImagesViewModel> imageCache = new List<HomeImagesViewModel>();
        public static string imagenFrontPartial;
        public static string imageOurMission;
        public static string imageLastProject;
        public static string imageDonate;
        public int maxImage = 6;

        #endregion

        #region Ctor
        public HomeController()
        {
            _homeRepository = new HomeRepository(new StartContext());
        }

        public HomeController(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;

        }


        #endregion


        #region Index
        public ActionResult Index()
        {
           var home = _homeRepository.Get();
            if (home != null)
            {
                var homeViewModel = new HomeViewModel(home);

                return View(homeViewModel);
            }
            else
            {
                HomeViewModel h = new HomeViewModel()
                {
                    Active = true,
                    Alias = "",
                    BackgroundDonate = "",
                    BackgroundOurMission = "",
                    BackgroundProject = "",
                    Cbu = "",
                    DescItemOneOurMission = "",
                    DescItemThreeOurMission = "",
                    DescItemTwoOurMission = "",
                    DescOurMission = "",
                    Id = 1,
                    ImageMain = "",

                };


                return View(h);
            }
        }

        #endregion



        #region Edit
        // GET: Home/Edit
       // [CustomAuthorize(true)]

        [HttpGet]
        public ActionResult Edit()
        {
            var model = _homeRepository.Get();
            var imageNotFound = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fwww.thermaxglobal.com%2Fwp-content%2Fuploads%2F2020%2F05%2Fimage-not-found.jpg&f=1&nofb=1";
            if (model != null)
            {
                if (model.ImageMain == null)
                {
                    model.ImageMain = imageNotFound;
                }
                if (model.BackgroundOurMission == null)
                {
                    model.BackgroundOurMission = imageNotFound;
                }
                if (model.BackgroundDonate == null)
                {
                    model.BackgroundDonate = imageNotFound;
                }
                if (model.BackgroundProject == null)
                {
                    model.BackgroundProject = imageNotFound;

                }
                imageCache.Clear();
                var modelViewModel = new HomeViewModel(model);
                imagenFrontPartial = modelViewModel.ImageMain;
                imageCache = modelViewModel.ListLinks;
                imageDonate = modelViewModel.BackgroundDonate;
                imageLastProject = modelViewModel.BackgroundProject;
                imageOurMission = modelViewModel.BackgroundOurMission;

                return View("Edit", modelViewModel);
            }
            else
            {
                var modelViewModel = new HomeViewModel();
                return View("Edit", modelViewModel);
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HomeViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException("El modelo no puede ser nulo.");

            if (CheckModel(model) == true)
            {
                if (ModelState.IsValid)
                {

                    model.BackgroundDonate = imageDonate;
                    model.BackgroundOurMission = imageOurMission;
                    model.BackgroundProject = imageLastProject;

                    model.ListLinks = imageCache;
                    model.ImageMain = imagenFrontPartial;
                    _homeRepository.Edit(model.ToEntity());
                    imageCache.Clear();
                }

            }
            else
            {
                throw new ArgumentException("El modelo no es valido.");
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion




        #region  Partial


        #region Front 
        public ActionResult PartialFrontImageGet()
        {
            var partialModel = new HomeViewModel();
            return PartialView("PartialFrontImage", partialModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PartialFrontImagesPost(HomeViewModel model)
        {


            if (model != null && model.ImageMain != null)
            {
                imagenFrontPartial = model.ImageMain;



            }
            else
            {

                if (imagenFrontPartial != null)
                {
                    model.ImageMain = imagenFrontPartial;

                }

            }
            return PartialView("PartialFrontImageView", model);

        }
        public PartialViewResult GetFrontImage()
        {
            var homeViewModel = new HomeViewModel(_homeRepository.Get());

            return PartialView("PartialFrontImageView", homeViewModel);

        }



        #endregion

        #region Donate
        [ChildActionOnly]
        [HttpGet]
        public ActionResult PartialBackgroundDonateImageGet()
        {
            var partialModel = new HomeViewModel();
            return PartialView("PartialBackgroundDonateImage", partialModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PartialBackgroundDonateImagesPost(HomeViewModel model)
        {

            if (model != null && model.BackgroundDonate != null)

            {

                imageDonate = model.BackgroundDonate;
                return PartialView("PartialBackgroundDonateImageView", model);

            }
            else
            {

                if (imageDonate != null)
                {
                    model.BackgroundDonate = imageDonate;
                }

                return PartialView("PartialBackgroundDonateImageView", model);
            }

        }
        public PartialViewResult GetBackgroundDonateImage()
        {
            var homeViewModel = new HomeViewModel(_homeRepository.Get());

            return PartialView("PartialBackgroundDonateImageView", homeViewModel);

        }

        #endregion

        #region ListImages
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PartialImagesPost(HomeImagesViewModel model)
        {
            Random random = new Random();

            if (imageCache.Count < maxImage && imageCache.Count >= 0 && model.LinkImage != null)
            {
                model.IdImage = random.Next();
                imageCache.Add(model);

            }
            else if (imageCache.Count > maxImage)
            {
                throw new ArgumentException("La lista supera la cantidad");

            }

            return PartialView("PartialLinksList", imageCache);
        }




        public PartialViewResult GetLinks()
        {
            return PartialView("PartialLinksList", imageCache);
        }
        [ChildActionOnly]
        [HttpGet]
        public ActionResult PartialImagesGet()
        {
            var partialModel = new HomeImagesViewModel();
            return PartialView("PartialArrayImages", partialModel);
        }
        public ActionResult QuitImage(HomeViewModel m)
        {

            var itemToDelete = imageCache.Find(x => x.IdImage == m.Id);
            imageCache.Remove(itemToDelete);

            return PartialView("PartialLinksList", imageCache);

        }


        #endregion

        #region OurMission
        [ChildActionOnly]
        [HttpGet]
        public ActionResult PartialBackgroundOurMissionImageGet()
        {
            var partialModel = new HomeViewModel();
            return PartialView("PartialBackgroundOurMissionImage", partialModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PartialBackgroundOurMissionImagesPost(HomeViewModel model)
        {
            var aux = _homeRepository.Get();
            if (model != null && aux != null && model.BackgroundOurMission != null)
            {
                aux.BackgroundOurMission = model.BackgroundOurMission;
                imageOurMission = model.BackgroundOurMission;

                return PartialView("PartialBackgroundOurMissionImageView", model);

            }
            else
            {
                if (imageOurMission != null)
                {
                    model.BackgroundOurMission = imageOurMission;
                }


                return PartialView("PartialBackgroundOurMissionImageView", model);
            }

        }
        public PartialViewResult GetBackgroundOurMissionImage()
        {
            var homeViewModel = new HomeViewModel(_homeRepository.Get());

            return PartialView("PartialBackgroundOurMissionImageView", homeViewModel);

        }

        #endregion

        #region Project
        [ChildActionOnly]
        [HttpGet]
        public ActionResult PartialBackgroundProjectImageGet()
        {
            var partialModel = new HomeViewModel();
            return PartialView("PartialBackgroundProjectImage", partialModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PartialBackgroundProjectImagesPost(HomeViewModel model)
        {

            if (model != null && model.BackgroundProject != null)
            {

                imageLastProject = model.BackgroundProject;
                return PartialView("PartialBackgroundProjectImageView", model);


            }
            else
            {
                if (imageLastProject != null)
                {
                    model.BackgroundProject = imageLastProject;
                }


                return PartialView("PartialBackgroundProjectImageView", model);
            }

        }
        public PartialViewResult GetBackgroundProjectImage()
        {
            var homeViewModel = new HomeViewModel(_homeRepository.Get());

            return PartialView("PartialBackgroundProjectImageView", homeViewModel);

        }

        #endregion
        #endregion

        private bool CheckModel(HomeViewModel model)
        {
            if (model.Alias != null || model.Cbu != null || model.DescItemOneOurMission != null ||
                model.DescItemThreeOurMission != null || model.DescItemTwoOurMission != null ||
                model.DescOurMission != null || model.NumeroDeCuenta != null || model.MercadoPagoId != null ||
                model.TitleItemOneOurMission != null || model.TitleItemTwoOurMission != null || model.TitleItemThreeOurMission != null ||
                model.TitleOurMission != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    #endregion
}