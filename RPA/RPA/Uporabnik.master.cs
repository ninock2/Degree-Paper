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
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

public partial class Uporanik : System.Web.UI.MasterPage
{
    public string name;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            if (Session["USER_NAME"] != null)
            {
                
                name = "Zdravo, " + Session["USER_NAME"].ToString() + "!";
                this.Page.DataBind();
            }
        }
    }
    public void OdjavaBtn_Click(object sender, EventArgs e)
    {
        Session.Remove("USER_ID");
        Session.RemoveAll();
        Response.Redirect("~/Login.aspx");
    }
}
