using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RPA;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class AdminPrijava : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["USER_NAME"] != null)
        //{
        //    Label labelAdmin = (Label)Page.Master.FindControl("labelAdmin");
        //    labelAdmin.Text = "Dobrodošel, " + Session["USER_ADMIN"].ToString() + " !" ;
        //}
    }

    protected void LogIn(object sender, EventArgs e)
    {

        if (UserName.Text == "Admin" && Password.Text == "Admin")
        {
            Session["USER_ADMIN"] = UserName.Text;
            Response.Redirect("Default.aspx");

        }
        else
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "Validacija", "<script language='javascript'>alert('Nepravilo administratorsko ime in/ali geslo')</script>");
        }
    }
}