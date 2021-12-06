using System.Diagnostics;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestLab10.MoqExamples {
    public interface IDogsUpdate {
        bool Update(string breed);
    }

    [TestClass]
    public class HuskyController : Controller{
        protected IDogsUpdate updateDogsService;

        public HuskyController(IDogsUpdate updateDogsService) {
            this.updateDogsService = updateDogsService;
        }

        public ActionResult UpdateDog(string breed) {
            ActionResult view;
            var result = this.updateDogsService.Update(breed);

            if(result == true) {
                view = RedirectToAction("Index", "Home");
                ViewBag.Message = "¡El perro " + breed + " fue actualizado con éxito!";
            } else {
                view = null;
                ViewBag.Message = "¡El perro " + breed + " no se pudo actualizar!";
            }

            return view;
        }
    }

    [TestClass]
    public class Example2 {
        [TestMethod]
        public void TestUpdateDogFromDogsController() {
            var huskyServiceMock = new Mock<IDogsUpdate>();
            huskyServiceMock.Setup(huskyService => huskyService.Update("husky")).Returns(true);
            var huskyController = new HuskyController(huskyServiceMock.Object);
            RedirectToRouteResult view = huskyController.UpdateDog("husky") as RedirectToRouteResult;
            Assert.AreEqual("Index", view.RouteValues["action"]);
        }
    }
}
