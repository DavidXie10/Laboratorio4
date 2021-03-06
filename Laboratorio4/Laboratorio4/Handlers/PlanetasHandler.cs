using Laboratorio4.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace Laboratorio4.Handlers
{
    public class PlanetasHandler
    {
        private SqlConnection conexion;
        private string rutaConexion;
        public PlanetasHandler()
        {
            rutaConexion =
           ConfigurationManager.ConnectionStrings["PlanetasConnection"].ToString();
            conexion = new SqlConnection(rutaConexion);
        }
        private DataTable CrearTablaConsulta(string consulta)
        {
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new
           SqlDataAdapter(comandoParaConsulta);
            DataTable consultaFormatoTabla = new DataTable();
            conexion.Open();
            adaptadorParaTabla.Fill(consultaFormatoTabla);
            conexion.Close();
            return consultaFormatoTabla;
        }
        public List<PlanetaModel> obtenerTodoslosPlanetas()
        {
            List<PlanetaModel> planetas = new List<PlanetaModel>();
            string consulta = "SELECT * FROM Planeta ";
            DataTable tablaResultado = CrearTablaConsulta(consulta);
            foreach (DataRow columna in tablaResultado.Rows)
            {
                planetas.Add(
                new PlanetaModel
                {
                    nombre = Convert.ToString(columna["nombrePlaneta"]),
                    tipo = Convert.ToString(columna["tipoPlaneta"]),
                    id = Convert.ToInt32(columna["planetaId"]),
                    numeroAnillos = Convert.ToInt32(columna["numeroAnillos"])
                });
            }
            return planetas;
        }

        private byte[] obtenerBytes(HttpPostedFileBase archivo)
        {
            byte[] bytes;
            BinaryReader lector = new BinaryReader(archivo.InputStream);
            bytes = lector.ReadBytes(archivo.ContentLength);
            return bytes;
        }

        public bool crearPlaneta(PlanetaModel planeta)
        {
            string consulta = "INSERT INTO Planeta (archivoPlaneta, tipoArchivo, nombrePlaneta, numeroAnillos, tipoPlaneta) " +
                "VALUES (@archivo, @tipoArchivo, @nombre, @numeroAnillos, @tipoPlaneta) ";
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@archivo", obtenerBytes(planeta.archivo));
            comandoParaConsulta.Parameters.AddWithValue("@tipoArchivo", planeta.archivo.ContentType);
            comandoParaConsulta.Parameters.AddWithValue("@nombre", planeta.nombre);
            comandoParaConsulta.Parameters.AddWithValue("@numeroAnillos", planeta.numeroAnillos);
            comandoParaConsulta.Parameters.AddWithValue("@tipoPlaneta", planeta.tipo);

            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1;  // indica que se agregO una tupla(cuando es mayor o igual que 1)
            conexion.Close();
            return exito;
        }

        public bool modificarPlaneta(PlanetaModel planeta)
        {
            string consulta = "UPDATE Planeta SET archivoPlaneta = @archivo, tipoArchivo = @tipoArchivo, nombrePlaneta = @nombre, " +
                "numeroAnillos = @numeroAnillos, tipoPlaneta = @tipoPlaneta " + 
                "WHERE planetaId = @planetaId";
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTable = new SqlDataAdapter(comandoParaConsulta);
            comandoParaConsulta.Parameters.AddWithValue("@archivo", obtenerBytes(planeta.archivo));
            comandoParaConsulta.Parameters.AddWithValue("@tipoArchivo", planeta.archivo.ContentType);
            comandoParaConsulta.Parameters.AddWithValue("@nombre", planeta.nombre);
            comandoParaConsulta.Parameters.AddWithValue("@numeroAnillos", planeta.numeroAnillos);
            comandoParaConsulta.Parameters.AddWithValue("@tipoPlaneta", planeta.tipo);
            comandoParaConsulta.Parameters.AddWithValue("@planetaId", planeta.id);

            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1;
            conexion.Close();

            return exito;
        }

        public Tuple<byte[], string> descargarContenido(int id)
        {
            byte[] bytes;
            string contentType;
            string consulta = "SELECT archivoPlaneta, tipoArchivo FROM Planeta WHERE planetaId = @planetaId";
            
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new
            SqlDataAdapter(comandoParaConsulta);
            comandoParaConsulta.Parameters.AddWithValue("@planetaId", id);
            conexion.Open();
            SqlDataReader lectorDeDatos = comandoParaConsulta.ExecuteReader();
            lectorDeDatos.Read();

            bytes = (byte[])lectorDeDatos["archivoPlaneta"];
            contentType = lectorDeDatos["tipoArchivo"].ToString();
            conexion.Close();
            return new Tuple<byte[], string>(bytes, contentType);
        }
    }
}