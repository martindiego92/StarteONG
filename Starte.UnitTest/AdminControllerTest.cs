using Microsoft.VisualStudio.TestTools.UnitTesting;
using starteAlkemy.Controllers;
using starteAlkemy.Models.ViewModels;
using starteAlkemy.Repository.Implements;
using starteAlkemy.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Starte.UnitTest
{
    /// <summary>
    /// Descripción resumida de AdminControllerTest
    /// </summary>
    [TestClass]
    public class AdminControllerTest
    {
        public AdminControllerTest()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Atributos de prueba adicionales
        //
        // Puede usar los siguientes atributos adicionales conforme escribe las pruebas:
        //
        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "El modelo no puede ser nulo.")]
        public void TestLoginAdminWhitNullParameter_shouldThrowException()
        {
            AdminController adminController = new AdminController();
            ViewResult viewResult = adminController.Login(null) as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [TestMethod]
        public void TestLoginAdminGetEmptyRedirection()
        {
            AdminController adminController = new AdminController();
            var result = (RedirectToRouteResult)adminController.Login();
            Assert.AreEqual("Create", result.RouteValues["action"]);
        }

        [TestMethod]
        public void TestLoginAdminNotFoundInTheDatabaseRedirection()
        {
            AdminController adminController = new AdminController();
            AdminViewModel adminViewModel = new AdminViewModel()
            {
                Active = true,
                Email = "gg",
                FullName = "gg",
                Password = "gg",
                Id = 1
            };
            var result = (RedirectToRouteResult)adminController.Login(adminViewModel);
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [TestMethod]
        public void TestLoginAdmiFoundInTheDatabaseRedirection()
        {
            AdminController adminController = new AdminController();
            AdminViewModel adminViewModel = new AdminViewModel()
            {
                Active = true,
                Email = "gg",
                FullName = "gg",
                Password = "gg",
                Id = 1
            };
            var result = (RedirectToRouteResult)adminController.Login(adminViewModel);
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }
        [TestMethod]
        public void TestLoginInDataBase()
        {
            var repository = new AdminRepository();
            var adminVm = new AdminViewModel()
            {
                Active = true,
                Email = "gg",
                FullName = "gg",
                Password = "gg",
                Id = 1
            };

            AdminController adminController = new AdminController();
            adminController.Create(adminVm);

            var updated = repository.Get();
            Assert.IsNotNull(updated);
            Assert.AreEqual("gg", updated.Email);
            Assert.AreEqual("gg", updated.FullName);


        }
    }
}
