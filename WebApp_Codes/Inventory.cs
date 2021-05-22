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
		private string oxygen;
		private string antypiretic;
		private string anesthesia;
		private string soap_alcohol_solution;
		private string disposable_masks;
		private string disposable_gloves;
		private string disposable_hats;
		private string disposable_aprons;
		private string surgical_gloves;
		private string shoe_covers;
		private string visors;
		private string covid_test_kits;
		Inventory i;

        public Inventory()
        {
        }

        public Inventory(int id_hospital, string oxygen, string antypiretic, string anesthesia, string soap_alcohol_solution, string disposable_masks, string disposable_gloves, string disposable_hats, string disposable_aprons, string surgical_gloves, string shoe_covers, string visors, string covid_test_kits)
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
			String q = "Select currval(pg_get_serial_sequence('voxmapp.inventory', 'id_inventory'))";
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
