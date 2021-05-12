using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Npgsql;

namespace Proyecto_BD
{
	public class Inventory
	{
		private int id_hospital;
		private int oxygen;
		private int antypiretic;
		private int anesthesia;
		private int soap_alcohol_solution;
		private int disposable_masks;
		private int disposable_gloves;
		private int disposable_hats;
		private int disposable_aprons;
		private int surgical_gloves;
		private int shoe_covers;
		private int visors;
		private int covid_test_kits;
		Inventory i;

        public Inventory()
        {
        }

        public Inventory(int id_hospital, int oxygen, int antypiretic, int anesthesia, int soap_alcohol_solution, int disposable_masks, int disposable_gloves, int disposable_hats, int disposable_aprons, int surgical_gloves, int shoe_covers, int visors, int covid_test_kits)
        {
            this.id_hospital = id_hospital;
            this.oxygen = oxygen;
            this.antypiretic = antypiretic;
            this.anesthesia = anesthesia;
            this.soap_alcohol_solution = soap_alcohol_solution;
            this.disposable_masks = disposable_masks;
            this.disposable_gloves = disposable_gloves;
            this.disposable_hats = disposable_hats;
            this.disposable_aprons = disposable_aprons;
            this.surgical_gloves = surgical_gloves;
            this.shoe_covers = shoe_covers;
            this.visors = visors;
            this.covid_test_kits = covid_test_kits;
        }

        public int registraInventario()
		{
			NpgsqlCommand cmd, cmd2;
			NpgsqlConnection con;
			NpgsqlDataReader rd;
			int res = -1;
			String q = "Select nextval(pg_get_serial_sequence('voxmapp.inventory', 'id_inventory'))";
			String query = "insert into voxmapp.inventory (id_hospital, oxygen, antypiretic, anesthesia, soap_alcohol_solution, disposable_masks, disposable_gloves, disposable_hats, disposable_aprons, surgical_gloves, shoe_covers, visors, covid_test_kits) values (" + id_hospital + ", " +
				oxygen + ", " + antypiretic + ", " + anesthesia + ", " + soap_alcohol_solution + ", " + disposable_masks + ", " + disposable_gloves + ", " + disposable_hats + ", " + disposable_aprons + ", " + surgical_gloves + ", " + shoe_covers + ", " + visors + ", " + covid_test_kits + ")";
			try
			{
				con = Conexion.agregarConexion();
				cmd = new NpgsqlCommand(query, con);
				cmd.ExecuteNonQuery();
				cmd2 = new NpgsqlCommand(q, con);
				rd = cmd2.ExecuteReader();
				if (rd.Read())
				{
					res = rd.GetInt16(0) - 1;
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