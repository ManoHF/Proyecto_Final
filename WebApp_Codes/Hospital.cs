using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Npgsql;

namespace Proyecto_BD
{
    public class Hospital
    {
        private double latitud;
        private double longitud;
        private double altura;
        private string nombre;
        private string distrito;
        private string provincia;
        private string pais;
        private string tipo_hospital;
        Hospital h;

        public Hospital()
        {
        }

        public Hospital(double latitud, double longitud, double altura, string nombre, string distrito, string provincia, string pais, string tipo_hospital)
        {
            this.latitud = latitud;
            this.longitud = longitud;
            this.altura = altura;
            this.nombre = nombre;
            this.distrito = distrito;
            this.provincia = provincia;
            this.pais = pais;
            this.tipo_hospital = tipo_hospital;
        }

        public int registraHospital()
        {
            NpgsqlCommand cmd, cmd2;
            NpgsqlConnection con = Conexion.agregarConexion();
            NpgsqlDataReader rd;
            int res=-1;
            String q = "Select nextval(pg_get_serial_sequence('voxmapp.hospital', 'id_hospital'))";
            String query = "insert into voxmapp.hospital (latitude, lengths, altitude, name_hospital, district, province, country, type_of_hospital, last_update) values (" +
                latitud + ", " + longitud + ", " + altura + ", " + "'" + nombre + "', '" + distrito + "', '" + provincia + "', '" + pais + "', '" + tipo_hospital + "', now())";
            try
            {
                cmd2 = new NpgsqlCommand(query, con);
                cmd2.ExecuteNonQuery();
                cmd = new NpgsqlCommand(q, con);
                rd = cmd.ExecuteReader();
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

        public int cuentaHospital()
        {
            int res = -1;
            try
            {
                NpgsqlConnection con;
                NpgsqlDataReader rd;
                con = Conexion.agregarConexion();
                NpgsqlCommand cmd = new NpgsqlCommand("select count(*) from voxmapp.hospital", con);
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    res = rd.GetInt32(0);
                }
                rd.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                res = -1;
            }
            return res;
        }

        public int regresaID(String nombre)
        {
            int res = -1;
            String query = "select id_hospital from voxmapp.hospital where name_hospital='" + nombre + "'";
            try
            {
                NpgsqlConnection con;
                NpgsqlDataReader rd;
                con = Conexion.agregarConexion();
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    res = rd.GetInt32(0);
                }
                rd.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                res = -1;
            }
            return res;
        }
    }
}
