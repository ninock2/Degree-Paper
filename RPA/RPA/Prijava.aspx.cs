using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Prijava : System.Web.UI.Page
{

    public string connectionString = "Data Source=NINOPC\\SQLEXPRESS;Initial Catalog=rpa_seminarska_naloga;Integrated Security=True";
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnPrijavi_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(connectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT * FROM RegistriraniUserji WHERE UporabniskoIme=@UporabniskoIme and Geslo=@Geslo", con);
        cmd.Parameters.AddWithValue("@UporabniskoIme", txtUserIme.Text);
        cmd.Parameters.AddWithValue("@Geslo", txtUserGeslo.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            
            Response.Redirect("Default.aspx");
            
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", "Data has been saved", true);
        }
    }
}