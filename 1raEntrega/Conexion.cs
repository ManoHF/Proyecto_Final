using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

using Npgsql;

namespace Proyecto_BD
{
    public class Conexion
    {

        public static NpgsqlConnection agregarConexion()
        {
            NpgsqlConnection conexion = new NpgsqlConnection();
            try
            {
                conexion.ConnectionString = "Host=localhost;Username=postgres;Password=admin;Database=proyecto_final";
                conexion.Open();
            }
            catch (Exception ex)
            {
                conexion = null;
            }
            return conexion;
        }

        //Método que recibe un DropDownList para llenarlo con los nombres de los hospitales registrados
        //obtenidos de la base de datos
        public static void llenarHospital(DropDownList dd)
        {
            try
            {
                NpgsqlConnection con;
                NpgsqlDataReader rd;
                con = Conexion.agregarConexion();
                NpgsqlCommand cmd = new NpgsqlCommand("select nombre from voxmapp.hospital", con);
                rd = cmd.ExecuteReader();
                dd.Items.Add("[Nombre del hospital]");
                while (rd.Read()) 
                {
                    dd.Items.Add(rd.GetString(0));  
                }
                dd.SelectedIndex = 0; 
                rd.Close();
                con.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public static void llenarDDLYN(DropDownList dd)
        {
            try
            {
                dd.Items.Add("[Selecciona una respuesta]");
                dd.Items.Add("SÍ");
                dd.Items.Add("NO");
                dd.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

            }
        }

        public static void llenarDDLReserves(DropDownList dd)
        {
            try
            {
                dd.Items.Add("[Selecciona una respuesta]");
                dd.Items.Add("30 días");
                dd.Items.Add("15 días");
                dd.Items.Add("7 días");
                dd.Items.Add("3 días");
                dd.Items.Add("Ninguno");
                dd.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

            }
        }

        public static void llenarDDLResources(DropDownList dd)
        {
            try
            {
                dd.Items.Add("[Selecciona una respuesta]");
                dd.Items.Add("Del gobierno");
                dd.Items.Add("No gubernamental");
                dd.Items.Add("Ambos");
                dd.Items.Add("Ninguno");
                dd.Items.Add("No sé");
                dd.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

            }
        }

        public static void llenarDDLNumeros1(DropDownList dd)
        {
            try
            {
                dd.Items.Add("[Selecciona una respuesta]");
                dd.Items.Add("0-200");
                dd.Items.Add("201-400");
                dd.Items.Add("401-600");
                dd.Items.Add("601-800");
                dd.Items.Add("801-1000");
                dd.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

            }
        }

        public static void llenarDDLNumeros2(DropDownList dd)
        {
            try
            {
                dd.Items.Add("[Selecciona una respuesta]");
                dd.Items.Add("9");
                dd.Items.Add("10-200");
                dd.Items.Add("201-400");
                dd.Items.Add("401-600");
                dd.Items.Add("601-800");
                dd.Items.Add("801-1000");
                dd.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

            }
        }

        public static void llenarDDLStatus(DropDownList dd)
        {
            try
            {
                dd.Items.Add("[Selecciona una respuesta]");
                dd.Items.Add("Completo");
                dd.Items.Add("Parcialmente completo");
                dd.Items.Add("No hecho");
                dd.Items.Add("Fue una prueba");
                dd.Items.Add("Ver otros comentarios");
                dd.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

            }
        }

        public static void llenarDDLProblemas(DropDownList dd)
        {
            try
            {
                dd.Items.Add("[Selecciona una respuesta]");
                dd.Items.Add("Ninguno");
                dd.Items.Add("Falta información");
                dd.Items.Add("Se negaron a hablar");
                dd.Items.Add("No se tomó la llamada");
                dd.Items.Add("Teléfono equivocado / Doctor renunció");
                dd.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

            }
        }

        public static void llenarDDLAcciones(DropDownList dd)
        {
            try
            {
                dd.Items.Add("[Selecciona una respuesta]");
                dd.Items.Add("Ninguna");
                dd.Items.Add("Llamar mañana");
                dd.Items.Add("Conseguir nuevo teléfono");
                dd.Items.Add("Conseguir autorización");
                dd.Items.Add("Otra");
                dd.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

            }
        }
    }
}