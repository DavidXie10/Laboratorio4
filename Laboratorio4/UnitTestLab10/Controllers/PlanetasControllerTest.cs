using Microsoft.VisualStudio.TestTools.UnitTesting;
using Laboratorio4.Controllers;
using System.Web.Mvc;
using Laboratorio4.Models;

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

        [TestMethod]
        public void EditarPlanetaIdValidoVistaNoNula() {
            // Arrange
            int id = 1;
            PlanetasController planetasController = new PlanetasController();

            // Act
            ViewResult vista = planetasController.editarPlaneta(id) as ViewResult;

            // Assert
            Assert.IsNotNull(vista);
        }

        [TestMethod]
        public void EditarPlanetaValidoModeloRetornadoNoEsNulo() {
            // Arrange
            int id = 1;
            PlanetasController planetasController = new PlanetasController();

            // Act
            ViewResult vista = planetasController.editarPlaneta(id) as ViewResult;

            // Assert
            Assert.IsNotNull(vista.Model);
        }

        [TestMethod]
        public void EditarPlanetaConIdNoExistenteRedirectToLP() {
            // Arrange
            int idInvalido = -1;
            PlanetasController planetasController = new PlanetasController();

            // Act
            RedirectToRouteResult vista = planetasController.editarPlaneta(idInvalido) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("listadoDePlanetas", vista.RouteValues["action"]);
        }

        [TestMethod]
        public void EditarPlanetaElModeloEnviadoEsCorrecto() {
            // Arrange
            int id = 1;
            PlanetasController planetasController = new PlanetasController();

            // Act
            ViewResult vista = planetasController.editarPlaneta(id) as ViewResult;
            PlanetaModel planeta = vista.Model as PlanetaModel;

            // Assert
            Assert.IsNotNull(planeta);
            Assert.AreEqual(6, planeta.numeroAnillos);
            Assert.AreEqual("Tierra", planeta.nombre);
        }

        [TestMethod]
        public void ListadoDePlanetasCantidadDePlanetasEsCorrecta() {
            // Arrange
            int numeroPlanetas = 10;
            PlanetasController planetasController = new PlanetasController();

            // Act
            ViewResult vista = planetasController.listadoDePlanetas() as ViewResult;

            // Assert
            Assert.AreEqual(numeroPlanetas, vista.ViewBag.planetas.Count);
        }
    }
}
