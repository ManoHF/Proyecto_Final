using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

using Npgsql;

namespace Proyecto_BD
{
    public class Users
    {
        private string u_name;
        private string u_position;
        private Int32 phone;
        private string mail;
        Users u;

        public Users()
        {
        }

        public Users(string u_name, string u_position, int phone, string mail)
        {
            this.u_name = u_name;
            this.u_position = u_position;
            this.phone = phone;
            this.mail = mail;
        }

        public void llenaDDLUsers(DropDownList ddl)
        {
            try
            {
                NpgsqlConnection con;
                NpgsqlDataReader rd;
                con = Conexion.agregarConexion();
                NpgsqlCommand cmd = new NpgsqlCommand("select id_users from voxmapp.users", con);
                rd = cmd.ExecuteReader();
                ddl.Items.Add("[User's id]");
                while (rd.Read())
                {
                    ddl.Items.Add(rd.GetInt16(0).ToString());
                }
                ddl.SelectedIndex = 0;
                rd.Close();
                con.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
