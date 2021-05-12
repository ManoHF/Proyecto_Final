using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Npgsql;

namespace Proyecto_BD
{
	public class Telephone
	{
		private int id_hospital;
		private int lada;
		private string phone_numb;


		public Telephone()
		{
		}

		public Telephone(int id_hospital, int lada, string phone_numb)
		{
			this.id_hospital = id_hospital;
			this.lada = lada;
			this.phone_numb = phone_numb;
		}

		public int registraTelefono()
		{
			NpgsqlCommand cmd;
			NpgsqlConnection con;
			int res;
			String query = "insert into voxmapp.telephone (id_hospital, lada, phone_numb) values (" + id_hospital +
				", " + lada + ", " + phone_numb + ")";
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

		public int actualizaTelefono(string telefono, int id_hospital)
		{
			NpgsqlCommand cmd;
			NpgsqlConnection con;
			int res;
			String query = "update voxmapp.telephone set phone_numb=" + telefono + ", last_update=now() where id_hospital=" + id_hospital;
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
	}
}