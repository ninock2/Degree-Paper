<%@ Page Title="Upravljanje rezervacij" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UpravljanjeRezervacij.aspx.cs" Inherits="UpravljanjeRezervacij" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">

    <!DOCTYPE html>

    <html>
    <head>
        <title></title>

        <link rel="stylesheet" type="text/css" href="stili.css" />
        <style type="text/css">
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

            .tblRezervacija {
                margin: 50px 20px auto;
                width: 100%;
                text-align: left;
                align-content: center;
            }
        </style>
    </head>
    <body>
        <body>
            <h2>Upravljanje dogodkov</h2>
            <hr style="width: 100%;" />
            <form>
                <table align="center">
                    <tr>
                        <td>
                            <asp:Button Text="Rezervacije" BorderStyle="None" ID="Tab1" CssClass="Clicked" runat="server"
                                OnClick="Tab1_Click" />
                            <asp:Button Text="Izposoje in Vračila" BorderStyle="None" ID="Tab2" CssClass="Initial" runat="server"
                                OnClick="Tab2_Click" />
                            <asp:Button Text="Zamudnine" BorderStyle="None" ID="Tab3" CssClass="Initial" runat="server"
                                OnClick="Tab3_Click" />
                        </td>
                    </tr>
                </table>
                <hr style="width: 30%;" />
                <asp:MultiView ID="MainView" runat="server" ActiveViewIndex="0">
                    <asp:View ID="View1" runat="server">
                        <div class="tblRezervacija">
                            <h3>Preklicane rezervacije</h3>
                            <hr style="width: 20%" />
                            <%-- VIEW1 --%>
                            <div style="width: 700px; height: 250px; overflow-y: auto; overflow-x: hidden;" draggable="true">
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource2" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Horizontal" Width="700px" AllowSorting="True">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                                        <asp:BoundField DataField="id_user" HeaderText="ID Uporabnika" SortExpression="id_user" />
                                        <asp:BoundField DataField="id_knjige" HeaderText="ID Knjige" SortExpression="id_knjige" />
                                        <asp:BoundField DataField="datumRezervacije" HeaderText="Datum rezervacije" SortExpression="datumRezervacije" DataFormatString="{0:dd.MM.yyyy}" />
                                        <asp:BoundField DataField="datumPreklica" HeaderText="Datum preklica" SortExpression="datumPreklica" DataFormatString="{0:dd.MM.yyyy}" />
                                        <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:rpa_seminarska_nalogaConnectionString %>"
                                    SelectCommand="SELECT * FROM [Rezervacije] WHERE datumPreklica IS NOT NULL"></asp:SqlDataSource>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-8">
                                <div class="tblRezervacija">
                                    <h3>Aktivne rezervacije</h3>
                                    <hr style="width: 20%" />
                                    <div style="width: 700px; height: 250px; overflow-y: auto; overflow-x: hidden;" draggable="true">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" Width="700px" GridLines="Horizontal" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AllowSorting="True">
                                            <AlternatingRowStyle BackColor="#CCCCCC" />
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" SelectText="Izberi" />
                                                <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                                                <asp:BoundField DataField="id_user" HeaderText="ID Uporabnika" SortExpression="id_user" />
                                                <asp:BoundField DataField="id_knjige" HeaderText="ID Knjige" SortExpression="id_knjige" />
                                                <asp:BoundField DataField="datumRezervacije" HeaderText="Datum rezervacije" SortExpression="datumRezervacije" DataFormatString="{0:dd.MM.yyyy}" />
                                                <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                                            </Columns>
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:rpa_seminarska_nalogaConnectionString %>"
                                            SelectCommand="SELECT [id], [id_user], [id_knjige], [datumRezervacije], [status] FROM [Rezervacije] WHERE datumPreklica IS NULL"></asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4" style="margin: 70px auto;">
                                <div style="text-align: center; align-content: center;">
                                    <h3 style="text-decoration: underline; width: auto;"><b>Izvedi rezervacijo</b></h3>
                                    <div class="row" style="text-align: center; width: auto;">
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-4" style="text-align: right; width: auto; margin: 10px auto; font-size: 14px; font-weight: bold;">
                                            <asp:Label ID="Label1" runat="server" Text="ID Rezervacije:"></asp:Label>
                                            <asp:TextBox ID="textBoxIdRezervacije" runat="server" Enabled="False"></asp:TextBox>
                                            <br />
                                            <asp:Label ID="labelIdUser" runat="server" Text="ID Uporabnika:"></asp:Label>
                                            <asp:TextBox ID="textBoxIdUporabnika" runat="server" Enabled="False"></asp:TextBox>
                                            <br />
                                            <asp:Label ID="labelIdKnjige" runat="server" Text="ID Knjige:"></asp:Label>
                                            <asp:TextBox ID="textBoxIdKnjige" runat="server" Enabled="False"></asp:TextBox>
                                            <br />
                                            <asp:Label ID="labelStatus" runat="server" Text="Status:"></asp:Label>
                                            <asp:TextBox ID="textBoxStatus" runat="server" Enabled="False" Text="izvedena"></asp:TextBox>
                                            <br />
                                            <asp:Label ID="label2" runat="server" Text="Dnevni penal (€):"></asp:Label>
                                            <asp:DropDownList ID="DropDownDnevniPenalty" runat="server" Width="197px">
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <asp:Label ID="labelDniIzposoje" runat="server" Text="Število dni izposoje:"></asp:Label>
                                            <asp:DropDownList ID="DropDownListDniIzposoje" runat="server" Width="197px">
                                                <asp:ListItem>14</asp:ListItem>
                                                <asp:ListItem>21</asp:ListItem>
                                                <asp:ListItem>28</asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <asp:Label ID="labelDanes" runat="server" Text="Datum rezervacije:"></asp:Label>
                                            <asp:TextBox ID="textBoxDatumRezervacije" runat="server" Enabled="False"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Shrani spremebo statusa" />
                                </div>
                            </div>
                        </div>

                    </asp:View>
                    <asp:View ID="View2" runat="server">

                        <div class="tblRezervacija">
                            <h3>Opravljena vračila</h3>
                            <hr style="width: 20%" />
                            <br />
                            <%-- VIEW2 --%>
                            <div style="width: 700px; height: 250px; overflow-y: auto; overflow-x: hidden;" draggable="true">
                                <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="700px" DataKeyNames="id" DataSourceID="SqlDataSource4" ForeColor="Black" GridLines="Horizontal" AllowSorting="True">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                                        <asp:BoundField DataField="id_user" HeaderText="ID Uporabnika" SortExpression="id_user" />
                                        <asp:BoundField DataField="id_knjige" HeaderText="ID Knjige" SortExpression="id_knjige" />
                                        <asp:BoundField DataField="id_rezervacije" HeaderText="ID Rezervacije" SortExpression="id_rezervacije" />
                                        <asp:BoundField DataField="datumIzposoje" DataFormatString="{0:dd.MM.yyyy}" HeaderText="Datum izposoje" SortExpression="datumIzposoje" />
                                        <asp:BoundField DataField="datumVracila" DataFormatString="{0:dd.MM.yyyy}" HeaderText="Datum vracila" SortExpression="datumVracila" />
                                        <asp:BoundField DataField="dniIzposoje" HeaderText="dniIzposoje" SortExpression="Dni izposoje" />
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:rpa_seminarska_nalogaConnectionString %>" SelectCommand="SELECT [id], [id_user], [id_knjige], [id_rezervacije], [datumIzposoje], [datumVracila], [dniIzposoje] FROM [Izposoje]"></asp:SqlDataSource>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-8">
                                <div class="tblRezervacija">
                                    <h3>Aktivne izposoje</h3>
                                    <hr style="width: 20%" />
                                    <div style="width: 700px; height: 250px; overflow-y: auto; overflow-x: hidden;" draggable="true">
                                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" DataKeyNames="id" DataSourceID="SqlDataSource3" ForeColor="Black" GridLines="Horizontal" OnSelectedIndexChanged="GridView3_SelectedIndexChanged" Width="700px" AllowSorting="True">
                                            <AlternatingRowStyle BackColor="#CCCCCC" />
                                            <Columns>
                                                <asp:CommandField SelectText="Izberi" ShowSelectButton="True" />
                                                <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                                                <asp:BoundField DataField="id_user" HeaderText="ID Uporabnika" SortExpression="id_user" />
                                                <asp:BoundField DataField="id_knjige" HeaderText="ID Knjige" SortExpression="id_knjige" />
                                                <asp:BoundField DataField="id_rezervacije" HeaderText="ID Rezervacije" SortExpression="id_rezervacije" />
                                                <asp:BoundField DataField="datumIzposoje" DataFormatString="{0:dd.MM.yyyy}" HeaderText="Datum izposoje" SortExpression="datumIzposoje" />
                                                <asp:BoundField DataField="dniIzposoje" HeaderText="Max. št. dni izposoje" SortExpression="dniIzposoje" />
                                            </Columns>
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="Gray" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:rpa_seminarska_nalogaConnectionString %>" SelectCommand="SELECT [id], [id_user], [id_knjige], [id_rezervacije], [datumIzposoje], [dniIzposoje] FROM [Izposoje]"></asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div style="text-align: center; align-content: center;">
                                    <h3 style="text-decoration: underline; width: auto;"><b>Zaključi izposojo (vračilo)</b></h3>
                                    <div class="row" style="text-align: center; width: auto;">
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-4" style="text-align: right; width: auto; margin: 10px auto; font-size: 14px; font-weight: bold;">
                                            <asp:Label ID="LabelIDIzposoje" runat="server" Text="ID Izposoje:"></asp:Label>
                                            <asp:TextBox ID="textBoxIDIZposoje" runat="server" Enabled="False"></asp:TextBox>
                                            <br />
                                            <asp:Label ID="labelUserID" runat="server" Text="ID Uporabnika:"></asp:Label>
                                            <asp:TextBox ID="textBoxUserID" runat="server" Enabled="False"></asp:TextBox>
                                            <br />
                                            <asp:Label ID="labelKnjigaID" runat="server" Text="ID Knjige:"></asp:Label>
                                            <asp:TextBox ID="textBoxKnjigaID" runat="server" Enabled="False"></asp:TextBox>
                                            <br />
                                            <asp:Label ID="labelDatumIzposoje" runat="server" Text="Datum izposoje:"></asp:Label>
                                            <asp:TextBox ID="textBoxDatumIzposoje" runat="server" Enabled="False"></asp:TextBox>
                                            <br />
                                            <asp:Label ID="labelDatumVracila" runat="server" Text="Datum vračila:"></asp:Label>
                                            <asp:TextBox ID="textBoxDatumVracila" runat="server" Enabled="False"></asp:TextBox>
                                            <br />
                                            <asp:Label ID="label1DniIzposoje" runat="server" Text="Dni izposoje:"></asp:Label>
                                            <asp:TextBox ID="textBoxDniIzposoje" runat="server" Enabled="False"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Zaključi izposojo" />
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="View3" runat="server">
                        <%-- VIEW 3 --%>
                        <div class="tblRezervacija">
                            <h3>Poravnane zamudnine</h3>
                            <hr style="width: 20%" />

                            <br />
                            <div style="width: 700px; height: 250px; overflow-y: auto; overflow-x: hidden;" draggable="true">
                                <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Width="700px" CellPadding="3" DataKeyNames="id" DataSourceID="SqlDataSource5" ForeColor="Black" GridLines="Horizontal" AllowSorting="True">
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                                        <asp:BoundField DataField="id_user" HeaderText="ID Uporabnika" SortExpression="id_user" />
                                        <asp:BoundField DataField="id_izposoje" HeaderText="ID Izposoje" SortExpression="id_izposoje" />
                                        <asp:BoundField DataField="vsota" HeaderText="Vsota" SortExpression="vsota" />
                                        <asp:BoundField DataField="datumVnosa" DataFormatString="{0:dd.MM.yyyy}" HeaderText="Datum vnosa" SortExpression="datumVnosa" />
                                        <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="Gray" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:rpa_seminarska_nalogaConnectionString %>" SelectCommand="SELECT * FROM [Zamudnina]"></asp:SqlDataSource>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-8">
                                <div class="tblRezervacija">
                                    <h3>Odprte zamudnine</h3>
                                    <hr style="width: 20%" />
                                    <br />
                                    <div style="width: 700px; height: 250px; overflow-y: auto; overflow-x: hidden;" draggable="true">
                                        <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" Width="700px" BorderWidth="1px" CellPadding="3" DataKeyNames="id" DataSourceID="SqlDataSource6" ForeColor="Black" GridLines="Horizontal" OnSelectedIndexChanged="GridView7_SelectedIndexChanged" AllowSorting="True">
                                            <AlternatingRowStyle BackColor="#CCCCCC" />
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" SelectText="Izberi" />
                                                <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                                                <asp:BoundField DataField="id_user" HeaderText="ID Uporabnika" SortExpression="id_user" />
                                                <asp:BoundField DataField="id_izposoje" HeaderText="ID Izposoje" SortExpression="id_izposoje" />
                                                <asp:BoundField DataField="vsota" HeaderText="Vsota" SortExpression="vsota" />
                                                <asp:BoundField DataField="datumVnosa" DataFormatString="{0:dd.MM.yyyy}" HeaderText="Datum vnosa" SortExpression="datumVnosa" />
                                                <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                                            </Columns>
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:rpa_seminarska_nalogaConnectionString %>" SelectCommand="SELECT * FROM [Zamudnina]"></asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4" style="margin: 50px auto;">
                                <div style="text-align: center; align-content: center;">
                                    <h3 style="text-decoration: underline; width: auto;"><b>Popravnava zamudnine</b></h3>
                                    <div class="row" style="text-align: center; width: auto;">
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-4" style="text-align: right; width: auto; margin: 10px auto; font-size: 16px; font-weight: bold;">
                                            <asp:Label ID="Label3" runat="server" Text="ID Zamudnine:"></asp:Label>
                                            <asp:TextBox ID="textBoxIDzamudnine" runat="server" Enabled="False"></asp:TextBox>
                                            <br />
                                            <asp:Label ID="label4" runat="server" Text="ID Uporabnika:"></asp:Label>
                                            <asp:TextBox ID="textBoxIDUpo" runat="server" Enabled="False"></asp:TextBox>
                                            <br />
                                            <asp:Label ID="label6" runat="server" Text="Vsota:"></asp:Label>
                                            <asp:TextBox ID="textBoxVsota" runat="server" Enabled="False"></asp:TextBox>
                                            <br />
                                            <asp:Label ID="label7" runat="server" Text="Datum vnosa:"></asp:Label>
                                            <asp:TextBox ID="textBoxDatumVnosa" runat="server" Enabled="False"></asp:TextBox>
                                            <br />
                                            <asp:Label ID="label5" runat="server" Text="Status:"></asp:Label>
                                            <asp:TextBox ID="textBoxStatus1" runat="server" Enabled="False" Text="poravnana"></asp:TextBox>
                                            <br />
                                        </div>
                                    </div>
                                    <br />
                                    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Poravnaj izbrano zamudnino" />
                                </div>
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
        </body>
    </body>
    </html>
    </form>
</asp:Content>
