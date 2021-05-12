using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Npgsql;

namespace Proyecto_BD
{
    public class Patient_statistics
    {
        private int id_hospital;
        private string amount_last_month_covid_symptoms;
        private string amount_last_month_tested_positive_covid;
        private string amount_last_month_in_intensive_care_wcovid;
        private string amount_last_month_deaths_by_covid;
        private string amount_last_month_deaths_non_covid;
        private string amount_last_month_recovered_from_covid;
        Patient_statistics ps;

        public Patient_statistics()
        {
        }

        public Patient_statistics(int id_hospital, string amount_last_month_covid_symptoms, string amount_last_month_tested_positive_covid, string amount_last_month_in_intensive_care_wcovid, string amount_last_month_deaths_by_covid, string amount_last_month_deaths_non_covid, string amount_last_month_recovered_from_covid)
        {
            this.id_hospital = id_hospital;
            this.amount_last_month_covid_symptoms = amount_last_month_covid_symptoms;
            this.amount_last_month_tested_positive_covid = amount_last_month_tested_positive_covid;
            this.amount_last_month_in_intensive_care_wcovid = amount_last_month_in_intensive_care_wcovid;
            this.amount_last_month_deaths_by_covid = amount_last_month_deaths_by_covid;
            this.amount_last_month_deaths_non_covid = amount_last_month_deaths_non_covid;
            this.amount_last_month_recovered_from_covid = amount_last_month_recovered_from_covid;
        }

        public int registraPatient_statistics()
        {
            NpgsqlCommand cmd, cmd2;
            NpgsqlConnection con;
            NpgsqlDataReader rd;
            int res = -1;
            String q = "Select nextval(pg_get_serial_sequence('voxmapp.patient_statistics', 'id_patient_statistics'))";
            String query = "insert into voxmapp.patient_statistics (id_hospital, amount_last_month_covid_symptoms, amount_last_month_tested_positive_covid, amount_last_month_in_intensive_care_wcovid, amount_last_month_deaths_by_covid, " +
                " amount_last_month_deaths_non_covid, amount_last_month_recovered_from_covid) values (" + id_hospital + ", '" +
                amount_last_month_covid_symptoms + "', '" + amount_last_month_tested_positive_covid + "', '" + amount_last_month_in_intensive_care_wcovid +
                "', '" + amount_last_month_deaths_by_covid + "', '" + amount_last_month_deaths_non_covid + "', '" + amount_last_month_recovered_from_covid + "')";
            try
            {
                con = Conexion.agregarConexion();
                cmd2 = new NpgsqlCommand(query, con);
                cmd2.ExecuteNonQuery();
                cmd = new NpgsqlCommand(q, con);
                rd = cmd.ExecuteReader();
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