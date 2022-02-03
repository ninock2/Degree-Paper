<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Odjava.aspx.cs" Inherits="Odjava" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Odjava</title>
    </head>
    <body>
        <div style="margin-bottom: 40px;">
            <h2><asp:Label ID="Label1" runat="server" Text="Odjava uspešna."></asp:Label></h2>
            <br />
            <h4>
            Preusmerili vas bomo čez 5 sekund. Za takojšno preusmeritev klikni <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">tukaj...</asp:HyperLink></h4>
        </div>
    </body>
    </html>

</asp:Content>

