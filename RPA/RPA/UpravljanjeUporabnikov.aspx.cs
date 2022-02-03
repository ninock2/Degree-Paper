using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

public partial class UpravljanjeUporabnikov : System.Web.UI.Page
{
    public string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;


    protected void GridView1_PreRender(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        textBoxID.Text = GridView1.SelectedRow.Cells[1].Text;
        textBoxUserName.Text = GridView1.SelectedRow.Cells[2].Text;
        textBoxPassword.Text = GridView1.SelectedRow.Cells[3].Text;
        textBoxSex.Text = GridView1.SelectedRow.Cells[4].Text;
        textBoxEmail.Text = GridView1.SelectedRow.Cells[5].Text;
    }

    protected void btnPotrdi_Click(object sender, EventArgs e)
    {
        string id = textBoxID.Text;
        string user = textBoxUserName.Text;
        string pass = textBoxPassword.Text;
        string sex = textBoxSex.Text;
        string email = textBoxEmail.Text;

        string query = "UPDATE RegistriraniUserji SET UporabniskoIme=@UporabniskoIme, Geslo=@Geslo, Spol=@Spol, Enaslov=@Enaslov WHERE ID=@ID";

        if (user.Trim().Length == 0 || pass.Trim().Length == 0 || email.Trim().Length == 0)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "Opozorilo!", "<script language='javascript'>alert('Podatki Uporabnisko ime, geslo in enaslov ne smejo ostati prazni!')</script>");
        }
        else
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@UporabniskoIme", user);
                    cmd.Parameters.AddWithValue("@Geslo", pass);
                    cmd.Parameters.AddWithValue("@Spol", sex);
                    cmd.Parameters.AddWithValue("@Enaslov", email);

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            SqlDataSource1.DataBind();
            Response.Redirect(Request.RawUrl, true);
        }
    }
}