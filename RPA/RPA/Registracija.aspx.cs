using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Registracija : System.Web.UI.Page
{
    public string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        //ALI JE UPORABNO??


        /*
        if (IsPostBack)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string preveriUporabnika = "SELECT COUNT(*) FROM RegistriraniUserji WHERE UporabniskoIme='" + txtIme.Text + "'";
            SqlCommand cmd = new SqlCommand(preveriUporabnika, conn);
            int temp = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            if (temp == 1)
            {
                Response.Write("Uporabniško ime že obstaja!");
            }

            conn.Close();
        }
        */
    }

    protected void btnRegistriraj_Click(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("SELECT ID FROM RegistriraniUserji WHERE UporabniskoIme = @UporabniskoIme", con);
        cmd.Parameters.AddWithValue("@UporabniskoIme", this.txtIme.Text);
        con.Open();

        var result = cmd.ExecuteScalar();

        //Skalarno preverjanje ali uporabnik z tem imenom ze obstaja!
        if (result != null)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "Validacija", "<script language='javascript'>alert('Uporabnik z tem imenom že obstaja!')</script>");

        }
        else
        {
            try
            {

                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                string insertQuery = "INSERT INTO RegistriraniUserji(UporabniskoIme,Geslo,Spol,Enaslov) VALUES (@UporabniskoIme,@Geslo,@Spol,@Enaslov)";
                SqlCommand cmdd = new SqlCommand(insertQuery, conn);
                cmdd.Parameters.AddWithValue("@UporabniskoIme", txtIme.Text);
                cmdd.Parameters.AddWithValue("@Geslo", txtGeslo.Text);
                cmdd.Parameters.AddWithValue("@Spol", txtSpol.Text);
                cmdd.Parameters.AddWithValue("@Enaslov", txtEnaslov.Text);
                cmdd.ExecuteNonQuery();

                ClientScript.RegisterStartupScript(Page.GetType(), "Validacija", "<script language='javascript'>alert('Uspešno ste registrirali uporabnika!')</script>");

                conn.Close();

            }
            catch (Exception ex)
            {
                Response.Write("Napaka. " + ex.ToString());
            }

            ClientScript.RegisterStartupScript(Page.GetType(), "Validacija", "<script language='javascript'>alert('Uporabnik z tem imenom že obstaja!')</script>");
        }


   
        
    }
}