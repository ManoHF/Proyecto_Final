using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Npgsql;

namespace Proyecto_BD
{
    public class Covid_protocol
    {
		private int id_hospital;
		private string screen_covid_patients;
		private string preventions_campaigns;
		private string current_ability_for_tests;
		private string resources_for_covid;
		private string track_regularly_cases;
		private string report_covid_result_to_moph_days;
		Covid_protocol cp;

        public Covid_protocol()
        {
        }

        public Covid_protocol(int id_hospital, string screen_covid_patients, string preventions_campaigns, string current_ability_for_tests, string resources_for_covid, string track_regularly_cases, string report_covid_result_to_moph_days)
        {
            this.id_hospital = id_hospital;
            this.screen_covid_patients = screen_covid_patients;
            this.preventions_campaigns = preventions_campaigns;
            this.current_ability_for_tests = current_ability_for_tests;
            this.resources_for_covid = resources_for_covid;
            this.track_regularly_cases = track_regularly_cases;
            this.report_covid_result_to_moph_days = report_covid_result_to_moph_days;
        }

        public int registraProtocol()
		{
			NpgsqlCommand cmd, cmd2;
			NpgsqlConnection con;
			NpgsqlDataReader rd;
			int res = -1;
			String q = "Select currval(pg_get_serial_sequence('voxmapp.covid_protocol', 'id_covid_protocol'))";
			String query = "insert into voxmapp.covid_protocol (id_hospital, screen_covid_patients, preventions_campaigns, currently_ability_for_tests, resources_for_covid, track_regularly_cases, report_covid_result_to_moph_days) values (" + id_hospital + ", " +
				screen_covid_patients + ", " + preventions_campaigns + ", " + current_ability_for_tests + ", " + resources_for_covid + ", " + track_regularly_cases + ", " + report_covid_result_to_moph_days + ")";
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
