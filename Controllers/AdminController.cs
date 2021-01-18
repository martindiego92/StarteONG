using starteAlkemy.Filters;
using starteAlkemy.Models;
using starteAlkemy.Models.ViewModels;
using starteAlkemy.Repository.Implements;
using starteAlkemy.Repository.IRepository;
using System;
using System.Net;
using System.Web.Mvc;

namespace starteAlkemy.Controllers
{
    public class AdminController : Controller
    {
        #region Atributes
        private readonly IAdminRepository _adminRepository;
        #endregion

        #region Ctor
        public AdminController()
        {
            _adminRepository = new AdminRepository(new StartContext());
        }



        public AdminController(IAdminRepository homeRepository)
        {
            _adminRepository = homeRepository;
        }
        #endregion

        #region Login - Logout

        [HttpGet]
        public ActionResult Login()
        {
            Admin admin = _adminRepository.Get();
            if (admin != null)
            {
                var adminvm = new AdminViewModel(admin);
                return View(adminvm);
            }
            else
            {
                return RedirectToAction("Create", "Admin");
            }
        }

        [HttpPost]
        public ActionResult Login(AdminViewModel admin)
        {

            if (admin == null)
                throw new NullReferenceException("El modelo no puede ser nulo.");
            else
            {
                if (ModelState.IsValid)
                {
                    Admin adminAux = _adminRepository.Get(Encrypt.SHA256(admin.Password), admin.Email);
                    if (adminAux != null)
                    {
                        AdminViewModel adminViewModel = new AdminViewModel(adminAux);
                        Session["Email"] = adminViewModel.Email;
                        Session["Password"] = adminViewModel.Password;
                        return RedirectToAction("ListProjectToCRUD", "Project");
                    }
                    else
                    {
                        return RedirectToAction("Login", "Admin");
                    }
                }
                else
                {
                    return Content("Error");
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["Email"] = string.Empty;
            Session["Password"] = string.Empty;
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Details
        [CustomAuthorize(true)]

        public ActionResult Details(int id)
        {
            var home = new Admin();
            home = _adminRepository.Get();
            return View(new AdminViewModel(home));
        }
        #endregion

        #region Create

        // GET: Blog/Create
        /// <summary>
        /// Retorna la vista de la creacion de un nuevo post
        /// </summary>
        /// <returns></returns>
        //[CustomAuthorize(true)]
        public ActionResult Create()
        {

            return View();
        }

        // POST: Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CustomAuthorize(true)]

        public ActionResult Create(AdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Password = Encrypt.SHA256(model.Password);
                model.Active = true;
                _adminRepository.Add(model.ToEntity());
                Content("Se creo usuario");
                return RedirectToAction("Login");
            }
            return View();
        }
        #endregion

        #region Edit
        // GET: Home/Edit
        [CustomAuthorize(true)]

        [HttpGet]
        public ActionResult Edit()
        {
            var model = _adminRepository.Get();
            var modelViewModel = new AdminViewModel(model);
            return View(modelViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(true)]

        public ActionResult Edit(AdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                _adminRepository.Edit(model.ToEntity());
            }
            return RedirectToAction("Index", "Home");
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
            AdminViewModel adminViewModel = new AdminViewModel(_adminRepository.Get());
            if (adminViewModel == null)
            {
                return HttpNotFound();
            }
            return View(adminViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            _adminRepository.Delete(id);
            return RedirectToAction("Index", "Home");
        }
        #endregion

    }
}