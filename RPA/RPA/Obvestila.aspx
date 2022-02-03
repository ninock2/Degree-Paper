<%@ Page Title="Obvestila" MasterPageFile="~/Uporabnik.master" Language="C#" AutoEventWireup="true" CodeFile="Obvestila.aspx.cs" Inherits="Obvestila" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <!DOCTYPE html>

    <html>
    <head>
        <title></title>
        <webopt:BundleReference runat="server" Path="~/Content/css" />
        <link rel="stylesheet" type="text/css" href="stili.css" />
        <style type="text/css"></style>
    </head>
    <body>
        <form id="form1">
            <h1>Moja obvestila</h1>
            <hr style="width: 100%;" />
            <h3 style="text-align: left;"><b>Opozorila</b></h3>
            <h4 style="text-align: left;">Danes je zadnji dan za oddajo naslednjih knjig.</h4>
            <hr />
            <div id="divOpozorila">
                <table style='table-layout: fixed; background: #F0FFFF; width: 70%; border-radius: 2em / 5em'>
                    <tr>
                        <td><b>Izposoja ID </b></td>
                        <td><b>Naslov knjige </b></td>
                        <td><b>Datum izposoje </b></td>
                        <td><b>Dni izposoje </b></td>
                    </tr>
                </table>
                <br />
                <%= htmlOpozorilo %>
            </div>

            <br />

            <h3 style="text-align: left;"><b>Opomini</b></h3>
            <h4 style="text-align: left;">Knjige, katerih je rok izposoje že potekel.</h4>
            <hr style="width: 29%;" />
            <div id="divOpomini">
                <table style='table-layout: fixed; background: #F0FFFF; width: 80%; border-radius: 2em / 5em'>
                    <tr>
                        <td><b>Izposoja ID </b></td>
                        <td><b>Naslov knjige </b></td>
                        <td><b>Datum izposoje </b></td>
                        <td><b>Dni izposoje </b></td>
                        <td><b>Dnevni penal </b></td>
                    </tr>
                </table>
                <br />
                <%= htmlOpomnik %>
            </div>
            <br />

            <h3 style="text-align: left;"><b>Zamudnine</b></h3>
            <h4 style="text-align: left;">Odprte zamudnine, ki jih je potrebno poravnati.</h4>
            <hr style="width: 29%;" />
            <div id="divZamudnine">
                <table style='table-layout: fixed; background: #F0FFFF; width: 90%; border-radius: 2em / 5em'>
                    <tr>
                        <td><b>Zamudnina ID </b></td>
                        <td><b>Naslov knjige </b></td>
                        <td><b>Datum izposoje </b></td>
                        <td><b>Datum vračila </b></td>
                        <td><b>Vsota </b></td>
                        <td><b>Status </b></td>
                    </tr>
                </table>
                <br />

                <%= htmlZamudnina %>
            </div>
            <br />
        </form>
    </body>
    </html>
</asp:Content>
