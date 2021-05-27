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

                //Lllenar dropdownlist de user id
                Users u = new Users();
                u.llenaDDLUsers(ddlUser);

                Conexion.llenarPais(ddPais);
                ddProv.Items.Add("Select country first");       
            }           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Hospital h = new Hospital(Convert.ToDouble(tbLat.Text), Convert.ToDouble(tbLon.Text), Convert.ToDouble(tbAlt.Text),
                tbNom.Text, tbDis.Text, ddProv.SelectedIndex, ddPais.SelectedIndex, tbTipo.Text); 

            tbLat.Text = ""; tbAlt.Text = ""; tbDis.Text = ""; ddPais.SelectedIndex = 0;
            tbLon.Text = ""; tbNom.Text = ""; tbTipo.Text = "";

            ddProv.Items.Clear(); ddProv.Items.Add("Select country first");

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

            if (id_hospital > 0 && ddlM1.SelectedIndex != 0 && ddlUser.SelectedIndex != 0)
            {
                lbUser.Text = ""; lbStatus.Text = ""; lbHos.Text = ""; lbRes.Text = "";

                String reserves1, reserves2, reserves3, reserves4, reserves5, reserves6, reserves7,
                reserves8, reserves9, reserves10, reserves11, reserves12;

                if (ddlReserves1.SelectedValue == "[Select an answer]")
                    reserves1 = "null";
                else
                    reserves1 = ddlReserves1.SelectedValue;
                if (ddlReserves2.SelectedValue == "[Select an answer]")
                    reserves2 = "null";
                else
                    reserves2 = ddlReserves2.SelectedValue;
                if (ddlReserves3.SelectedValue == "[Select an answer]")
                    reserves3 = "null";
                else
                    reserves3 = ddlReserves3.SelectedValue;
                if (ddlReserves4.SelectedValue == "[Select an answer]")
                    reserves4 = "null";
                else
                    reserves4 = ddlReserves4.SelectedValue;
                if (ddlReserves5.SelectedValue == "[Select an answer]")
                    reserves5 = "null";
                else
                    reserves5 = ddlReserves5.SelectedValue;
                if (ddlReserves6.SelectedValue == "[Select an answer]")
                    reserves6 = "null";
                else
                    reserves6 = ddlReserves6.SelectedValue;
                if (ddlReserves7.SelectedValue == "[Select an answer]")
                    reserves7 = "null";
                else
                    reserves7 = ddlReserves7.SelectedValue;
                if (ddlReserves8.SelectedValue == "[Select an answer]")
                    reserves8 = "null";
                else
                    reserves8 = ddlReserves8.SelectedValue;
                if (ddlReserves9.SelectedValue == "[Select an answer]")
                    reserves9 = "null";
                else
                    reserves9 = ddlReserves9.SelectedValue;
                if (ddlReserves10.SelectedValue == "[Select an answer]")
                    reserves10 = "null";
                else
                    reserves10 = ddlReserves10.SelectedValue;
                if (ddlReserves11.SelectedValue == "[Select an answer]")
                    reserves11 = "null";
                else
                    reserves11 = ddlReserves11.SelectedValue;
                if (ddlReserves12.SelectedValue == "[Select an answer]")
                    reserves12 = "null";
                else
                    reserves12 = ddlReserves12.SelectedValue;


                Inventory i = new Inventory(id_hospital, reserves1, reserves2, reserves3, reserves4, reserves5,
                    reserves6, reserves7, reserves8, reserves9, reserves10, reserves11, reserves12);
                int inventario = i.registraInventario();


                String screen = "false", campaign = "false", ability = "false", track = "false";
                string resources, tracking;

                if (ddlYN13.SelectedValue == "YES")
                    screen = "true";
                if (ddlYN14.SelectedValue == "YES")
                    campaign = "true";
                if (ddlYN15.SelectedValue == "YES")
                    ability = "true";
                if (ddlYN17.SelectedValue == "YES")
                    track = "true";
                if (ddlYN13.SelectedValue == "[Select an answer]")
                    screen = "null";
                if (ddlYN14.SelectedValue == "[Select an answer]")
                    campaign = "null";
                if (ddlYN15.SelectedValue == "[Select an answer]")
                    ability = "null";
                if (ddlYN17.SelectedValue == "[Select an answer]")
                    track = "null";
                if (ddlRecursos16.SelectedValue == "[Select an answer]")
                    resources = "null";
                else
                    resources = "'" + ddlRecursos16.SelectedValue + "'";
                if (ddlRastreo18.SelectedValue == "[Select an answer]")
                    tracking = "null";
                else
                    tracking = "'" + ddlRastreo18.SelectedValue + "'";

                Covid_protocol cp = new Covid_protocol(id_hospital, screen, campaign, ability, resources, track, tracking);
                int protocol = cp.registraProtocol();

                String numDoctors, numParamedics;

                if (tbDoctors.Text == "")
                    numDoctors = "null";
                else
                    numDoctors = tbDoctors.Text;
                if (tbParamedics.Text == "")
                    numParamedics = "null";
                else
                    numParamedics = tbParamedics.Text;

                Staff s = new Staff(id_hospital, numDoctors, numParamedics);
                int staff = s.registraStaff();

                //Recoleccion de datos para su posterior insercion en la tabla patient statistics
                String q21, q22, q23, q24, q25, q26;

                if (tbQ21.Text == "")
                    q21 = "null";
                else
                    q21 = tbQ21.Text;
                if (tbQ22.Text == "")
                    q22 = "null";
                else
                    q22 = tbQ22.Text;
                if (tbQ23.Text == "")
                    q23 = "null";
                else
                    q23 = tbQ23.Text;
                if (tbQ24.Text == "")
                    q24 = "null";
                else
                    q24 = tbQ24.Text;
                if (tbQ25.Text == "")
                    q25 = "null";
                else
                    q25 = tbQ25.Text;
                if (tbQ26.Text == "")
                    q26 = "null";
                else
                    q26 = tbQ26.Text;

                Patient_statistics ps = new Patient_statistics(id_hospital, q21, q22, q23, q24, q25, q26);
                int stats = ps.registraPatient_statistics();

                String problem, action, moph;

                if (tbMOPH.Text == "")
                    moph = "null";
                else
                    moph = tbMOPH.Text;
                if (ddlM2.SelectedValue.Equals("[Select an answer]"))
                    problem = "null";
                else
                    problem = "'" + ddlM2.SelectedValue + "'";
                if (ddlM3.SelectedValue.Equals("[Select an answer]"))
                    action = "null";
                else
                    action = "'" + ddlM3.SelectedValue + "'";


                Interview it = new Interview(id_hospital, Convert.ToInt16(ddlUser.SelectedValue), moph,
                    ddlM1.SelectedValue, problem, action);
                it.registraEntrevista();

                Hospital_structure hs = new Hospital_structure(id_hospital, inventario, stats, protocol, staff);
                hs.registraStructure();

                lbRes.Text = "Registro exitoso";
            }
            else
            {
                lbRes.Text = "Faltan alguno de los datos obligatorios: hospital, status, o id usuario";
                if (id_hospital <= 0)
                    lbHos.Text = "* Obligatorio";
                if (ddlM1.SelectedIndex == 0)
                    lbStatus.Text = "* Obligatorio";
                if (ddlUser.SelectedIndex == 0)
                    lbUser.Text = "* Obligatorio";
            }
            
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

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (ddPais.SelectedIndex != 0)
            {
                ddProv.Items.Clear();
                Conexion.llenarProvincia(ddProv, ddPais.SelectedIndex);
            }
        }
    }
}
