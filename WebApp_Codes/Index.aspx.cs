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
        int id_hospital = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Conexion.llenarHospital(ddHospital);

                //Llenado de dropdownlist con SI y NO
                Conexion.llenarDDLYN(ddlYN13);
                Conexion.llenarDDLYN(ddlYN14);
                Conexion.llenarDDLYN(ddlYN15);
                Conexion.llenarDDLYN(ddlYN17);

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
                Conexion.llenarDDLReserves(ddlReserves12);

                //LLena lista de radio buttons de recursos
                Conexion.llenarDDLResources(ddlRecursos16);

                //Llena lista de radio buttons de rastreo
                Conexion.llenarDDLRastreo(ddlRastreo18);

                //Llena radio button lists de preguntas de monitoreo
                Conexion.llenarDDLStatus(ddlM1);
                Conexion.llenarDDLProblemas(ddlM2);
                Conexion.llenarDDLAcciones(ddlM3);

                //Llenado de preguntas con rangos de números
                Conexion.llenarDDLNumeros1(ddlNum19);
                Conexion.llenarDDLNumeros1(ddlNum20);

                //Llenado de preguntas con rango de números con opción 9 como "no sé"
                Conexion.llenarDDLNumeros2(ddlNum21);
                Conexion.llenarDDLNumeros2(ddlNum22);
                Conexion.llenarDDLNumeros2(ddlNum23);
                Conexion.llenarDDLNumeros2(ddlNum24);
                Conexion.llenarDDLNumeros2(ddlNum25);
                Conexion.llenarDDLNumeros2(ddlNum26);

                //Lllenar dropdownlist de user id
                Users u = new Users();
                u.llenaDDLUsers(ddlUser);

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Hospital h = new Hospital(Convert.ToDouble(tbLat.Text), Convert.ToDouble(tbLon.Text), Convert.ToDouble(tbAlt.Text),
                tbNom.Text, tbDis.Text, tbPro.Text, tbPa.Text, tbTipo.Text); 

            tbLat.Text = ""; tbAlt.Text = ""; tbDis.Text = ""; tbPa.Text = "";
            tbLon.Text = ""; tbNom.Text = ""; tbPro.Text = ""; tbTipo.Text = "";

            int res = h.registraHospital();
            id_hospital = res;

            if (res > 0)
            {
                lbRegistro.Text = "Success";
                ddHospital.Items.Clear();
                Conexion.llenarHospital(ddHospital);
                ddHospital.SelectedIndex = h.cuentaHospital();
            }
            else
                lbRegistro.Text = "" + res;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Hospital h1 = new Hospital();
            if (id_hospital == 0)
                id_hospital = h1.regresaID(ddHospital.SelectedValue);

            Inventory i = new Inventory(id_hospital, Convert.ToInt16(ddlReserves1.SelectedValue), Convert.ToInt16(ddlReserves2.SelectedValue),
                Convert.ToInt16(ddlReserves3.SelectedValue), Convert.ToInt16(ddlReserves4.SelectedValue), Convert.ToInt16(ddlReserves5.SelectedValue), Convert.ToInt16(ddlReserves6.SelectedValue),
                Convert.ToInt16(ddlReserves7.SelectedValue), Convert.ToInt16(ddlReserves8.SelectedValue), Convert.ToInt16(ddlReserves9.SelectedValue), Convert.ToInt16(ddlReserves10.SelectedValue),
                Convert.ToInt16(ddlReserves11.SelectedValue), Convert.ToInt16(ddlReserves12.SelectedValue));
            int inventario = i.registraInventario();

            Staff s = new Staff(id_hospital, ddlNum19.SelectedValue , ddlNum20.SelectedValue);
            int staff = s.registraStaff();

            bool screen = false, campaign = false, ability = false, track = false;
            if (ddlYN13.SelectedValue == "YES")
                screen = true;
            if (ddlYN14.SelectedValue == "YES")
                campaign = true;
            if (ddlYN15.SelectedValue == "YES")
                ability = true;
            if (ddlYN17.SelectedValue == "YES")
                track = true;
            Covid_protocol cp = new Covid_protocol(id_hospital, screen, campaign, ability, ddlRecursos16.SelectedValue, track, ddlRastreo18.SelectedValue);
            int protocol = cp.registraProtocol();

            Patient_statistics ps = new Patient_statistics(id_hospital, ddlNum21.SelectedValue, ddlNum22.SelectedValue, ddlNum23.SelectedValue, 
                ddlNum24.SelectedValue, ddlNum25.SelectedValue, ddlNum26.SelectedValue);
            int stats = ps.registraPatient_statistics();

            Interview it = new Interview(id_hospital, Convert.ToInt16(ddlUser.SelectedValue), Convert.ToInt16(tbMOPH.Text),
                ddlM1.SelectedValue, ddlM2.SelectedValue, ddlM3.SelectedValue);
            it.registraEntrevista();

            lbRegistro.Text = id_hospital + " " + inventario + " " + stats + " " + protocol + " " + staff;

            Hospital_structure hs = new Hospital_structure(id_hospital, inventario, stats, protocol, staff);
            hs.registraStructure();
        }

        protected void btUpdate_Click(object sender, EventArgs e)
        {
            Telephone t = new Telephone();
            int res = t.actualizaTelefono(tbPhone.Text, ddHospital.SelectedIndex);
            if (res == 0)
                lbPhone.Text = "Successful update";
            else
                lbPhone.Text = "Update failed";
        }

        protected void btNewPhone_Click(object sender, EventArgs e)
        {
            Telephone t = new Telephone(ddHospital.SelectedIndex, Convert.ToInt32(tbLada.Text), tbPhone.Text);
            int res = t.registraTelefono();
            if (res == 0)
                lbPhone.Text = "Created successfully";
            else
                lbPhone.Text = "Register failed";
        }
    }
}