using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Npgsql;

namespace Proyecto_BD
{
    public class Hospital_structure
    {
        private int id_hospital;
        private int id_inventory;
        private int id_patient_statistics;
        private int id_covid_protocol;
        private int id_staff;
        Hospital_structure hs;

        public Hospital_structure()
        {
        }

        public Hospital_structure(int id_hospital, int id_inventory, int id_patient_statistics, int id_covid_protocol, int id_staff)
        {
            this.id_hospital = id_hospital;
            this.id_inventory = id_inventory;
            this.id_patient_statistics = id_patient_statistics;
            this.id_covid_protocol = id_covid_protocol;
            this.id_staff = id_staff;
        }

        public int registraStructure()
        {
            NpgsqlCommand cmd, cmd2;
            NpgsqlConnection con;
            NpgsqlDataReader rd;
            int res = -1;
            String q = "Select nextval(pg_get_serial_sequence('voxmapp.hospital_structure', 'id_hospital_structure'))";
            String query = "insert into voxmapp.hospital_structure (id_hospital, id_inventory, id_patient_statistics, id_covid_protocol, id_staff) values (" + id_hospital + ", " +
                id_inventory + ", " + id_patient_statistics + ", " + id_covid_protocol + ", " + id_staff + ")";
            try
            {
                con = Conexion.agregarConexion();
                cmd = new NpgsqlCommand(query, con);
                cmd.ExecuteNonQuery();
                cmd2 = new NpgsqlCommand(q, con);
                rd = cmd2.ExecuteReader();
                if (rd.Read())
                {
                    res = rd.GetInt16(0)-1;
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