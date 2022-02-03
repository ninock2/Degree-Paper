using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Web.UI;
using RPA;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class Account_Login : Page
{
    public void Page_PreInit(object sender, EventArgs e)
    {

    }
    public string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void LogIn(object sender, EventArgs e)
    {

        if (IsValid)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM RegistriraniUserji WHERE UporabniskoIme=@UporabniskoIme AND Geslo=@Geslo", con);
            cmd.Parameters.AddWithValue("@UporabniskoIme", UserName.Text);
            cmd.Parameters.AddWithValue("@Geslo", Password.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                SqlCommand id = new SqlCommand("SELECT ID FROM RegistriraniUserji WHERE UporabniskoIme = @UporabniskoIme", con);
                id.Parameters.AddWithValue("@UporabniskoIme", UserName.Text);
                int userID = (int)id.ExecuteScalar();
                Session["USER_ID"] = userID;
                Session["USER_NAME"] = UserName.Text;               
                Response.Redirect("~/Default.aspx");

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Validacija", "alert('Uporabniško ime in/ali geslo je nepravilno! \\nPritisni vredu za ponovni poizkus.')", true);

               //FailureText.Text = "Uporabniško ime ali geslo je nepravilno.";
                //ErrorMessage.Visible = true;
            }
            

        }
        
    }
}