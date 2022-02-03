<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Prijava.aspx.cs" Inherits="Prijava"  %>

<!DOCTYPE html>

<link rel="stylesheet" href="stili.css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Prijava v aplikacijo</title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link rel="stylesheet" href="stili.css" />
</head>

<body class="prijava-body">
    <form id="form1" runat="server">

        <%-- Skripte za navbar --%>
        <asp:ScriptManager runat="server">
            <Scripts>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
            </Scripts>
        </asp:ScriptManager>

        <%-- Navbar - navigacija po straneh --%>
        <%--<div class="navbar navbar-inverse navbar-fixed-top" id="navbar">
            <div class="contain" id="contain" style="padding-left: 100px; padding-right: 100px;">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" runat="server" href="~/">RPA Seminarska naloga</a>
                </div>
                <div class="navbar-collapse collapse">
                    <ul class="nav navbar-nav">
                        <li><a runat="server" href="~/">Domov</a></li>
                        <li><a runat="server" href="~/About">O aplikaciji</a></li>
                        <li><a runat="server" id="rezervirajKnjigo" href="~/KnjigeNaVoljo">Upravljanje z knjigami</a></li>
                        <li><a runat="server" id="A1" href="~/RezervirajKnjigo">Rezerviraj knjigo</a></li>
                        <li><a runat="server" href="~/Contact">Contact</a></li>
                    </ul>
                    <asp:LoginView runat="server" ViewStateMode="Disabled">
                        <AnonymousTemplate>
                            <ul class="nav navbar-nav navbar-right">
                                <li><a runat="server" href="~/Prijava.aspx" id="odjavaBtn" visible="false">Odjava</a></li>
                                <li><a runat="server" href="~/Registracija.aspx" id="registracijaBtn" visible="true"><span>Registracija</a></li>
                                <li><a runat="server" href="~/Prijava.aspx" id="prijavaBtn" visible="false"><span>Prijava</a></li>
                                <li><a runat="server" href="~/AdminPrijava.aspx" id="adminPrijavBtn" visible="true"><span>Admin prijava</a></li>
                            </ul>
                        </AnonymousTemplate>
                    </asp:LoginView>
                </div>
            </div>
        </div>--%>
        <%-- Konec navbar --%>

        <h1 class="prijava-naslov">Prijava v aplikacijo</h1>

        <div class="div-formaprijave">
            <table class="forma-prijava">
                <tr>
                    <td>Uporabniško ime: </td>
                    <td>
                        <asp:TextBox ID="txtUserIme" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Geslo: </td>
                    <td>
                        <asp:TextBox ID="txtUserGeslo" runat="server" TextMode="Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnPrijavi" runat="server" Text="Prijavi" Height="36px" Width="107px" OnClick="btnPrijavi_Click" />
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Table]"></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
