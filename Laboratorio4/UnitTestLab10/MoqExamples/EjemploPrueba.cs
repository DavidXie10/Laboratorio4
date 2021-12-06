using Microsoft.VisualStudio.TestTools.UnitTesting;
using Laboratorio4.Controllers;
using Laboratorio4.Models;
using Moq;
using System.Web.Mvc;

namespace UnitTestLab10.MoqExamples {
    [TestClass]
    public class EjemploPrueba {
        [TestMethod]
        public void TestEliminatePlanet() {
            PlanetaModel planet = new PlanetaModel { id = 1, nombre = "Tierra", numeroAnillos = 6, tipo = "Rocoso" };
            var eliminatePlanetServiceMock = new Mock<IPlanetsEliminate>();
            eliminatePlanetServiceMock.Setup(earthService => earthService.Eliminate(planet.id)).Returns(true);
            var earthController = new ExampleLab11Controller(eliminatePlanetServiceMock.Object);
            RedirectToRouteResult view = earthController.EliminatePlanet(planet) as RedirectToRouteResult;
            Assert.AreEqual("listadoDePlanetas", view.RouteValues["action"]);
        }
    }
}
