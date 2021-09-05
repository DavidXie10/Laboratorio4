using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorio4.Models
{
    public class PlanetaModel
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string tipo { get; set; }
        public int numeroAnillos { get; set; }
        // de momento no se va trabajar con el archivo...
    }
}