<%@ Page Title="Kontakt" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <h2>Kontakt</h2>
        <hr style="width:100%;" />
        <div class ="row">
            <div class="col-lg-6" style="margin:auto; width:50%; padding: 100px 0; font-size: 20px;">
            <h3><b>Najdete nas...</b></h3>
            <address>
                Kraj: Maribor<br />
                Naslov: Namišljena ulica 10<br />
                Telefon: 040-123-456
            </address>

            <address>
                <strong>Mail:</strong>   <a href="mailto:cehteljkristl.nino@gmail.com">cehteljkristl.nino@gmail.com</a><br />
            </address>
            <div class="row" style="width:200px; margin:auto;">
                <div class="col-md-3">
                    <a href="https://www.facebook.com/"><img src="https://img.icons8.com/office/40/000000/facebook-new.png"/></a>
                </div>
                <div class="col-md-3">
                    <a href="https://twitter.com/"><img src="https://img.icons8.com/office/40/000000/twitter.png"/></a>
                </div>
                <div class="col-md-3">
                    <a href="https://www.linkedin.com/"><img src="https://img.icons8.com/office/40/000000/linkedin.png"/></a>
                </div>
                <div class="col-md-3">
                    <a href="https://www.instagram.com/"><img src="https://img.icons8.com/office/40/000000/instagram-new.png"/></a>
                </div>

            </div>


        </div>

        <div class="col-lg-6">
            <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d9775.901224022555!2d15.648773000008612!3d46.56213547331442!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x476f77011dec62e1%3A0x6d6cd629e58b62c9!2sMaribor!5e0!3m2!1ssl!2ssi!4v1639424539923!5m2!1ssl!2ssi" width="600" height="450" style="border:0; border-radius: 2rem;" allowfullscreen="" loading="lazy"></iframe>
        </div>
    </div>
</asp:Content>
