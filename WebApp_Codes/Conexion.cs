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
                NpgsqlCommand cmd = new NpgsqlCommand("select name_hospital from voxmapp.hospital", con);
                rd = cmd.ExecuteReader();
                dd.Items.Add("[Hospital's name]");
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
                dd.Items.Add("[Select an answer]");
                dd.Items.Add("YES");
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
                dd.Items.Add("[Select an answer]");
                dd.Items.Add("30");
                dd.Items.Add("15");
                dd.Items.Add("7");
                dd.Items.Add("3");
                dd.Items.Add("0");
                dd.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

            }
        }

        public static void llenarDDLRastreo(DropDownList dd)
        {
            try
            {
                dd.Items.Add("[Select an answer]");
                dd.Items.Add("Every 30 days");
                dd.Items.Add("Every 15 days");
                dd.Items.Add("Every 7 days");
                dd.Items.Add("Every 3 days");
                dd.Items.Add("Never");
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
                dd.Items.Add("[Select an answer]");
                dd.Items.Add("Government");
                dd.Items.Add("Non-governmental");
                dd.Items.Add("Both");
                dd.Items.Add("None");
                dd.Items.Add("Don't know");
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
                dd.Items.Add("[Select an answer]");
                dd.Items.Add("Questionnaire completed");
                dd.Items.Add("Questionnaire completed partially");
                dd.Items.Add("Questionnaire not done");
                dd.Items.Add("This was a test");
                dd.Items.Add("Other see comments");
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
                dd.Items.Add("[Select an answer]");
                dd.Items.Add("No problem");
                dd.Items.Add("Some data is missing");
                dd.Items.Add("Refused to speak");
                dd.Items.Add("No answer");
                dd.Items.Add("Wrong phone number");
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
                dd.Items.Add("[Select an answer]");
                dd.Items.Add("No additional action needed");
                dd.Items.Add("Call back tomorrow to complete data");
                dd.Items.Add("Find new phone number");
                dd.Items.Add("Find authorization");
                dd.Items.Add("Other see comments");
                dd.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
