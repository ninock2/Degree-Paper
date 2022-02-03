<%@ Page Title="O aplikaciji" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>O aplikaciji</h2>
    <hr style="width: 900px;" />
    <h3><b>Diplomsko delo</b></h3>
    <h3>Spletna aplikacija na osnovi ASP.NET - Spletna knjižnica</h3>
    <br />
    <h4 style="width: 500px; margin: 15px auto; text-align:left;">Funkcionalnosti uporabnika:</h4>
    <div class="o-app" style="text-align: left; width: 500px; margin: 15px auto;">
        <ul>
            <li>
                <p>Pregled in rezervacija vseh knjig, ki so na razpolago,</p>
            </li>
            <li>
                <p>Sprejemanje statusa rezervacije, izoposoje in vračila,</p>
            </li>
            <li>
                <p>Prejemanje obvestil na mail,</p>
            </li>
            <li>
                <p>Pregled obvesil, ki jih uporabnik prejme.</p>
            </li>
        </ul>
    </div>
        <h4 style="width: 500px; margin: 15px auto; text-align:left;">Funkcionalnosti administratorja:</h4>
    <div class="o-app" style="text-align: left; width: 500px; margin: 15px auto;">
        <ul>
            <li>
                <p>Upravljanje knjig</p>
            </li>
            <li>
                <p>Upravljanje dogodkov (oddana rezervacija, rezervacija, izposoja in vračilo)</p>
            </li>
            <li>
                <p>Pregled vseh obvestil</p>
            </li>
            <li>
                <p>Upravljanje uporabnikov</p>
            </li>
        </ul>
        </div>
        <h4 style="width: 500px; margin: 15px auto; text-align:left;">Baza podatkov:</h4>
    <div class="o-app" style="text-align: left; width: 500px; margin: 15px auto;">
        <ul>
            <li>
                <p>dbo.Knjige</p>
            </li>
            <li>
                <p>dbo.RegistriraniUserji</p>
            </li>
            <li>
                <p>dbo.Eventi</p>
            </li>
        </ul>

        <br />
        <h4 style="width: 500px; margin: 15px auto; text-align:left;">Namen spletne aplikacije:</h4>
        <p>
            Spletna knjižnica je namenjena ljudem, ki si raje v odobju svojega doma pregledajo vse knjige in izbrane tudi rezervirajo.
        </p>
    </div>
</asp:Content>
