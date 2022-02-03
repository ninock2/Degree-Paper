<%@ Page MasterPageFile="~/Administrator.master" Language="C#" AutoEventWireup="true" CodeFile="UpravljanjeObvestil.aspx.cs" Inherits="UpravljenjeObvestil" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <!DOCTYPE html>

    <html>
    <head>
        <title></title>
        <webopt:BundleReference runat="server" Path="~/Content/css" />
        <link rel="stylesheet" type="text/css" href="stili.css" />
        <style type="text/css">
            .auto-style4 {
                width: 829px;
            }

            .auto-style5 {
                width: 41px;
            }

            .auto-style6 {
                width: 647px;
            }

            .auto-style7 {
                height: 546px;
                width: 1170px;
                text-align: center;
                margin-left: auto;
                margin-right: auto;
                margin-top: 0px;
                padding-left: 15px;
                padding-right: 15px;
            }

            .Initial {
                display: block;
                padding: 4px 18px 4px 18px;
                float: left;
                background-color: transparent;
                color: Black;
                font-weight: bold;
                font-size: large;
                border-color: black;
                border-bottom: 1px solid black;
                border-left: 1px solid black;
                border-right: 1px solid black;
            }

                .Initial:hover {
                    color: lightblue;
                    background-color: transparent;
                    font-size: large;
                }

            .Clicked {
                float: left;
                display: block;
                padding: 4px 18px 4px 18px;
                color: Blue;
                background-color: transparent;
                font-weight: bold;
                font-size: large;
            }
        </style>
    </head>
    <body>

        <hr style="width: 30%;" />
        <h1>Pregled vseh obvestil</h1>
        <hr style="width: 100%;" />
        <table align="center">
            <tr>
                <td>
                    <asp:Button Text="Opozorila" BorderStyle="None" ID="Tab1" CssClass="Clicked" runat="server"
                        OnClick="Tab1_Click" />
                    <asp:Button Text="Opomini" BorderStyle="None" ID="Tab2" CssClass="Initial" runat="server"
                        OnClick="Tab2_Click" />
                    <asp:Button Text="Zamudnine" BorderStyle="None" ID="Tab3" CssClass="Initial" runat="server"
                        OnClick="Tab3_Click" />
                </td>
            </tr>
        </table>
        <hr style="width: 30%;" />
        <asp:MultiView ID="MainView" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <h3 style="text-align: left;"><b>Opozorila</b></h3>
                        <h4 style="text-align: left;">Danes je zadnji dan za oddajo naslednjih knjig.</h4>
                        <hr />
                        <div id="divOpozorila">
                            <table style='table-layout: fixed; background: #F0FFFF; width: 70%; border-radius: 2em / 5em'>
                                <tr>
                                    <td><b>Izposoja ID </b></td>
                                    <td><b>User ID </b></td>
                                    <td><b>Knjiga ID </b></td>
                                    <td><b>Datum izposoje </b></td>
                                    <td><b>Dni izposoje </b></td>
                                </tr>
                            </table>
                            <br />
                            <div style="height: 500px; overflow-y: auto; overflow-x: hidden;" draggable="true">
                                <%= htmlOpozorilo %>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
            </asp:View>

            <asp:View ID="View2" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <h3 style="text-align: left;"><b>Opomini</b></h3>
                        <h4 style="text-align: left;">Knjige, katerih je rok izposoje že potekel.</h4>
                        <hr style="width: 29%;" />
                        <table style='table-layout: fixed; background: #F0FFFF; width: 80%; border-radius: 2em / 5em'>
                            <tr>
                                <td><b>Izposoja ID </b></td>
                                <td><b>User ID </b></td>
                                <td><b>Knjiga ID </b></td>
                                <td><b>Datum izposoje </b></td>
                                <td><b>Dni izposoje </b></td>
                                <td><b>Dnevni penal </b></td>
                            </tr>
                        </table>
                        <br />
                        <div style="height: 500px; overflow-y: auto; overflow-x: hidden;" draggable="true">
                            <%= htmlOpomnik %>
                        </div>
                    </div>
                </div>
                <br />
            </asp:View>

            <asp:View ID="View3" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <h3 style="text-align: left;"><b>Zamudnine</b></h3>
                        <h4 style="text-align: left;">Odprte zamudnine, ki jih je potrebno poravnati.</h4>
                        <hr style="width: 29%;" />
                        <table style='table-layout: fixed; background: #F0FFFF; width: 90%; border-radius: 2em / 5em'>
                            <tr>
                                <td><b>Zamudnina ID </b></td>
                                <td><b>User ID </b></td>
                                <td><b>Knjiga ID </b></td>
                                <td><b>Datum izposoje </b></td>
                                <td><b>Datum vračila </b></td>
                                <td><b>Vsota </b></td>
                                <td><b>Status </b></td>
                            </tr>
                        </table>
                        <br />
                        <div style="height: 500px; overflow-y: auto; overflow-x: hidden;" draggable="true">
                            <%= htmlZamudnina %>
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>
    </body>
    </html>
</asp:Content>
