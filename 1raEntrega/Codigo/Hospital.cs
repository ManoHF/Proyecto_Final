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
        Hospital h;

        public Hospital(double latitud, double longitud, double altura, string nombre, string distrito, string provincia, string pais)
        {
            this.latitud = latitud;
            this.longitud = longitud;
            this.altura = altura;
            this.nombre = nombre;
            this.distrito = distrito;
            this.provincia = provincia;
            this.pais = pais;
        }

        public int registraHospital()
        {
            NpgsqlCommand cmd;
            NpgsqlConnection con;
            int res;
            String query = "insert into voxmapp.hospital (latitud, longitud, altitud, nombre, distrito, provincia, pais) values (" +
                latitud + ", " + longitud + ", " + altura + ", " + "'" + nombre + "', '" + distrito + "', '" + provincia + "', '" + pais + "')";
            try
            {
                con = Conexion.agregarConexion();
                cmd = new NpgsqlCommand(query, con);
                res = cmd.ExecuteNonQuery();
                con.Close();
                return 0;
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
    }
}
