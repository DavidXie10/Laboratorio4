using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laboratorio4.Handlers;
using Laboratorio4.Models;

namespace Laboratorio4.Controllers
{
    public class PlanetasController : Controller
    {
        public ActionResult listadoDePlanetas()
        {
            PlanetasHandler accesoDatos = new PlanetasHandler();
            ViewBag.planetas = accesoDatos.obtenerTodoslosPlanetas();
            return View();
        }

        // método para enlazar la vista del formulario
        /*
         * El método encargado de "llamar" o "ejecutar" la vista es el que no recibe ningún parámetro
         */
        public ActionResult crearPlaneta()
        {
            return View();
        }

        // también desplegará la misma vista que el primero, pero únicamente después que a través de un formulario haga post direccionado los datos este método
        /*
         * el parámetro que recibe el segundo método es un modelo, este modelo cuando el formulario se envía, va cargado de información. El modelo es el paquete de datos. Se puede decir que, en general, usualmente cuando se crea un formulario se hacen dos métodos de controlador: el que ejecuta la vista para llegar al formulario y el que recibe los datos del formulario. 
         */
        [HttpPost]
        public ActionResult crearPlaneta(PlanetaModel planeta)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    PlanetasHandler accesoDatos = new PlanetasHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.crearPlaneta(planeta);
                    if(ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "El planeta" + " " + planeta.nombre + " fue creado con éxito :)";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible crear el planeta :(";
                return View(); // si falla se regresa a la vista original pero sin el mensaje
            }
        }

        // Esto es lo que hace el GET: https://localhost:44346/Planetas/editarPlaneta?identificador=1
        [HttpGet]
        public ActionResult editarPlaneta(int ? identificador)
        {
            ActionResult vista;
            try
            {
                PlanetasHandler accesoDatos = new PlanetasHandler();
                PlanetaModel planetaModificar = accesoDatos.obtenerTodoslosPlanetas().Find(smodel => smodel.id == identificador);
                if (planetaModificar == null)
                {
                    vista = RedirectToAction("listadoDePlanetas");
                }
                else
                {
                    // el planeta resultante se pasa por parámetro a la vista. Esta es otra manera de enviar valores a la vista(recuerden el ViewBag)
                    vista = View(planetaModificar);
                }
            }
            catch
            {
                vista = RedirectToAction("listadoDePlanetas");
            }
            return vista;
        }

        // Esto envia los datos del formulario que se han escrito y los recibe
        [HttpPost]
        public ActionResult editarPlaneta(PlanetaModel planeta)
        {
            try
            {
                PlanetasHandler accesoDatos = new PlanetasHandler();
                accesoDatos.modificarPlaneta(planeta);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public FileResult accederArchivo(int identificador) {
            PlanetasHandler accesoDatos = new PlanetasHandler();
            var tupla = accesoDatos.descargarContenido(identificador);
            return File(tupla.Item1, tupla.Item2);
        }
    }
}