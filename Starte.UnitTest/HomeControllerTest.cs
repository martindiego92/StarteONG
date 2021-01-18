using Microsoft.VisualStudio.TestTools.UnitTesting;
using starteAlkemy.Controllers;
using starteAlkemy.Models;
using starteAlkemy.Models.ViewModels;
using starteAlkemy.Repository.IRepository;
using System;
using System.Web.Mvc;

namespace Starte.UnitTest
{
    [TestClass]
    public class HomeControllerTest
    {
        #region Index
        [TestMethod]
        public void TestIndex()
        {
            HomeController home = new HomeController();
            ViewResult viewResult = home.Index() as ViewResult;
            Assert.IsNotNull(viewResult);
        }
        
        #endregion

        #region EditTest
        [TestMethod]

        [ExpectedException(typeof(ArgumentNullException), "El modelo no puede ser nulo.")]
        public void TestEditHomeWhitNullParameter_shouldThrowException()
        {
            HomeController homeController = new HomeController();
            ViewResult viewResult = homeController.Edit(null) as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "El modelo no es valido.")]
        public void TestEditHomeModelInvald_shouldThrowException()
        {
            HomeController homeController = new HomeController();
            HomeViewModel model = new HomeViewModel();
          
            ViewResult viewResult = homeController.Edit(model) as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [TestMethod]
        public void TestEditHome()
        {
            HomeController homeController = new HomeController();
            
            HomeViewModel model = new HomeViewModel();
            model.Alias = "null";
            model.Cbu = "null";
            model.DescItemOneOurMission = "null";
            model.DescItemThreeOurMission = "null";
            model.DescItemTwoOurMission = "null";
            model.DescOurMission = "null";
            model.NumeroDeCuenta = "null";
            model.MercadoPagoId = "null";
            model.TitleItemOneOurMission = "null";
            model.TitleItemTwoOurMission = "null";
            model.TitleItemThreeOurMission = "null";
            model.TitleOurMission = "null";
            var result = (RedirectToRouteResult)homeController.Edit(model);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

       
        #endregion

        #region PartialTests
        [TestMethod]

        public void PartialListImageRedirection()
        {
            var controller = new HomeController();
            var model = new HomeImagesViewModel();

            var result = controller.PartialImagesPost(model) as PartialViewResult;
            Assert.AreEqual("PartialLinksList", result.ViewName);
        }

      
        [TestMethod]
        public void PartialBackGroundDonateImageRedirection()
        {
            var controller = new HomeController();
            var model = new HomeViewModel();
            model.BackgroundDonate = "sasda";
            var result = controller.PartialBackgroundDonateImagesPost(model) as PartialViewResult;
            Assert.AreEqual("PartialBackgroundDonateImageView", result.ViewName);
        }
        [TestMethod]
        public void PartialBackgroundOurMissionRedirection()
        {
            var controller = new HomeController();
            var model = new HomeViewModel();
            model.BackgroundOurMission = "adasd";
            var result = controller.PartialBackgroundOurMissionImagesPost(model) as PartialViewResult;
            Assert.AreEqual("PartialBackgroundOurMissionImage", result.ViewName);
        }
        [TestMethod]
        public void PartialBackgroundProjectImageRedirection()
        {
            var controller = new HomeController();
            var model = new HomeViewModel();

            var result = controller.PartialBackgroundProjectImagesPost(model) as PartialViewResult;
            Assert.AreEqual("PartialBackgroundProjectImageView", result.ViewName);
        }
        #endregion
        #region UpdateRepoTest
        [TestMethod]

        public void EditHomeTest()
        {

            var repository = new HomeRepository();
            var homeVM = new HomeViewModel();
            homeVM.Id = 1;
            homeVM.Alias = "null";
            homeVM.Cbu = "null";
            homeVM.DescItemOneOurMission = "null";
            homeVM.DescItemThreeOurMission = "null";
            homeVM.DescItemTwoOurMission = "null";
            homeVM.DescOurMission = "null";
            homeVM.NumeroDeCuenta = "null";
            homeVM.MercadoPagoId = "null";
            homeVM.TitleItemOneOurMission = "null";
            homeVM.TitleItemTwoOurMission = "null";
            homeVM.TitleItemThreeOurMission = "null";
            homeVM.TitleOurMission = "null";

            repository.Edit(homeVM.ToEntity());

            var updated = repository.Get();
            Assert.IsNotNull(updated);
            Assert.AreEqual(updated.Alias, "null");
            Assert.AreEqual(updated.Cbu, "null");
            Assert.AreEqual(updated.DescItemOneOurMission, "null");
            Assert.AreEqual(updated.DescItemThreeOurMission, "null");
            Assert.AreEqual(updated.DescItemTwoOurMission, "null");
            Assert.AreEqual(updated.DescOurMission, "null");
            Assert.AreEqual(updated.NumeroDeCuenta, "null");
            Assert.AreEqual(updated.MercadoPagoId, "null");
            Assert.AreEqual(updated.TitleItemOneOurMission, "null");
            Assert.AreEqual(updated.TitleItemTwoOurMission, "null");
            Assert.AreEqual(updated.TitleItemThreeOurMission, "null");
            Assert.AreEqual(updated.TitleOurMission, "null");
        }
        #endregion


    }
}
