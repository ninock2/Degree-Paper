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
using System.Text;


public class Opozorilo
{
    public int IzposojaID { get; set; }
    public string UserID { get; set; }
    public string NaslovKnjige { get; set; }
    public int KnjigaID { get; set; }
    public DateTime DatumIzposoje { get; set; }
    public int DniIzposoje { get; set; }

}
public class Opomin
{
    public int IzposojaID { get; set; }
    public string UserID { get; set; }
    public string NaslovKnjige { get; set; }
    public int KnjigeID { get; set; } //Dopolni z sql querijom
    public DateTime DatumIzposoje { get; set; }
    public int DniIzposoje { get; set; }
    public int DnevniPenal { get; set; }

}
public class Zamudnina
{
    public int ZamudninaID { get; set; } //Izpolnimo, da lahko dobimo Knjiga ID
    public int IzposojaID { get; set; }
    public string UserID { get; set; }
    public string NaslovKnjige { get; set; }
    public int KnjigeID { get; set; } //Dopolni z sql querijom
    public DateTime DatumIzposoje { get; set; }
    public DateTime DatumVracila { get; set; }
    public int Vsota { get; set; }
    public string Status { get; set; }

}


public partial class Obvestila : System.Web.UI.Page
{
    public string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

    public string htmlOpozorilo;
    public string htmlOpomnik;
    public string htmlZamudnina;

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
        string userID = Session["USER_ID"].ToString();
        var today = DateTime.Now;

        Opozorilo[] vsaOpozorila = null;
        Opomin[] vsiOpomini = null;
        Zamudnina[] vseZamudnine = null;

        string queryOpozorila = "SELECT id,id_knjige,datumIzposoje,dniIzposoje FROM Izposoje WHERE id_user=@id_user";
        string queryOpomin = "SELECT id,id_knjige,datumIzposoje,dniIzposoje, dnevniPenalty FROM Izposoje WHERE id_user=@id_user";
        string queryZamudnina = "SELECT id,id_izposoje,vsota,datumVnosa,status FROM Zamudnina WHERE id_user=@id_user";
        string queryGetKnjigaID = "SELECT id_knjige FROM Izposoje WHERE id=@id";
        string queryGetDI = "SELECT datumIzposoje FROM Izposoje WHERE id=@id";
        string queryGetDV = "SELECT datumVracila FROM Izposoje WHERE id=@id";
        string queryGetNaslov = "SELECT NaslovKnjige FROM Knjige WHERE id=@id";

        string naslov;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            //OPOZORILA
            using (SqlCommand command = new SqlCommand(queryOpozorila, con))
            {
                command.Parameters.AddWithValue("@id_user", userID);
                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var opozorilo = new List<Opozorilo>();
                    while (reader.Read() == true)
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("id_knjige"));

                        using (SqlConnection con2 = new SqlConnection(connectionString))
                        {
                            con2.Open();
                            using (SqlCommand cmd = new SqlCommand(queryGetNaslov))
                            {
                                cmd.Connection = con2;
                                cmd.Parameters.AddWithValue("@id", id);
                                naslov = (string)cmd.ExecuteScalar();

                            }
                        }

                        opozorilo.Add(new Opozorilo
                        {
                            IzposojaID = reader.GetInt32(reader.GetOrdinal("id")),
                            UserID = userID,
                            KnjigaID = reader.GetInt32(reader.GetOrdinal("id_knjige")),
                            NaslovKnjige = naslov,
                            DatumIzposoje = reader.GetDateTime(reader.GetOrdinal("datumIzposoje")),
                            DniIzposoje = reader.GetInt32(reader.GetOrdinal("dniIzposoje"))
                        }); 
                    }

                    vsaOpozorila = opozorilo.ToArray();
                }
                con.Close();
            }

            //OPOMINI
            using (SqlCommand command = new SqlCommand(queryOpomin, con))
            {
                command.Parameters.AddWithValue("@id_user", userID);
                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var opomin = new List<Opomin>();

                    
                    while (reader.Read() == true)
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("id_knjige"));

                        using (SqlConnection con2 = new SqlConnection(connectionString))
                        {
                            con2.Open();
                            using (SqlCommand cmd = new SqlCommand(queryGetNaslov))
                            {
                                cmd.Connection = con2;
                                cmd.Parameters.AddWithValue("@id", id);
                                naslov = (string)cmd.ExecuteScalar();

                            }
                        }
                        opomin.Add(new Opomin
                        {
                            UserID = userID,
                            IzposojaID = reader.GetInt32(reader.GetOrdinal("id")),
                            KnjigeID = reader.GetInt32(reader.GetOrdinal("id_knjige")),
                            NaslovKnjige = naslov,
                            DatumIzposoje = reader.GetDateTime(reader.GetOrdinal("datumIzposoje")),
                            DniIzposoje = reader.GetInt32(reader.GetOrdinal("dniIzposoje")),
                            DnevniPenal = reader.GetInt32(reader.GetOrdinal("dnevniPenalty")),
                        });
                    }
                    vsiOpomini = opomin.ToArray();
                }
                con.Close();
            }
            //ODPRTE ZAMUDNINE
            using (SqlCommand command = new SqlCommand(queryZamudnina, con))
            {
                command.Parameters.AddWithValue("@id_user", userID);
                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var zamudnina = new List<Zamudnina>();
                    while (reader.Read() == true)
                    {
                        int izposojaID = reader.GetInt32(reader.GetOrdinal("id_izposoje"));
                        int knjigaID;
                        DateTime datumIzposoje;
                        DateTime datumVracila;
                        //knjigaID, datumIzposoje in datumVracila podatke vzame iz Izposoje baze in jih doda v tabelo Odprte zamudnine za več konteksta
                        using (SqlConnection con2 = new SqlConnection(connectionString))
                        {
                            con2.Open();
                            using (SqlCommand cmd = new SqlCommand(queryGetKnjigaID))
                            {
                                cmd.Connection = con2;
                                cmd.Parameters.AddWithValue("@id", izposojaID);
                                knjigaID = (int)cmd.ExecuteScalar();

                            }
                            using (SqlCommand cmd = new SqlCommand(queryGetDI))
                            {
                                cmd.Connection = con2;
                                cmd.Parameters.AddWithValue("@id", izposojaID);
                                datumIzposoje = (DateTime)cmd.ExecuteScalar();

                            }
                            using (SqlCommand cmd = new SqlCommand(queryGetDV))
                            {
                                cmd.Connection = con2;
                                cmd.Parameters.AddWithValue("@id", izposojaID);
                                datumVracila = (DateTime)cmd.ExecuteScalar();

                            }
                            using (SqlCommand cmd = new SqlCommand(queryGetNaslov))
                            {
                                cmd.Connection = con2;
                                cmd.Parameters.AddWithValue("@id", knjigaID);
                                naslov = (string)cmd.ExecuteScalar();

                            }
                            con2.Close();
                        }

                        zamudnina.Add(new Zamudnina
                        {
                            ZamudninaID = reader.GetInt32(reader.GetOrdinal("id")),
                            IzposojaID = izposojaID,
                            UserID = userID,
                            KnjigeID = knjigaID,
                            NaslovKnjige = naslov,
                            DatumIzposoje = datumIzposoje,
                            DatumVracila = datumVracila,
                            Vsota = reader.GetInt32(reader.GetOrdinal("vsota")),
                            Status = reader.GetString(reader.GetOrdinal("status")),
                        });
                    }
                    vseZamudnine = zamudnina.ToArray();
                    con.Close();
                }
            }
        }

        //Filter za opozorila -> Danes je zadnji dan za vračilo
        foreach (var x in vsaOpozorila)
        {
            DateTime zadnjiDan = x.DatumIzposoje.AddDays(x.DniIzposoje);

            if (zadnjiDan == today)
            {
                htmlOpozorilo += "<table style='table-layout: fixed; background: #F8F8FF; width: 70%; border-radius: 2em / 5em'>";
                htmlOpozorilo += "<td>" + x.IzposojaID.ToString() + "</td>";
                htmlOpozorilo += "<td>" + x.NaslovKnjige.ToString() + "</td>";
                htmlOpozorilo += "<td>" + x.DatumIzposoje.ToString("dd.MM.yyyy") + " €" + "</td>";
                htmlOpozorilo += "<td>" + x.DniIzposoje.ToString() + "</td>";
                htmlOpozorilo += "</tr>";
                htmlOpozorilo += "<table/>";

                this.Page.DataBind();
            }
        }

        //Filter za opomine -> danes < datumIzposoje + dniIzposoje + 1 
        foreach (var x in vsiOpomini)
        {
            DateTime zadnjiDan = x.DatumIzposoje.AddDays(x.DniIzposoje);
            if (zadnjiDan < today)
            {
                htmlOpomnik += "<table style='table-layout: fixed; background: #F8F8FF; width: 80%; border-radius: 2em / 5em'>";
                htmlOpomnik += "<td>" + x.IzposojaID.ToString() + "</td>";
                htmlOpomnik += "<td>" + x.NaslovKnjige.ToString() + "</td>";
                htmlOpomnik += "<td>" + x.DatumIzposoje.ToString("dd.MM.yyyy") + "</td>";
                htmlOpomnik += "<td>" + x.DniIzposoje.ToString() + "</td>";
                htmlOpomnik += "<td>" + x.DnevniPenal.ToString() + "</td>";
                htmlOpomnik += "</tr>";
                htmlOpomnik += "<table/>";
            }

            this.Page.DataBind();
        }


        //Filter zamudnine
        foreach (var x in vseZamudnine)
        {
            if (x.Vsota > 0 && x.Status == "odprta")
            {
                htmlZamudnina += "<table style='table-layout: fixed; background: #F8F8FF; width: 90%; border-radius: 2em / 5em'>";
                htmlZamudnina += "<td>" + x.ZamudninaID.ToString() + "</td>";
                htmlZamudnina += "<td>" + x.NaslovKnjige.ToString() + "</td>";
                htmlZamudnina += "<td>" + x.DatumIzposoje.ToString("dd.MM.yyyy") + "</td>";
                htmlZamudnina += "<td>" + x.DatumVracila.ToString("dd.MM.yyyy") + "</td>";
                htmlZamudnina += "<td>" + x.Vsota.ToString() + "</td>";
                htmlZamudnina += "<td>" + x.Status.ToString() + "</td>";
                htmlZamudnina += "</tr>";
                htmlZamudnina += "<table/>";
            }
            this.Page.DataBind();
        }

    }

}
