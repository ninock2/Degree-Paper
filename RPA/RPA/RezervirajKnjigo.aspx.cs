using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

public partial class RezervirajKnjigo : Page
{
    public string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //Session.RemoveAll();
        if (Session["USER_ADMIN"] != null)
        {
            Page.MasterPageFile = "/Administrator.master";

        }
        else if (Session["USER_ID"] != null)
        {
            Page.MasterPageFile = "/Uporabnik.master";
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void GridView1_RezervirajKnjigo(object sender, EventArgs e)
    {
        //string IDuporabnika = Session["USER_ID"].ToString();
        //string uporabniskoIme = Session["USER_NAME"].ToString();
        //string IDknjige = GridView1.SelectedRow.Cells[1].Text;
        //string naslovKnjige = GridView1.SelectedRow.Cells[2].Text;
        //string pisateljKnjige = GridView1.SelectedRow.Cells[3].Text;




        string query = "INSERT INTO Rezervacije VALUES(@id_user, @id_knjige, @datumRezervacije, @datumPreklica, @status)";
        string queryZmanjasNaVoljo = "UPDATE Knjige SET NaVoljo=NaVoljo-1 WHERE ID=@ID";

        if (GridView1.SelectedIndex >= 0)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        var today = DateTime.Today;
                        cmd.Parameters.AddWithValue("@id_user", Session["USER_ID"].ToString());
                        cmd.Parameters.AddWithValue("@id_knjige", GridView1.SelectedRow.Cells[1].Text);
                        cmd.Parameters.AddWithValue("@datumRezervacije", today);
                        cmd.Parameters.AddWithValue("@datumPreklica", DBNull.Value);
                        cmd.Parameters.AddWithValue("@status", "aktivna");

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    using (SqlCommand cmd = new SqlCommand(queryZmanjasNaVoljo, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", GridView1.SelectedRow.Cells[1].Text);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                Knjige.DataBind();
                Response.Redirect(Request.RawUrl, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "Pozor!", "<script language='javascript'>alert('Nobena knjiga ni bila izbrana!')</script>");
        }

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Knjige.DataBind();
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtbosNaslov.Text = GridView1.SelectedRow.Cells[2].Text;
        txtboxPisatelj.Text = GridView1.SelectedRow.Cells[3].Text;
        txtboxLetoIzdaje.Text = GridView1.SelectedRow.Cells[4].Text;
        txtboxNaVoljo.Text = GridView1.SelectedRow.Cells[5].Text;

    }
}