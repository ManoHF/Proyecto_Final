<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Proyecto_BD.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="background-color:black">
            <center>
                <h1 style="color:white">
                    Cuestionario COVID para hospitales
                </h1>
            </center>
            <h3 style="color:white">
                Selecciona tu hospital:
            </h3>
            <asp:DropDownList ID="ddHospital" runat="server" Height="30px" Width="442px">
            </asp:DropDownList> <br />
            <h3 style="color:white">
                ¿No lo encuentras?, regístralo: <br /><br />
                Latitud:&nbsp;&nbsp;&nbsp;  <asp:TextBox ID="tbLat" runat="server"></asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Nombre&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="tbNom" runat="server"></asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; País <asp:TextBox ID="tbPa" runat="server"></asp:TextBox> <br />
                Longitud: <asp:TextBox ID="tbLon" runat="server"></asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Provincia&nbsp;&nbsp; <asp:TextBox ID="tbPro" runat="server"></asp:TextBox> <br />
                Altura:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   <asp:TextBox ID="tbAlt" runat="server"> </asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Distrito&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="tbDis" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Registrar" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lbRegistro" runat="server"></asp:Label>
                <br />
            </h3 >
            <hr />
            <h1 style="color:white"> Reservas disponibles (días de disponibilidad)</h1>
            <hr>
            <h2 style="color:white">
                1) Oxígeno (O2) <br /><br />
                <asp:DropDownList ID="ddlReserves1" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                2) Antipirético (paracetamol) <br /><br />
                <asp:DropDownList ID="ddlReserves2" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                3) Medicinas anestésicas / relajantes musculares <br /><br />
                <asp:DropDownList ID="ddlReserves3" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                4) Soluciones alcohólicas (&gt; 70°) y jabón para manos <br /><br />
                <asp:DropDownList ID="ddlReserves4" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                <u> Material de protección para el personal </u> <br /><br />

                5) Máscaras desechables (mínimo P2) <br /><br />
                <asp:DropDownList ID="ddlReserves5" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList> 
                <br /><br />

                6) Guantes desechables de vinilo <br /><br />
                <asp:DropDownList ID="ddlReserves6" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList> 
                <br /><br />

                7) Sombreros médicos desechables <br /><br />
                <asp:DropDownList ID="ddlReserves7" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList> 
                <br /><br />

                8) Delantales desechables <br /><br />
                <asp:DropDownList ID="ddlReserves8" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                9) Viseras <br /><br />
               <asp:DropDownList ID="ddlReserves9" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                10) Fundas desechables para zapatos <br /><br />
                <asp:DropDownList ID="ddlReserves10" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                <u> COVID </u> <br /><br />

                11) Pruebas de COVID <br /><br />
                 <asp:DropDownList ID="ddlReserves11" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

            </h2>
            <hr />
            <h1 style="color:white"> 
                Protocolos y datos: COVID-19
            </h1>
            <hr />
            <h2 style="color:white"> 

                12) Se tiene implementado un cribado para pacientes COVID, basado en síntomas, durante la llegada al hospital? <br /><br />
                <asp:DropDownList ID="ddlYN12" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                13) Ha habido campañas de consciencia para la prevención de COVID en este hospital? <br /><br />
               <asp:DropDownList ID="ddlYN13" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                 14) Tiene la capacidad de aplicar pruebas de COVID en su centro de salud? <br /><br />
                <asp:DropDownList ID="ddlYN14" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                15) Ha recibido recursos para COVID en el último mes? <br /><br />
                <asp:DropDownList ID="ddlRecursos15" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                16) Rastrea regularmente casos de COVID en su centro de salud? <br /><br />
                <asp:DropDownList ID="ddlYN16" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                17) Qué tan seguido reporta sus rastreos de COVID al ministerio de salud pública, cada: <br /><br />
                <asp:DropDownList ID="ddlRastreo17" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                18) Número de kits de prueba COVID disponibles en el hospital: <br /><br />
                 <asp:DropDownList ID="ddlNum18" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                19) Número de aparatos respiratorios en funcionamiento: <br /><br />
                 <asp:DropDownList ID="ddlNum19" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                20) Número de doctores trabajando en el hospital: <br /><br />
                 <asp:DropDownList ID="ddlNum20" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                21) Número de staff paramédico trabajando en el hospital: <br /><br />
                 <asp:DropDownList ID="ddlNum21" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                22)Qué tan rápida fue en promedio la entrega de resultados de prueba COVID durante el último mes? <br /><br />
                <asp:DropDownList ID="ddlNum22" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                <u> En las siguiente preguntas, si no dispone o no sabe la información, seleccione 9  </u><br /><br />

                23)Número de casos COVID mandados a PHC? <br /><br />
                <asp:DropDownList ID="ddlNum23" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                24)En el último mes, cuántos pacientes ha observado con síntomas de COVID (fiebre, tos)? <br /><br />
                <asp:DropDownList ID="ddlNum24" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                25) El último mes, cuátos pacientes dieron positivo por COVID? <br /><br />
                <asp:DropDownList ID="ddlNum25" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                26) El último mes, cuántos pacientes se tuvo positivos por COVID o en cuidados especiales por COVID? <br /><br />
                <asp:DropDownList ID="ddlNum26" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                27) Número de muertes de COVID en el hospital el último mes: <br /><br />
                <asp:DropDownList ID="ddlNum27" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                28) Número de muertes NO COVID en el último mes: <br /><br />
                <asp:DropDownList ID="ddlNum28" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                29) Número de pacientes que se recuperaron de COVID fuera del hospital en el último mes: <br /><br />
                <asp:DropDownList ID="ddlNum29" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

            </h2>
            <hr />
            <h1 style="color:white">
                Preguntas de Monitoreo
            </h1>
            <hr />
            <h2 style="color:white">
                Número de Ministerio de Salud Publica: <asp:TextBox ID="tbMOPH" runat="server"></asp:TextBox> <br /> <br />

                Status del cuestionario: <br /><br />
                <asp:DropDownList ID="ddlM1" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                Problemas encontrados: <br /><br />
                <asp:DropDownList ID="ddlM2" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                Acciones a tomar: <br /><br />
                <asp:DropDownList ID="ddlM3" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

            </h2>
        </div>
    </form>
</body>
</html>
