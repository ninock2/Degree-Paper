using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class MojeRezervacije : System.Web.UI.Page
{
    public int userID;
    public string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["USER_ADMIN"] != null)
        {
            Page.MasterPageFile = "/Administrator.master";

        }
        else if (Session["USER_ID"] != null)
        {
            userID = Convert.ToInt32(Session["USER_ID"]);
            Page.MasterPageFile = "/Uporabnik.master";
        }

    }

    //public string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        Tab1.CssClass = "Clicked";
        MainView.ActiveViewIndex = 0;
        //Preklicane rezervacije
        SqlDataSource2.SelectCommand = "SELECT * FROM [Rezervacije] WHERE datumPreklica IS NOT NULL AND id_user = " + userID;
        //Aktivne rezervacije
        SqlDataSource1.SelectCommand = "SELECT [id], [id_user], [id_knjige], [datumRezervacije], [status] FROM [Rezervacije] WHERE datumPreklica IS NULL AND status = 'aktivna' AND id_user = " + userID;
        //Izposoje
        SqlDataSource3.SelectCommand = "SELECT [id], [id_user], [id_knjige], [id_rezervacije], [datumIzposoje], [dniIzposoje] FROM [Izposoje] WHERE datumVracila IS NULL AND id_user = " + userID;
        //Vračila
        SqlDataSource4.SelectCommand = "SELECT [id], [id_user], [id_knjige], [id_rezervacije], [datumIzposoje], [datumVracila], [dniIzposoje] FROM [Izposoje] WHERE datumVracila IS NOT NULL AND id_user = " + userID;

    }
    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }

    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        Tab3.CssClass = "Initial";
        MainView.ActiveViewIndex = 1;
    }

    protected void Tab3_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Clicked";
        MainView.ActiveViewIndex = 2;
    }

    //View1
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        textBoxIdRezervacije.Text = GridView1.SelectedRow.Cells[1].Text;
        textBoxIdUporabnika.Text = GridView1.SelectedRow.Cells[2].Text;
        textBoxIdKnjige.Text = GridView1.SelectedRow.Cells[3].Text;
        textBoxDatumRezervacije.Text = GridView1.SelectedRow.Cells[4].Text;
        textBoxDatumPreklica.Text = DateTime.Now.ToString("dd.MM.yyyy");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {

            string queryPreklic = "UPDATE Rezervacije SET datumPreklica=@datumPreklica, status=@status WHERE id=@id";
            string queryPovecajZalogo = "UPDATE Knjige SET NaVoljo=NaVoljo+1 WHERE ID=@id_knjige";

            string id = textBoxIdRezervacije.Text;
            string id_knjige = textBoxIdKnjige.Text;
            DateTime datumPreklica = DateTime.Today;
            string status = textBoxStatus.Text;

            if (string.IsNullOrEmpty(id))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "Opozorilo", "<script language='javascript'>alert('Za preklic niste izbrali nobene knjige!')</script>");

            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(queryPreklic))
                    {
                        var today = DateTime.Today;
                        cmd.Parameters.AddWithValue("@datumPreklica", datumPreklica);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@id", id);

                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    using (SqlCommand cmd = new SqlCommand(queryPovecajZalogo))
                    {
                        var today = DateTime.Today;
                        cmd.Parameters.AddWithValue("@id_knjige", id_knjige);

                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

                ClientScript.RegisterStartupScript(Page.GetType(), "Potrdilo", "<script language='javascript'>alert('Rezervacije knjige je bila uspešno preklicana. Če je bil preklic narejen po pomoti knjigo ponovno rezervirajte!')</script>");



                SqlDataSource1.DataBind();
                SqlDataSource2.DataBind();
                Response.AppendHeader("Refresh", "1;url=MojeRezervacije.aspx");

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }

    //View2
    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}