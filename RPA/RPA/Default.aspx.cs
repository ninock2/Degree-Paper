using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["USER_ADMIN"] != null)
        {
            Page.MasterPageFile = "/Administrator.master";

        }
        else if (Session["USER_ID"] != null)
        {
            Page.MasterPageFile = "/Uporabnik.master";
        }
        
    }

}