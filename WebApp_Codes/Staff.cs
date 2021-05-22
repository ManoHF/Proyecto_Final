using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Npgsql;

namespace Proyecto_BD
{
    public class Staff
    {
        private int id_hospital;
        private string amount_of_doctors_in_hospital;
        private string amount_of_paramedical_staff_in_hospital;
        Staff s;

        public Staff()
        {
        }

        public Staff(int id_hospital, string amount_of_doctors_in_hospital, string amount_of_paramedical_staff_in_hospital)
        {
            this.id_hospital = id_hospital;
            this.amount_of_doctors_in_hospital = amount_of_doctors_in_hospital;
            this.amount_of_paramedical_staff_in_hospital = amount_of_paramedical_staff_in_hospital;
        }

        public int registraStaff()
        {
            NpgsqlCommand cmd, cmd2;
            NpgsqlConnection con;
            NpgsqlDataReader rd;
            int res = -1;
            String q = "Select currval(pg_get_serial_sequence('voxmapp.staff', 'id_staff'))";
            String query = "insert into voxmapp.staff (id_hospital, amount_of_doctors_in_hospital, amount_of_paramedical_staff_in_hospital) values (" +
               id_hospital + ", " + amount_of_doctors_in_hospital + ", " + amount_of_paramedical_staff_in_hospital + ")";
            try
            {
                con = Conexion.agregarConexion();
                cmd = new NpgsqlCommand(query, con);
                cmd.ExecuteNonQuery();
                cmd2 = new NpgsqlCommand(q, con);
                rd = cmd2.ExecuteReader();
                if (rd.Read())
                {
                    res = rd.GetInt16(0);
                } 
                con.Close();
                return res;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
