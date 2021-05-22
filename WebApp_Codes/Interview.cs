using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Npgsql;

namespace Proyecto_BD
{
    public class Interview
    {
        private int id_hospital;
        private int id_users;
        private string moph;
        private string status;
        private string problems;
        private string actions;
        Interview e;

        public Interview()
        {
        }

        public Interview(int id_hospital, int id_users, string moph, string status, string problems, string actions)
        {
            this.id_hospital = id_hospital;
            this.id_users = id_users;
            this.moph = moph;
            this.status = status;
            this.problems = problems;
            this.actions = actions;
        }

        public int registraEntrevista()
        {
            NpgsqlCommand cmd;
            NpgsqlConnection con;
            int res;
            String query = "insert into voxmapp.interview (id_hospital, id_users, moph, status, problems, actions) values (" +
               id_hospital + ", " + id_users + ", " + moph + ", '" + status + "', " + problems + ", " + actions + ")";
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
