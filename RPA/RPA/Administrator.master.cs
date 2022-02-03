using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administrator : System.Web.UI.MasterPage
{
    public string name; 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Session["USER_ADMIN"] != null)
            {

                name = "Zdravo, " + Session["USER_ADMIN"].ToString() + "!";
                this.Page.DataBind();
            }
        }

    }
}
