using Microsoft.VisualStudio.TestTools.UnitTesting;
using starteAlkemy.Controllers;
using starteAlkemy.Models;
using starteAlkemy.Models.ViewModels;
using starteAlkemy.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Starte.UnitTest
{
    [TestClass]
    public class TestControllerProject
    {



        [TestMethod]
        public void TestValidProjectDelete()

        {

            ProjectController _Controller = new ProjectController();
            ProjectViewModel model = new ProjectViewModel();
            model.Id = 1;
            model.TitleProject = "Projecto1";
            model.ContentProject = "Contenido 1";
            model.DescProject = "descripcion";
            model.ProjectAddedDate = DateTime.Now;
            model.Active = true;

            ViewResult view = (ViewResult)_Controller.ConfirmDelete(model.Id);

            Assert.IsNotNull(view);
            Assert.AreEqual(model.TitleProject, "Project1");
        }



        [TestMethod]
        public void TestInvalidProjectDelete()
        {
            ProjectController _Controller = new ProjectController();
            ProjectViewModel model = new ProjectViewModel();
            model.Id = 100;
            model.TitleProject = "Projecto100";
            model.ContentProject = "Contenido 100";
            model.DescProject = "descripcion100";
            model.ProjectAddedDate = DateTime.Now;
            model.Active = true;

            ViewResult view = (ViewResult)_Controller.ConfirmDelete(model.Id);


            Assert.AreEqual(model.Id, "100");
        }





        [TestMethod]
        public void TestDetails()
        {
            int id = 2;
            Boolean bTestSuccess = false;
            string sMessage = "";


            ProjectController Controller = new ProjectController();

            ActionResult result = Controller.Details(id);


            var actResult = (RedirectResult)result;
            id = 0;

            // User Added successfully
            if (actResult.Url.StartsWith("/Details?id="))
            {
                // We remove the URL and pull the ID for the new user we just created
                string sURL = actResult.Url.Replace("/Details?id=", "");

                id = Convert.ToInt32(sURL);

                // If the user ID not 0
                if (id != 0)
                {
                    bTestSuccess = true;
                    sMessage = "Project Details ok";
                }
                else
                {
                    // If UserId is 0 something went wrong and the User didn't save ok, 
                    // So the test fails.
                    bTestSuccess = false;
                    sMessage = "Project Details failed";
                }
            }
            else
            {
                // If the result URL is Save user
                string sURL = actResult.Url.Replace("/Details?message=", "");



                // The test has passed if this code is hit.
                bTestSuccess = true;
                sMessage = "";
            }


            // Test fails if bTestSuccess is false;
            Assert.IsTrue(bTestSuccess, sMessage);

        }



        [TestMethod]
        public void TestGetAll()
        {
           

         

        }



        [TestMethod]
        public void IsValidModelCreateProject()
        {
            //arrange
            ProjectController projectController = new ProjectController();
            ProjectViewModel model = new ProjectViewModel();
            //Act
            ViewResult result = projectController.Create(model) as ViewResult;

            //Assert
            Assert.IsNull(result);
        }






            [TestMethod]
            public void DetallesArrojaUnError()
            {
                // Preparación
                Exception expectedException = null;
                ProjectController projectController = new ProjectController();
                ProjectViewModel model = new ProjectViewModel();
               
             

                // Prueba
                try
                {
                    projectController.ListProjectToCRUD();
                    
                }
                catch (Exception ex)
                {
                    expectedException = ex;
                    Assert.Fail("Un error debió ser arrojado");
                }

                // Verificación
               // Assert.IsTrue(expectedException is ApplicationException);
                Assert.IsNotNull(expectedException.Message);
            }



        [TestMethod]
       
        public void LoadProjectPartialGet()
        {
  
           ProjectController projectController = new ProjectController();
            ViewResult view = (ViewResult)projectController.PartialImagesGet();
            Assert.AreEqual(view,"PartialImagesGet" );
        }

        public void LoadProjectPartialPost()
        {
            ProjectViewModel model = new ProjectViewModel();
            ProjectController projectController = new ProjectController();
            ViewResult view = (ViewResult)projectController.PartialImagesPost(model.link);
            
            Assert.IsNotNull(view);
        
        }




        [TestMethod]
        public void DeleteProjectTest()
        {
            int id = 2;
            Boolean bTestSuccess = false;
            string sMessage = "";


            ProjectController Controller = new ProjectController();

            ActionResult result = Controller.Delete(id);


            var actResult = (RedirectResult)result;
            id = 0;

            // User Added successfully
            if (actResult.Url.StartsWith("/Delete?id="))
            {
                // We remove the URL and pull the ID for the new user we just created
                string sURL = actResult.Url.Replace("/Delete?id=", "");

                id = Convert.ToInt32(sURL);

                // If the user ID not 0
                if (id != 0)
                {
                    bTestSuccess = true;
                    sMessage = "Project Delete ok";
                }
                else
                {
                    // If UserId is 0 something went wrong and the User didn't save ok, 
                    // So the test fails.
                    bTestSuccess = false;
                    sMessage = "Project Delete failed";
                }
            }
            else
            {
                // If the result URL is Save user
                string sURL = actResult.Url.Replace("/Delete?message=", "");



                // The test has passed if this code is hit.
                bTestSuccess = true;
                sMessage = "";
            }


            // Test fails if bTestSuccess is false;
            Assert.IsTrue(bTestSuccess, sMessage);

        }

    }


   

}


  
