<%@ Page Title="Rezerviraj knjigo" Language="C#" MasterPageFile="~/Uporabnik.master" AutoEventWireup="true" CodeFile="RezervirajKnjigo.aspx.cs" Inherits="RezervirajKnjigo" %>

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
        </style>
    </head>
    <body>


        <h1 style="text-align: center;">Rezerviraj knjigo</h1>
        <hr style="width: 100%;" />

        <%-- GridView Knjig ki so trenutno na voljo --%>
        <div class="row">
            <div class="col-md-8" style="margin-top: 50px; text-align: center; width: 800px; height: 700px; margin: 20px auto; overflow-y: auto; overflow-x: hidden;" draggable="true">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="Knjige" Height="230px" Width="785px" CssClass="auto-style1" HorizontalAlign="Justify" PageSize="7" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="None" AllowSorting="True">
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle Height="35px" HorizontalAlign="Center" BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerSettings FirstPageText="&amp;lt; &amp;lt;" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle Height="25px" HorizontalAlign="Center" VerticalAlign="NotSet" />
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID št." InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                        <asp:BoundField DataField="NaslovKnjige" HeaderText="Naslov knjige" SortExpression="NaslovKnjige" />
                        <asp:BoundField DataField="Pisatelj" HeaderText="Pisatelj" SortExpression="Pisatelj" />
                        <asp:BoundField DataField="LetoIzdaje" HeaderText="Leto izdaje" SortExpression="LetoIzdaje" />
                        <asp:BoundField DataField="NaVoljo" HeaderText="Zaloga" SortExpression="NaVoljo" />
                    </Columns>
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>
                <asp:SqlDataSource ID="Knjige" runat="server" ConnectionString="<%$ ConnectionStrings:myConnectionString %>"
                    SelectCommand="SELECT * FROM [Knjige] WHERE NaVoljo > 0"
                    DeleteCommand="DELETE FROM Knjige WHERE ID = @ID"
                    UpdateCommand="UPDATE Knjige SET NaslovKnjige=@NaslovKnjige, Pisatelj=@Pisatelj, LetoIzdaje=@LetoIzdaje, NaVoljo=@NaVoljo WHERE ID=@ID"></asp:SqlDataSource>
            </div>
            <div class="col-md-4">
                <p style="text-align: center; font-size: large; font-weight: 700; margin-top: 20px;"><b>IZBRANA KNJIGA</b></p>
                <table border="0" style="width: 300px; font-size: large;">
                    <tr>
                        <td>Naslov knjige:</td>
                        <td>
                            <asp:TextBox ID="txtbosNaslov" runat="server" Width="140" ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>Pisatelj:</td>
                        <td>
                            <asp:TextBox ID="txtboxPisatelj" runat="server" Width="140" ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>Leto izdaje:</td>
                        <td>
                            <asp:TextBox ID="txtboxLetoIzdaje" runat="server" Width="140" ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>Na voljo:</td>
                        <td>
                            <asp:TextBox ID="txtboxNaVoljo" runat="server" Width="140" ReadOnly="true" />

                        </td>
                    </tr>
                </table>
                <div style="margin-top: 20px;">
                    <asp:Button ID="rezervirajKnjigo" runat="server" Text="Rezerviraj izbrano knjigo" OnClick="GridView1_RezervirajKnjigo" />
                </div>
            </div>
        </div>
    </body>
    </html>
</asp:Content>
