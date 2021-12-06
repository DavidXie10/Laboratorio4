using System.Web.Mvc;
using Laboratorio4.Models;

namespace Laboratorio4.Controllers
{
    public interface IPlanetsEliminate {
        bool Eliminate(int planetId);
    }

    public class ExampleLab11Controller : Controller
    {
        protected IPlanetsEliminate eliminatePlanetsService;

        public ExampleLab11Controller(IPlanetsEliminate eliminatePlanetsService) {
            this.eliminatePlanetsService = eliminatePlanetsService;
        }

        public ActionResult EliminatePlanet(PlanetaModel planet) {
            ActionResult view;
            var result = this.eliminatePlanetsService.Eliminate(planet.id);

            if (result == true) {
                view = RedirectToAction("listadoDePlanetas", "Planetas");
                ViewBag.Message = "¡El planeta " + planet.nombre + " ha sido eliminado satisfactoriamente!";
                ViewBag.Success = true;
            } else {
                view = RedirectToAction("Index", "Home");
                ViewBag.Message = "¡Ocurrió un error! No se pudo eliminar el planeta";
                ViewBag.Success = false;
            }

            return view;
        }
    }
}