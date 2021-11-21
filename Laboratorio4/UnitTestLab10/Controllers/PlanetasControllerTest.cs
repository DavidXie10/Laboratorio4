using Microsoft.VisualStudio.TestTools.UnitTesting;
using Laboratorio4.Controllers;
using System.Web.Mvc;

namespace UnitTestLab10.Controllers {
    [TestClass]
    public class PlanetasControllerTest {
        [TestMethod]
        public void TestCrearPlanetaViewResultNotNull() {
            // Arrange
            PlanetasController planetasController = new PlanetasController();

            // Act 
            ActionResult vista = planetasController.crearPlaneta();
            // Assert 
            Assert.IsNotNull(vista);
        }

        [TestMethod]
        public void TestCrearPlanetaViewResult() {
            // Arrange
            PlanetasController planetasController = new PlanetasController();

            // Act
            ViewResult vista = planetasController.crearPlaneta() as ViewResult;

            // Assert
            Assert.AreEqual("crearPlaneta", vista.ViewName);
        }
    }
}
