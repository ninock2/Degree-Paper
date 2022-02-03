<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Account_Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2>Registracija</h2>
        <h4>Ustvarite nov uporabniški račun.</h4>
        <hr style="width: 100%;"/>

    <div class="form-horizontal" style="width: 1000px; margin: 20px auto;">
        <asp:ValidationSummary runat="server" CssClass="text-danger" />

        <%-- Uporabniško ime --%>
        <div class="form-group" style="width: 2200px; margin-bottom: 0px;">
            <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label">Uporaniško ime: </asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="UserName" CssClass="form-control" Width="300px"/>
                <div class="col-md-2">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                        CssClass="text-danger" ErrorMessage="Vpišite ime." />
                </div>
            </div>
        </div>

        <%-- Uporabniško geslo --%>
        <div class="form-group" style="width: 2200px; margin-bottom: 0px;">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Geslo: </asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" Width="300px"/>
                <div class="col-md-2">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                        CssClass="text-danger" ErrorMessage="Vpišite geslo." />
                </div>
            </div>
        </div>

        <%-- Potrditev uporabniškega gesla --%>
        <div class="form-group" style="width: 2200px; margin-bottom: 20px;">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Potrdi geslo: </asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" Width="300px"/>
                <div class="col-md-2">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                        CssClass="text-danger" Display="Dynamic" ErrorMessage="Potrditev geslo je potrebno vpisati." />
                    <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                        CssClass="text-danger" Display="Dynamic" ErrorMessage="Gesla se ne ujemata." />
                </div>
            </div>
        </div>

        <%-- Spol --%>
        <div class="form-group" style="width: 2200px; margin-bottom: 20px;">
            <asp:Label runat="server" AssociatedControlID="Gender" CssClass="col-md-2 control-label">Spol: </asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="Gender" runat="server" CssClass="form-control" Width="300px">
                <asp:ListItem>Moški</asp:ListItem>
                <asp:ListItem>Ženska</asp:ListItem>
                <asp:ListItem>Neopredeljen</asp:ListItem>
            </asp:DropDownList>
            </div>
        </div>

        <%-- E-naslov --%>
        <div class="form-group" style="width: 2200px; margin-bottom: 0px;">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">E-naslov: </asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control" Width="300px"/>
                <div class="col-md-2">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                        CssClass="text-danger" ErrorMessage="Vpišite E-naslov." />
                </div>
            </div>
        </div>

        <div class="form-group" style="width:900px;">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Registriraj" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>

