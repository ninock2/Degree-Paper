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
using System.IO;
using System.Net;
using System.Net.Mail;

public partial class UpravljenjeObvestil : System.Web.UI.Page
{
    public class Opozorilo
    {
        public int IzposojaID { get; set; }
        public int UserID { get; set; }
        public int KnjigaID { get; set; }
        public DateTime DatumIzposoje { get; set; }
        public int DniIzposoje { get; set; }

    }
    public class Opomin
    {
        public int IzposojaID { get; set; }
        public int UserID { get; set; }
        public int KnjigeID { get; set; } //Dopolni z sql querijom
        public DateTime DatumIzposoje { get; set; }
        public int DniIzposoje { get; set; }
        public int DnevniPenal { get; set; }

    }
    public class Zamudnina
    {
        public int ZamudninaID { get; set; } //Izpolnimo, da lahko dobimo Knjiga ID
        public int IzposojaID { get; set; }
        public int UserID { get; set; }
        public int KnjigeID { get; set; } //Dopolni z sql querijom
        public DateTime DatumIzposoje { get; set; }
        public DateTime DatumVracila { get; set; }
        public int Vsota { get; set; }
        public DateTime DatumVnosa { get; set; }
        public string Status { get; set; }

    }
    public class SentMails
    {
        public int ID { get; set; } //Izpolnimo, da lahko dobimo Knjiga ID
        public int IzposojaID { get; set; }
        public int UserID { get; set; }
        public string TipMaila { get; set; } //Dopolni z sql querijom
        public string Namen { get; set; }
        public DateTime DatumMail { get; set; }

    }

    public string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

    public string htmlOpozorilo;
    public string htmlOpomnik;
    public string htmlZamudnina;

    //MAIL
    //User: spletna.knjiznica@gmail.com
    //Password: Admin123!

    readonly string queryUserName = "SELECT UporabniskoIme FROM RegistriraniUserji WHERE ID=@ID";
    readonly string queryUserMail = "SELECT Enaslov FROM RegistriraniUserji WHERE ID=@ID";
    readonly string queryKnjigaID = "SELECT id_knjige FROM Izposoje WHERE id=@id";
    readonly string queryBook = "SELECT NaslovKnjige FROM Knjige WHERE ID=@ID";

    protected void SendMail(int user_id, int izposoja_id, string namenMaila, string tipMaila)
    {
        string queryPreveriSentMails = "SELECT namen FROM SentMails WHERE id_izposoje=@id";
        SentMails[] vsiPoslaniMaili = null;

        string username;
        string usermail;
        int knjigaID;
        string naslovKnjige;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            //Pridobi vse maile po queryu
            using (SqlCommand command = new SqlCommand(queryPreveriSentMails, con))
            {
                con.Open();
                command.Parameters.AddWithValue("@id", izposoja_id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var poslanMail = new List<SentMails>();
                    while (reader.Read() == true)
                    {
                        poslanMail.Add(new SentMails
                        {
                            Namen = reader.GetString(reader.GetOrdinal("namen")),

                        });
                    }

                    vsiPoslaniMaili = poslanMail.ToArray();
                }
                con.Close();
            }
        }

        int i = 0;

        foreach (var x in vsiPoslaniMaili)
        {
            if (namenMaila == x.Namen)
            {
                ++i;
            }
        }

        if (i == 0)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //Pridobim username
                using (SqlCommand cmd = new SqlCommand(queryUserName))
                {
                    cmd.Parameters.AddWithValue("@id", user_id);

                    cmd.Connection = conn;
                    conn.Open();
                    username = (string)cmd.ExecuteScalar();
                    conn.Close();
                }
                //Pridobim mail uporabnika
                using (SqlCommand cmd = new SqlCommand(queryUserMail))
                {
                    cmd.Parameters.AddWithValue("@id", user_id);

                    cmd.Connection = conn;
                    conn.Open();
                    usermail = (string)cmd.ExecuteScalar();
                    conn.Close();
                }
                //Pridobim ID knjige
                using (SqlCommand cmd = new SqlCommand(queryKnjigaID))
                {
                    cmd.Parameters.AddWithValue("@id", izposoja_id);

                    cmd.Connection = conn;
                    conn.Open();
                    knjigaID = (int)cmd.ExecuteScalar();
                    conn.Close();
                }
                //Pridobim Naslov knjige
                using (SqlCommand cmd = new SqlCommand(queryBook))
                {
                    cmd.Parameters.AddWithValue("@id", knjigaID);

                    cmd.Connection = conn;
                    conn.Open();
                    naslovKnjige = (string)cmd.ExecuteScalar();
                    conn.Close();
                }
            }

            MailAddress mailUser = new MailAddress(usermail);
            string mailName = "spletna.knjiznica@gmail.com";
            string mailPass = "Admin123!";
            string sporočilo = "";
            string zadeva = "";

            switch (namenMaila)
            {
                case "opozorilo": // Mail 3: Opozorilo
                    sporočilo = @"<html>
                      <body>
                      <p> Pozdravljen/a " + username + ",</p>" +
                          "<p> Skozi pregled sistema smo ugotovili, da je jutri zadnji dan za vračilo knjige z naslovom " + naslovKnjige + " . Vrnite je pravočasno :) </p>" +
                          "<p>Lep pozdrav,<br>  Spletna Knjižnica</br></p>" +
                          "<br><p>Telefonska številka: 040-123-456 </p></br> " +
                          "<br><p>Mail: spletna.knjiznica@gmail.com </p></br> " +
                          "<br><p>Naslov: Namišljena ulica 10 </p></br>" +
                          "</body>" +
                          "</html>";
                    zadeva = "Spletna knjižnica: Opozorilo";
                    break;

                case "opomin": // Mail 2: Opomin
                    sporočilo = @"<html>
                      <body>
                      <p> Pozdravljen/a " + username + ",</p>" +
                          "<p> Skozi pregled sistema smo ugotovili, da še niste vrnili knjige z naslovom" + naslovKnjige + " . Prosim vrnite jo v najkrajšem možnem času. </p>" +
                          "<p>Lep pozdrav,<br>  Spletna Knjižnica</br></p>" +
                          "<br><p>Telefonska številka: 040-123-456 </p></br> " +
                          "<br><p>Mail: spletna.knjiznica@gmail.com </p></br> " +
                          "<br><p>Naslov: Namišljena ulica 10 </p></br>" +
                          "</body>" +
                          "</html>";
                    zadeva = "Spletna knjižnica: Opomin";
                    break;

                case "poravnava": //Mail 3: Zamudnina
                    sporočilo = @"<html>
                      <body>
                      <p> Pozdravljen/a " + username + ",</p>" +
                          "<p> Skozi pregled sistema smo ugotovili, da še niste poravnali vseh obveznosti za knjigo " + naslovKnjige + ". Za več informacij obiščite spletno stran.  </p>" +
                          "<p>Lep pozdrav,<br>  Spletna Knjižnica</br></p>" +
                          "<p>Telefonska številka: 040-123-456 </p><" +
                          "<p>Mail: spletna.knjiznica@gmail.com </p>" +
                          "<p>Naslov: Namišljena ulica 10 </p>" +
                          "</body>" +
                          "</html>";
                    zadeva = "Spletna knjižnica: Poravnava";
                    break;

                default:
                    break;
            }

            //MAIL
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(mailName);
                mail.To.Add(mailUser);
                mail.Subject = zadeva;
                mail.Body = sporočilo;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(mailName, mailPass);
                    smtp.EnableSsl = true;

                    try
                    {
                        smtp.Send(mail);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }

            string queryVstaviVSentMails = "INSERT INTO SentMails VALUES (@id_user,@id_izposoje,@tipMaila,@namen,@datumMaila)";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //Pridobim username
                    using (SqlCommand cmd = new SqlCommand(queryVstaviVSentMails))
                    {
                        cmd.Parameters.AddWithValue("@id_user", user_id);
                        cmd.Parameters.AddWithValue("@id_izposoje", izposoja_id);
                        cmd.Parameters.AddWithValue("@tipMaila", tipMaila);
                        cmd.Parameters.AddWithValue("@namen", namenMaila);
                        cmd.Parameters.AddWithValue("@datumMaila", DateTime.Now);

                        cmd.Connection = conn;
                        conn.Open();
                        username = (string)cmd.ExecuteScalar();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        else
        {
            //Mail že obstaja
        }
    }

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
    protected void Page_Load(object sender, EventArgs e)
    {
        var today = DateTime.Now;

        Opozorilo[] vsaOpozorila = null;
        Opomin[] vsiOpomini = null;
        Zamudnina[] vseZamudnine = null;

        string queryOpozorila = "SELECT id,id_user,id_knjige,datumIzposoje,dniIzposoje FROM Izposoje";
        string queryOpomin = "SELECT id,id_user,id_knjige,datumIzposoje,dniIzposoje, dnevniPenalty FROM Izposoje";
        string queryZamudnina = "SELECT id,id_user,id_izposoje,vsota,datumVnosa,status FROM Zamudnina";
        string queryGetKnjigaID = "SELECT id_knjige FROM Izposoje WHERE id=@id";
        string queryDatumIzposoje = "SELECT datumIzposoje FROM Izposoje WHERE id=@id";
        string queryDatumVracila = "SELECT datumVracila FROM Izposoje WHERE id=@id";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            //OPOZORILA
            using (SqlCommand command = new SqlCommand(queryOpozorila, con))
            {
                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var opozorilo = new List<Opozorilo>();
                    while (reader.Read() == true)
                    {
                        opozorilo.Add(new Opozorilo
                        {
                            IzposojaID = reader.GetInt32(reader.GetOrdinal("id")),
                            UserID = reader.GetInt32(reader.GetOrdinal("id_user")),
                            KnjigaID = reader.GetInt32(reader.GetOrdinal("id_knjige")),
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
                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var opomin = new List<Opomin>();
                    while (reader.Read() == true)
                    {
                        opomin.Add(new Opomin
                        {
                            UserID = reader.GetInt32(reader.GetOrdinal("id_user")),
                            IzposojaID = reader.GetInt32(reader.GetOrdinal("id")),
                            KnjigeID = reader.GetInt32(reader.GetOrdinal("id_knjige")),
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
                        //reader in cmd komanda ne morata naenkrat laufat na istem connection (con), zato je ustvarjen con2
                        using (SqlConnection con2 = new SqlConnection(connectionString))
                        {
                            con2.Open();
                            using (SqlCommand cmd = new SqlCommand(queryGetKnjigaID))
                            {
                                cmd.Connection = con2;
                                cmd.Parameters.AddWithValue("@id", izposojaID);
                                knjigaID = (int)cmd.ExecuteScalar();
                            }
                            using (SqlCommand cmd = new SqlCommand(queryDatumIzposoje))
                            {
                                cmd.Connection = con2;
                                cmd.Parameters.AddWithValue("@id", izposojaID);
                                datumIzposoje = (DateTime)cmd.ExecuteScalar();
                            }
                            using (SqlCommand cmd = new SqlCommand(queryDatumVracila))
                            {
                                cmd.Connection = con2;
                                cmd.Parameters.AddWithValue("@id", izposojaID);
                                datumVracila = (DateTime)cmd.ExecuteScalar();
                            }
                            con2.Close();
                        }

                        zamudnina.Add(new Zamudnina
                        {
                            ZamudninaID = reader.GetInt32(reader.GetOrdinal("id")),
                            IzposojaID = izposojaID,
                            UserID = reader.GetInt32(reader.GetOrdinal("id_user")),
                            KnjigeID = knjigaID,
                            DatumIzposoje = datumIzposoje,
                            DatumVracila = datumVracila,
                            Vsota = reader.GetInt32(reader.GetOrdinal("vsota")),
                            DatumVnosa = reader.GetDateTime(reader.GetOrdinal("datumVnosa")),
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
                htmlOpozorilo += "<tr>";
                htmlOpozorilo += "<td>" + x.IzposojaID.ToString() + "</td>";
                htmlOpozorilo += "<td>" + x.UserID.ToString() + "</td>";
                htmlOpozorilo += "<td>" + x.KnjigaID.ToString() + "</td>";
                htmlOpozorilo += "<td>" + x.DatumIzposoje.ToString("dd.MM.yyyy") +"td>";
                htmlOpozorilo += "<td>" + x.DniIzposoje.ToString() + "</td>";
                htmlOpozorilo += "</tr>";
                htmlOpozorilo += "<table/>";

                this.Page.DataBind();

                SendMail(x.UserID, x.IzposojaID, "opozorilo", "izposoja");
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
                htmlOpomnik += "<td>" + x.UserID.ToString() + "</td>";
                htmlOpomnik += "<td>" + x.KnjigeID.ToString() + "</td>";
                htmlOpomnik += "<td>" + x.DatumIzposoje.ToString("dd.MM.yyyy") + "</td>";
                htmlOpomnik += "<td>" + x.DniIzposoje.ToString() + "</td>";
                htmlOpomnik += "<td>" + x.DnevniPenal.ToString() + "</td>";
                htmlOpomnik += "</tr>";
                htmlOpomnik += "<table/>";

                SendMail(x.UserID, x.IzposojaID, "opomin", "izposoja");
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
                htmlZamudnina += "<td>" + x.UserID.ToString() + "</td>";
                htmlZamudnina += "<td>" + x.KnjigeID.ToString() + "</td>";
                htmlZamudnina += "<td>" + x.DatumIzposoje.ToString("dd.MM.yyyy") + "</td>";
                htmlZamudnina += "<td>" + x.DatumVracila.ToString("dd.MM.yyyy") + "</td>";
                htmlZamudnina += "<td>" + x.Vsota.ToString() + "</td>";
                htmlZamudnina += "<td>" + x.Status.ToString() + "</td>";
                htmlZamudnina += "</tr>";
                htmlZamudnina += "<table/>";

                SendMail(x.UserID, x.IzposojaID, "poravnava", "zamudnina");
            }

            this.Page.DataBind();
        }

    }

}

