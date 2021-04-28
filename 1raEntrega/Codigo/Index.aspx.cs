using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_BD
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion.llenarHospital(ddHospital);
                //Llenado de dropdownlist con SI y NO
                Conexion.llenarDDLYN(ddlYN12);
                Conexion.llenarDDLYN(ddlYN13);
                Conexion.llenarDDLYN(ddlYN14);
                Conexion.llenarDDLYN(ddlYN16);
                //Llenado de dropdownlist para las preguntas de reservas
                Conexion.llenarDDLReserves(ddlReserves1);
                Conexion.llenarDDLReserves(ddlReserves2);
                Conexion.llenarDDLReserves(ddlReserves3);
                Conexion.llenarDDLReserves(ddlReserves4);
                Conexion.llenarDDLReserves(ddlReserves5);
                Conexion.llenarDDLReserves(ddlReserves6);
                Conexion.llenarDDLReserves(ddlReserves7);
                Conexion.llenarDDLReserves(ddlReserves8);
                Conexion.llenarDDLReserves(ddlReserves9);
                Conexion.llenarDDLReserves(ddlReserves10);
                Conexion.llenarDDLReserves(ddlReserves11);
                //LLena lista de radio buttons de recursos
                Conexion.llenarDDLResources(ddlRecursos15);
                //Llena lista de radio buttons de rastreo usando el método de reservas
                Conexion.llenarDDLReserves(ddlRastreo17);
                //Llena radio button lists de preguntas de monitoreo
                Conexion.llenarDDLStatus(ddlM1);
                Conexion.llenarDDLProblemas(ddlM2);
                Conexion.llenarDDLAcciones(ddlM3);
                //Llenado de preguntas con rangos de números
                Conexion.llenarDDLNumeros1(ddlNum18);
                Conexion.llenarDDLNumeros1(ddlNum19);
                Conexion.llenarDDLNumeros1(ddlNum20);
                Conexion.llenarDDLNumeros1(ddlNum21);
                //Llenado de preguntas con rango de números con opción 9 como "no sé"
                Conexion.llenarDDLNumeros2(ddlNum22);
                Conexion.llenarDDLNumeros2(ddlNum23);
                Conexion.llenarDDLNumeros2(ddlNum24);
                Conexion.llenarDDLNumeros2(ddlNum25);
                Conexion.llenarDDLNumeros2(ddlNum26);
                Conexion.llenarDDLNumeros2(ddlNum27);
                Conexion.llenarDDLNumeros2(ddlNum28);
                Conexion.llenarDDLNumeros2(ddlNum29);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Hospital h = new Hospital(Convert.ToDouble(tbLat.Text), Convert.ToDouble(tbLon.Text), Convert.ToDouble(tbAlt.Text),
                tbNom.Text, tbDis.Text, tbPro.Text, tbPa.Text);
            tbLat.Text = ""; tbAlt.Text = ""; tbDis.Text = ""; tbPa.Text = "";
            tbLon.Text = ""; tbNom.Text = ""; tbPro.Text = "";
            int res = h.registraHospital();
            if (res == 0)
            {
                lbRegistro.Text = "Éxito";
                Button1.Enabled = false;
                ddHospital.Items.Clear();
                Conexion.llenarHospital(ddHospital);
                ddHospital.SelectedIndex = h.cuentaHospital();
            }
            else
                lbRegistro.Text = "Fallo en el registro";
        }
    }
}
