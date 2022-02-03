using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


public partial class KnjigeNaVoljo : System.Web.UI.Page
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

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)

    {


    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)

    {
        GridView1.EditIndex = e.NewEditIndex;
        Knjige.DataBind();

    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)

    {
        int id = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());

        string naslovKnjige = ((TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0]).Text;
        string pisatelj = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
        string letoIzdaje = ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
        string naVoljo = ((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text;

        if ()
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "Opozorilo!", "<script language='javascript'>alert('Podatka Naslov knjige in Zaloga morata biti vnesena!')</script>");
        }
        else
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);

                SqlDataAdapter da = new SqlDataAdapter("", conn);

                conn.Open();

                da.UpdateCommand = new SqlCommand(
                    "UPDATE Knjige " +
                    "SET NaslovKnjige='" + naslovKnjige +
                    "' Pisatelj='" + pisatelj +
                    "' LetoIzdaje='" + letoIzdaje +
                    "' NaVoljo='" + naVoljo +
                    "' WHERE ID=" + id, conn);

                da.UpdateCommand.ExecuteNonQuery();
                conn.Close();
                GridView1.EditIndex = -1;

                Knjige.DataBind();
            }
            catch (Exception ex)
            {

                ClientScript.RegisterStartupScript(Page.GetType(), "Napaka!", "<script language='javascript'>alert('Prišlo je do napake! " + ex + "')</script>");
            }
            finally
            {
               
            }
        }

    }

    protected void GridView1_Add(object sender, EventArgs e)

    {
        string naslovknjige = naslovKnjige.Text;
        string pisat = pisatelj.Text;
        string letoizdaje = letoIzdaje.Text;
        string navoljo = naVoljo.Text;

        string query = "INSERT INTO Knjige VALUES(@NaslovKnjige, @Pisatelj, @LetoIzdaje, @NaVoljo)";
        
        if(string.IsNullOrEmpty(naslovknjige) || string.IsNullOrEmpty(navoljo))
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "Opozorilo!", "<script language='javascript'>alert('Podatka Naslov knjige in Zaloga morata biti vnesena!')</script>");
        }
        else
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Parameters.AddWithValue("@NaslovKnjige", naslovknjige);
                        cmd.Parameters.AddWithValue("@Pisatelj", pisat);
                        cmd.Parameters.AddWithValue("@LetoIzdaje", letoizdaje);
                        cmd.Parameters.AddWithValue("@NaVoljo", navoljo);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                Knjige.DataBind();
                Response.Redirect(Request.RawUrl, true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "Napaka!", "<script language='javascript'>alert('Prišlo je do napake! "+ ex +"')</script>");

            }
        }
        
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Knjige.DataBind();
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}