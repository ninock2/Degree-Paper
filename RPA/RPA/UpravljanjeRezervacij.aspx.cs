using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

public partial class UpravljanjeRezervacij : System.Web.UI.Page

{
    public string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

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

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        textBoxIdRezervacije.Text = GridView1.SelectedRow.Cells[1].Text;
        textBoxIdUporabnika.Text = GridView1.SelectedRow.Cells[2].Text;
        textBoxIdKnjige.Text = GridView1.SelectedRow.Cells[3].Text;
        textBoxDatumRezervacije.Text = GridView1.SelectedRow.Cells[4].Text;
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        //Preklicane rezervacije
        SqlDataSource2.SelectCommand = "SELECT * FROM [Rezervacije] WHERE datumPreklica IS NOT NULL AND status='preklicana'";
        //Aktivne rezervacije
        SqlDataSource1.SelectCommand = "SELECT [id], [id_user], [id_knjige], [datumRezervacije], [status] FROM [Rezervacije] WHERE datumPreklica IS NULL AND status='aktivna' ";
        //Izposoje
        SqlDataSource3.SelectCommand = "SELECT [id], [id_user], [id_knjige], [id_rezervacije], [datumIzposoje], [dniIzposoje] FROM [Izposoje] WHERE datumVracila IS NULL";
        //Vračila
        SqlDataSource4.SelectCommand = "SELECT [id], [id_user], [id_knjige], [id_rezervacije], [datumIzposoje], [datumVracila], [dniIzposoje] FROM [Izposoje] WHERE datumVracila IS NOT NULL";
        //Poravnane zamudnine
        SqlDataSource5.SelectCommand = "SELECT * FROM [Zamudnina] WHERE status='poravnana'";
        //Odprte zamudnine
        SqlDataSource6.SelectCommand = "SELECT * FROM [Zamudnina] WHERE status='odprta'";

    }

    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
        GridView1.DataBind();
        GridView2.DataBind();
    }

    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        Tab3.CssClass = "Initial";
        MainView.ActiveViewIndex = 1;
        GridView8.DataBind();
        GridView3.DataBind();
    }

    protected void Tab3_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Clicked";
        MainView.ActiveViewIndex = 2;
        GridView6.DataBind();
        GridView7.DataBind();
    }

    protected void CustomValidator1_ServerValidate(object sender, ServerValidateEventArgs e)
    {
        DateTime d;
        e.IsValid = DateTime.TryParseExact(e.Value, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
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

            if (GridView1.SelectedIndex < 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "Opozorilo!", "<script language='javascript'>alert('Nobena rezervacija ni bila izbrana za potrditev!')</script>");
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


                //VSTAVI Rezervacijo v Izposoje
                string queryVstaviVIzposoje = "INSERT INTO Izposoje VALUES (@id_user,@id_knjige,@id_rezervacije,@datumIzposoje,@datumVracila,@dniIzposoje,@dnevniPenalty)";
                string querySpremeniStatus = "UPDATE Rezervacije SET status=@status WHERE id=@id";


                string id_rezervacije = textBoxIdRezervacije.Text;
                string id_user = textBoxIdUporabnika.Text;
                string iid_knjige = textBoxIdKnjige.Text;
                DateTime datumIzposoje = DateTime.Today;
                string dniIzposoje = DropDownListDniIzposoje.Text;
                string dnevniPenalty = DropDownDnevniPenalty.Text;
                string sstatus = textBoxStatus.Text;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(queryVstaviVIzposoje))
                    {
                        var today = DateTime.Today;
                        cmd.Parameters.AddWithValue("@id_rezervacije", id_rezervacije);
                        cmd.Parameters.AddWithValue("@id_knjige", iid_knjige);
                        cmd.Parameters.AddWithValue("@id_user", id_user);
                        cmd.Parameters.AddWithValue("@datumIzposoje", datumIzposoje);
                        cmd.Parameters.AddWithValue("@datumVracila", DBNull.Value);
                        cmd.Parameters.AddWithValue("@dniIzposoje", dniIzposoje);
                        cmd.Parameters.AddWithValue("@dnevniPenalty", dnevniPenalty);

                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    using (SqlCommand cmd = new SqlCommand(querySpremeniStatus))
                    {
                        var today = DateTime.Today;
                        cmd.Parameters.AddWithValue("@id", id_rezervacije);

                        cmd.Parameters.AddWithValue("@status", sstatus);

                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

                GridView1.DataBind();
                GridView2.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    //Izposoje
    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {
        textBoxIDIZposoje.Text = GridView3.SelectedRow.Cells[1].Text;
        textBoxUserID.Text = GridView3.SelectedRow.Cells[2].Text;
        textBoxKnjigaID.Text = GridView3.SelectedRow.Cells[3].Text;
        textBoxDatumIzposoje.Text = GridView3.SelectedRow.Cells[5].Text;
        textBoxDatumVracila.Text = DateTime.Now.ToString("dd.MM.yyyy");
        textBoxDniIzposoje.Text = GridView3.SelectedRow.Cells[6].Text;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string queryVstaviVVracila = "UPDATE Izposoje SET datumVracila=@datumVracila WHERE id=@id";
        string queryGetDatumIzposoje = "SELECT datumIzposoje FROM Izposoje WHERE id=@id";
        string queryVstaviVZamudnine = "INSERT INTO Zamudnina VALUES (@id_user, @id_izposoje, @vsota, @datumVnosa, @status)";
        string queryGetDnevniPenalty = "SELECT dnevniPenalty FROM Izposoje WHERE id=@id";

        if (GridView3.SelectedIndex < 0)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "Opozorilo!", "<script language='javascript'>alert('Nobena izposoja ni bila izbrana za potrditev vračila!')</script>");
        }
        else
        {
            string IDizposoje = textBoxIDIZposoje.Text;
            string IDuser = textBoxUserID.Text;
            DateTime datumVracila = DateTime.Now;
            DateTime datumVnosa = DateTime.Now;
            int dniIzposoje = int.Parse(textBoxDniIzposoje.Text);
            DateTime datumIzposoje; //SQL
            int dnevniPenalty; //SQL
            string status = "odprta";


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(queryGetDatumIzposoje))
                    {
                        cmd.Parameters.AddWithValue("@id", IDizposoje);

                        cmd.Connection = conn;
                        conn.Open();
                        datumIzposoje = (DateTime)cmd.ExecuteScalar();
                        conn.Close();
                    }

                    using (SqlCommand cmd = new SqlCommand(queryVstaviVVracila))
                    {
                        cmd.Parameters.AddWithValue("@id", IDizposoje);
                        cmd.Parameters.AddWithValue("@datumVracila", datumVracila);

                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    DateTime zadnjiDanVracila = datumIzposoje.AddDays(dniIzposoje);

                    //VSTAVI V ZAMUDNINE
                    if (zadnjiDanVracila < datumVracila)
                    {
                        //dobim dnevniPenalty
                        using (SqlCommand cmd = new SqlCommand(queryGetDnevniPenalty))
                        {
                            cmd.Parameters.AddWithValue("@id", IDizposoje);

                            cmd.Connection = conn;
                            conn.Open();
                            dnevniPenalty = (int)cmd.ExecuteScalar();
                            conn.Close();
                        }

                        int stDni = (datumVracila - zadnjiDanVracila).Days;
                        int vsota = stDni * dnevniPenalty;

                        //vstavim v zamudnine
                        using (SqlCommand cmd = new SqlCommand(queryVstaviVZamudnine))
                        {
                            cmd.Parameters.AddWithValue("@id_user", IDuser);
                            cmd.Parameters.AddWithValue("@id_izposoje", IDizposoje);
                            cmd.Parameters.AddWithValue("@vsota", vsota);
                            cmd.Parameters.AddWithValue("@datumVnosa", datumVnosa);
                            cmd.Parameters.AddWithValue("@status", status);

                            cmd.Connection = conn;
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            GridView8.DataBind();
            GridView3.DataBind();
        }


    }

    protected void GridView7_SelectedIndexChanged(object sender, EventArgs e)
    {
        textBoxIDzamudnine.Text = GridView7.SelectedRow.Cells[1].Text;
        textBoxIDUpo.Text = GridView7.SelectedRow.Cells[2].Text;
        textBoxVsota.Text = GridView7.SelectedRow.Cells[3].Text;
        textBoxVsota.Text = GridView7.SelectedRow.Cells[4].Text;
        textBoxDatumVnosa.Text = GridView7.SelectedRow.Cells[5].Text;
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        string queryPoravnajZamudnino = "UPDATE Zamudnina SET status=@status WHERE id=@id";

        if (GridView7.SelectedIndex < 0)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "Opozorilo!", "<script language='javascript'>alert('Nobena zamudnina ni bila izbrana za poravnavo!')</script>");
        }
        else
        {

            string id = textBoxIDzamudnine.Text;
            string status = textBoxStatus1.Text;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(queryPoravnajZamudnino))
                    {
                        var today = DateTime.Today;
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@status", status);

                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            GridView6.DataBind();
            GridView7.DataBind();
        }
    }

}
