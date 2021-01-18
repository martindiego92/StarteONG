using Microsoft.VisualStudio.TestTools.UnitTesting;
using starteAlkemy.Controllers;
using starteAlkemy.Models.ViewModels;
using System;
using System.Web.Mvc;


namespace Starte.UnitTest
{
    [TestClass]
    public class ExamplesPablo
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "El modelo no puede ser nulo.")]
        public void TestEditProjectWhitNullParameter_shouldThrowException()
        {
            ProjectController projectController = new ProjectController();
            ViewResult viewResult = projectController.Edit(null) as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "El modelo no es valido.")]
        public void TestEditProjectModelInvald_shouldThrowException()
        {
            ProjectController projectController = new ProjectController();
            ProjectViewModel model = new ProjectViewModel();
            ViewResult viewResult = projectController.Edit(model) as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [TestMethod]
        public void TestEditProject()
        {
            ProjectController projectController = new ProjectController();
            ProjectViewModel model = new ProjectViewModel();
            model.TitleProject = "dfsafdsafds";

            var result = (RedirectToRouteResult)projectController.Edit(model);

            Assert.AreEqual("ListProjectToCRUD", result.RouteValues["action"]);


        }

        [TestMethod]
        public void TestIndexViewRedirection()
        {
            //testeo que si mañana cambio la view, me explote este test
            var controller = new ProjectController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }


    }
}
