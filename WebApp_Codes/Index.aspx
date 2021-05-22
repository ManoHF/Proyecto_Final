<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Proyecto_BD.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        input::-webkit-outer-spin-button,
        input::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }
  
        input[type=number] {
            -moz-appearance: textfield;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="background-color:slategrey">
            <center>
                <h1 style="color:cyan; font-family:'Courier New'">
                    Hospitals' COVID Questionnaire
                </h1>
            </center>
            <h3 style="color:white; font-family:'Courier New'">
                <asp:Label ID="lbRes" runat="server" Text="" ForeColor="Red" Font-Size="Large"></asp:Label><br />
                Select your hospital:
            </h3>
            <asp:DropDownList ID="ddHospital" runat="server" Height="30px" Width="442px"> 
            </asp:DropDownList> <asp:Label ID="lbHos" runat="server" Text="" ForeColor="Red" Font-Size="Large"></asp:Label> <br />
            <h3 style="color:white; font-family:'Courier New'">
                If it is not in the list, register it here: <br /><br />
                Latitude:&nbsp;&nbsp;&nbsp;  <asp:TextBox ID="tbLat" runat="server"></asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="tbNom" runat="server"></asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Country: <asp:TextBox ID="tbPa" runat="server"></asp:TextBox> <br />
                Lengths:&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="tbLon" runat="server"></asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Province:&nbsp; <asp:TextBox ID="tbPro" runat="server"></asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Type:&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="tbTipo" runat="server"></asp:TextBox>
                <br />
                Altitude:&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="tbAlt" runat="server"> </asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; District:&nbsp;&nbsp;<asp:TextBox ID="tbDis" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </h3 >
            <h3 style="color:black; font-family:'Courier New'">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Register hospital" Height="33px" Width="167px" />
&nbsp;</h3 >
            <h3 style="color:black; font-family:'Courier New'">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="lbRegistro" runat="server"></asp:Label>
                <br />
            </h3 >
            <h3 style="color:white; font-family:'Courier New'">
                Phone number (10 digits):&nbsp;&nbsp;&nbsp;  
                <asp:TextBox ID="tbPhone" runat="server" TextMode="Number" onkeypress="return this.value.length<=9" ></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btNewPhone" runat="server" Height="34px" Text="Register phone" Width="132px" OnClick="btNewPhone_Click" />
&nbsp;<asp:Button ID="btUpdate" runat="server" Height="34px" Text="Update" Width="132px" OnClick="btUpdate_Click" /> <br />
                Lada:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="tbLada" runat="server" TextMode="Number" onkeypress="return this.value.length<=2" ></asp:TextBox>
                <h3 style="color:black; font-family:'Courier New'">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="lbPhone" runat="server"></asp:Label>
                <br />
            </h3 >
            <hr />
            <h1 style="color:white; font-family:'Courier New'; color:cyan"> Available reserves (disponibility days)</h1>
            <hr>
            <h2 style="color:white; font-family:'Courier New'">
                1) Oxygen reserves (O2) <br /><br />
                <asp:DropDownList ID="ddlReserves1" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                2) Antipyretics (paracetamol) <br /><br />
                <asp:DropDownList ID="ddlReserves2" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                3) Anestesthic drugs / muscular relaxant <br /><br />
                <asp:DropDownList ID="ddlReserves3" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                4) Alcohol solutions (> 70°) and soap for handwashing  <br /><br />
                <asp:DropDownList ID="ddlReserves4" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                <u> Protection material for personnel </u> <br /><br />

                5) Disposable masks (minumum P2) <br /><br />
                <asp:DropDownList ID="ddlReserves5" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList> 
                <br /><br />

                6) Disposable vynil gloves <br /><br />
                <asp:DropDownList ID="ddlReserves6" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList> 
                <br /><br />

                7) Disposable hats <br /><br />
                <asp:DropDownList ID="ddlReserves7" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList> 
                <br /><br />

                8) Disposable aprons <br /><br />
                <asp:DropDownList ID="ddlReserves8" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                9) Surgical gloves <br /><br />
                <asp:DropDownList ID="ddlReserves9" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                10) Visors <br /><br />
               <asp:DropDownList ID="ddlReserves10" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                11) Disposable shoe covers <br /><br />
                <asp:DropDownList ID="ddlReserves11" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                <u> COVID </u> <br /><br />

                12) COVID test kits <br /><br />
                 <asp:DropDownList ID="ddlReserves12" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

            </h2>
            <hr />
            <h1 style="color:white; font-family:'Courier New'; color:cyan"> 
                Data & protocols: COVID-19
            </h1>
            <hr />
            <h2 style="color:white; font-family:'Courier New'"> 

                13) Has a screening of COVID patients, based on symptoms, been implemented at hospital arrival? <br /><br />
                <asp:DropDownList ID="ddlYN13" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                14) Have there been awareness campaigns on COVID prevention in this hospital? <br /><br />
               <asp:DropDownList ID="ddlYN14" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                 15) Does your health center currently have the ability to test patients for COVID-19? <br /><br />
                <asp:DropDownList ID="ddlYN15" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                16) Have you received COVID-19 related resources in the last one month?  <br /><br />
                <asp:DropDownList ID="ddlRecursos16" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                17) Do you regularly track COVID19 cases in your health facility? <br /><br />
                <asp:DropDownList ID="ddlYN17" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                18) How often to you report your COVID19 tracking results to the MOPH?, every __ days: <br /><br />
                <asp:DropDownList ID="ddlRastreo18" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                19) Number of doctors working in this health facility/ hospital: <br /><br />
                <asp:TextBox ID="tbDoctors" runat="server" Height="35px" Width="350px" TextMode="Number" Font-Size="Large"></asp:TextBox> 
                <br /><br />

                20) Number of paramedical staff working in this health facility/ hospital: <br /><br />
                <asp:TextBox ID="tbParamedics" runat="server" Height="35px" Width="350px" TextMode="Number" Font-Size="Large"></asp:TextBox>
                <br /><br />

                <u> On the next questions, if you do not know the answer, leave it blank  </u><br /><br />

                21) Last month, how many patients with COVID symptoms have you observed? (fever, cough, shortness of breath) <br /><br />
                <asp:TextBox ID="tbQ21" runat="server" Height="35px" Width="350px" TextMode="Number" Font-Size="Large"></asp:TextBox>
                <br /><br />

                22) Last month, how many patients have tested positive for COVID? <br /><br />
                <asp:TextBox ID="tbQ22" runat="server" Height="35px" Width="350px" TextMode="Number" Font-Size="Large"></asp:TextBox>
                <br /><br />

                23) Last month, how many patients are in intensive care with COVID symptoms and/or testing positive for COVID in this hospital? <br /><br />
                <asp:TextBox ID="tbQ23" runat="server" Height="35px" Width="350px" TextMode="Number" Font-Size="Large"></asp:TextBox>
                <br /><br />

                24) Number of deaths from COVID-19 during the last month in this hospital: <br /><br />
                <asp:TextBox ID="tbQ24" runat="server" Height="35px" Width="350px" TextMode="Number" Font-Size="Large"></asp:TextBox>
                <br /><br />

                25) Number of non-COVID-19 deaths during last month: <br /><br />
                <asp:TextBox ID="tbQ25" runat="server" Height="35px" Width="350px" TextMode="Number" Font-Size="Large"></asp:TextBox>
                <br /><br />

                26) Number of patients recovered from COVID-19 out of hospital during the last month: <br /><br />
                <asp:TextBox ID="tbQ26" runat="server" Height="35px" Width="350px" TextMode="Number" Font-Size="Large"></asp:TextBox>
                <br /><br />

            </h2>
            <hr />
            <h1 style="color:white; font-family:'Courier New'; color:cyan">
                Monitoring questions:
            </h1>
            <hr />
            <h2 style="color:white; font-family:'Courier New'">
                Ministry of Public Health number: <asp:TextBox ID="tbMOPH" runat="server" TextMode="Number"></asp:TextBox> <br /> <br />

                Questionnaire status: <br /><br />
                <asp:DropDownList ID="ddlM1" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList> <asp:Label ID="lbStatus" runat="server" Text="" ForeColor="Red" Font-Size="Large"></asp:Label>
                <br /><br />

                Problems: <br /><br />
                <asp:DropDownList ID="ddlM2" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

                Actions: <br /><br />
                <asp:DropDownList ID="ddlM3" runat="server" Height="40px" Width="382px" Font-Size="Large"></asp:DropDownList>
                <br /><br />

            </h2>
            <h2 style="color:white; font-family:'Courier New'">
                Select your User_ID:
                <asp:DropDownList ID="ddlUser" runat="server" Height="40px" Width="194px" Font-Size="Large"></asp:DropDownList><asp:Label ID="lbUser" runat="server" Text="" ForeColor="Red" Font-Size="Large"></asp:Label>
                <br /><br />
            </h2>
            <center>
                <asp:Button ID="Button2" runat="server" Text="Send" Height="45px" Width="167px" OnClick="Button2_Click" />
            </center>
        </div>
    </form>
</body>
</html>
