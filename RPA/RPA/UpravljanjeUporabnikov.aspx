<%@ Page Language="C#" MasterPageFile="~/Administrator.master" AutoEventWireup="true" CodeFile="UpravljanjeUporabnikov.aspx.cs" Inherits="UpravljanjeUporabnikov" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <!DOCTYPE html>

    <html>
    <head>
        <title></title>
    </head>
    <body>
        <h1>Upravljanje uporabnikov</h1>
        <hr style="width: 100%;" />
        <div class="row">
            <div class="col-md-8">
                <div style="text-align: center; width: 700px; height: 350px; overflow-y: auto; overflow-x: hidden; margin: 10px auto;" draggable="true">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1" ForeColor="Black" Width="700px" AutoGenerateSelectButton="True" Height="300px" HorizontalAlign="Center" PageSize="25" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" OnPreRender="GridView1_PreRender" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" GridLines="Horizontal">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                            <asp:BoundField DataField="UporabniskoIme" HeaderText="Uporabnisko ime" SortExpression="UporabniskoIme" />
                            <asp:BoundField DataField="Geslo" HeaderText="Geslo" SortExpression="Geslo" />
                            <asp:BoundField DataField="Spol" HeaderText="Spol" SortExpression="Spol" />
                            <asp:BoundField DataField="Enaslov" HeaderText="E-naslov" SortExpression="Enaslov" />
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Right" />
                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#808080" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#383838" />
                    </asp:GridView>
                </div>
                <br />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:rpa_seminarska_nalogaConnectionString %>"
                    SelectCommand="SELECT * FROM [RegistriraniUserji]"
                    DeleteCommand="DELETE FROM RegistriraniUserji WHERE ID=@ID"></asp:SqlDataSource>
            </div>
            <h3 style="text-decoration: underline"><b>Urejanje podatkov izbranega uporabnika</b></h3>
            <div class="row" style="text-align: center;">
                <div class="col-md-4" style="text-align:center;">
                </div>
                <div class="col-md-4" style="text-align: right; width: 400px; margin: 10px auto; font-size: 16px; font-weight: bold;">
                    <asp:Label ID="Label1" runat="server" Text="ID Uporabnika:"></asp:Label>
                    <asp:TextBox ID="textBoxID" runat="server" Enabled="False"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="Uporabniško Ime: "></asp:Label>
                    <asp:TextBox ID="textBoxUserName" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label4" runat="server" Text="Geslo: "></asp:Label>
                    <asp:TextBox ID="textBoxPassword" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="Spol:"></asp:Label>
                    <asp:TextBox ID="textBoxSex" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label5" runat="server" Text="E-naslov:"></asp:Label>
                    <asp:TextBox ID="textBoxEmail" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Button ID="btnPotrdi" runat="server" Text="Potrdi spremembe" OnClick="btnPotrdi_Click" />
                </div>
            </div>
        </div>
    </body>
    </html>

</asp:Content>
