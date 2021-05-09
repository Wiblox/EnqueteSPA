using System.Windows;
using MahApps.Metro.Controls;
using System;
using EnquteSPA.bo;
using EnquteSPA.modele;
using MahApps.Metro.Controls.Dialogs;
using System.Linq;
using System.Xml;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;

namespace EnquteSPA
{
    /// <summary>
    /// Logique d'interaction pour AddEnquete.xaml
    /// </summary>
    public partial class AddEnquete : MetroWindow
    {
        private readonly Erreur erreur;
        private Enquete enquete;
        private Personne plaignant, infracteur;
        private double latitude;
        private double longitude;

        public AddEnquete(Enquete enquete = null)
        {
            InitializeComponent();
            XDateDepot.SelectedDate = DateTime.Today;
            GenTxtNumEnquete();
            erreur = new Erreur();
            this.enquete = enquete;

            if (this.enquete != null)
            {
                Title = "Modification d'une enquête";

                XNumEnquete.Text = this.enquete.NoEnquete;
                XDateDepot.SelectedDate = this.enquete.DateDepot;
                XDateDepot.IsEnabled = false;
                XDepartement.Text = this.enquete.Departement;
                XDepartement.IsEnabled = false;
                XMotif.Text = this.enquete.Motif;

                using var db = new Context();

                XEnqueteur.SelectedItem = db.SpaPersonne.Find(enquete.IdEnqueteur);

                plaignant = db.Personne.Find(this.enquete.IdPlaignant);
                infracteur = db.Personne.Find(this.enquete.IdInfracteur);

                XPlaignantNom.Text = plaignant.Nom;
                XPlaignantNom.IsEnabled = false;
                XPlaignantPrenom.Text = plaignant.Prenom;
                XPlaignantPrenom.IsEnabled = false;
                XPlaignantMail.Text = plaignant.Mail;
                XPlaignantMail.IsEnabled = false;
                XPlaignantVille.Text = plaignant.Ville;
                XPlaignantVille.IsEnabled = false;
                XPlaignantNumero.Text = plaignant.Numero;
                XPlaignantNumero.IsEnabled = false;
                XPlaignantRue.Text = plaignant.Rue;
                XPlaignantRue.IsEnabled = false;

                XInfracteurNom.Text = infracteur.Nom;
                XInfracteurNom.IsEnabled = false;
                XInfracteurPrenom.Text = infracteur.Prenom;
                XInfracteurPrenom.IsEnabled = false;
                XInfracteurMail.Text = infracteur.Mail;
                XInfracteurMail.IsEnabled = false;
                XInfracteurVille.Text = infracteur.Ville;
                XInfracteurVille.IsEnabled = false;
                XInfracteurNumero.Text = infracteur.Numero;
                XInfracteurNumero.IsEnabled = false;
                XInfracteurRue.Text = infracteur.Rue;
                XInfracteurRue.IsEnabled = false;
            }
        }

        private void GenTxtNumEnquete()
        {
            if(enquete == null)
            {
                string id = "<ID>";
                string depTxt = XDepartement.Text;
                if (depTxt.Length == 0) depTxt = "<DEP>";
                string numEnqueteWithoutID = $"{depTxt}/{DateTime.Now.ToString("yy/MM")}";
                if (XDepartement.Text.Length > 0)
                {
                    using var db = new Context();
                    id = $"{db.Enquete.Where(v => v.NoEnquete.StartsWith(numEnqueteWithoutID)).Count() + 1}".PadLeft(3, '0');
                }
                XNumEnquete.Text = $"{numEnqueteWithoutID}/{id}";
            }
        }

        private XmlDocument GetXmlResponse(string requestUrl)
        {
            System.Diagnostics.Trace.WriteLine("Request URL (XML): " + requestUrl);
            HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(response.GetResponseStream());
                return xmlDoc;
            }
        }

        public Point localisation(string addressQuery)
        {
            //Create REST Services geocode request using Locations API
            string geocodeRequest = "http://dev.virtualearth.net/REST/v1/Locations/" + addressQuery + "?o=xml&key=" + "magxKWMjHaUtTsRgF1lW~MRGGMAW5GbjaUW6sUk7-Cw~AtgoUxOrpiDSHWcYdPwQMWlLB71ydq7H2smazBVWmL3vt28stY2eAw3bv40wZBiM";

            //Make the request and get the response
            XmlDocument geocodeResponse = GetXmlResponse(geocodeRequest);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(geocodeResponse.NameTable);
            nsmgr.AddNamespace("rest", "http://schemas.microsoft.com/search/local/ws/rest/v1");
            XmlNodeList locationElements = geocodeResponse.SelectNodes("//rest:Location", nsmgr);
            if (locationElements.Count == 0)
            {

            }
            else
            {
                //Get the geocode points that are used for display (UsageType=Display)
                XmlNodeList displayGeocodePoints =
                        locationElements[0].SelectNodes(".//rest:GeocodePoint/rest:UsageType[.='Display']/parent::node()", nsmgr);
                 latitude = double.Parse(displayGeocodePoints[0].SelectSingleNode(".//rest:Latitude", nsmgr).InnerText.Replace('.',','));
                 longitude = double.Parse(displayGeocodePoints[0].SelectSingleNode(".//rest:Longitude", nsmgr).InnerText.Replace('.', ','));
            }


            return new Point(latitude, longitude);
        }

        private List<SpaPersonne> listTrier(Point p)
        {
            List<SpaPersonne> listefinale = new List<SpaPersonne>();
            using var db = new Context();
            var listeEnqueteur = db.SpaPersonne.Where(v => v.IsEnqueteur==true).ToList();
            Point[] distance = new Point[listeEnqueteur.Count()];
            double[] distanceDouble = new double[listeEnqueteur.Count()];

            int i = 0;
            foreach (SpaPersonne enqueteur in listeEnqueteur)
            {
                distance[i] = new Point(enqueteur.x, enqueteur.y);
                i++;

            }
            for ( i = 0; i < listeEnqueteur.Count(); i++)
            {
                distanceDouble[i] = Math.Abs(distance[i].X - p.X) + Math.Abs(distance[i].Y - p.Y);
            }
            List<Tuple<double, SpaPersonne>> test = new List<Tuple<double, SpaPersonne>>() ;

            for (i = 0; i < listeEnqueteur.Count(); i++)
            {
                test.Add(new Tuple<double, SpaPersonne>(distanceDouble[i], listeEnqueteur[i]));

            }
            test.Sort(Comparer<Tuple<double, SpaPersonne>>.Default);
            for (i = 0; i < listeEnqueteur.Count(); i++)
            {
                listefinale.Add(test[i].Item2);

            }

            return listefinale;

        }

        private void ListeEnqueteurs(object sender, EventArgs e)
        {
            XEnqueteur.ItemsSource = listTrier();
        }

        private void TxtDepartementChange(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (erreur.TestNonVide(XDepartement.Text) && !erreur.TestDepartement(XDepartement.Text))
            {
                XDepartement.Text = XDepartement.Text.Remove(XDepartement.Text.Length - 1, 1);
                XDepartement.SelectionStart = XDepartement.Text.Length;
            }
            GenTxtNumEnquete();
        }

        private bool VerifChamps()
        {
            erreur.ResetMsgRetour();
            erreur.TestDepartement(XDepartement.Text, "Département");
            erreur.TestNonVide(XMotif.Text, "Motif");
            // Plaignant
            erreur.TestNonVide(XPlaignantNom.Text, "Nom du plaignant");
            erreur.TestNonVide(XPlaignantPrenom.Text, "Prénom du plaignant");
            erreur.TestMail(XPlaignantMail.Text, "Mail du plaignant");
            erreur.TestNonVide(XPlaignantVille.Text, "Ville du plaignant");
            erreur.TestNonVide(XPlaignantNumero.Text, "Numéro du plaignant");
            erreur.TestNonVide(XPlaignantRue.Text, "Rue du plaignant");
            // Infracteur
            erreur.TestNonVide(XInfracteurNom.Text, "Nom de l'infracteur");
            erreur.TestNonVide(XInfracteurPrenom.Text, "Prénom de l'infracteur");
            erreur.TestMail(XInfracteurMail.Text, "Mail de l'infracteur");
            erreur.TestNonVide(XInfracteurVille.Text, "Ville de l'infracteur");
            erreur.TestNonVide(XInfracteurNumero.Text, "Numéro de l'infracteur");
            erreur.TestNonVide(XInfracteurRue.Text, "Rue de l'infracteur");

            if (!erreur.IsSafe())
                this.ShowMessageAsync("Erreur création de compte", erreur.GetMsgRetour());
            return erreur.IsSafe();
        }

        private void ClickBtnValider(object sender, RoutedEventArgs e)
        {
            if (VerifChamps())
            {
                using var db = new Context();

                int statut = (XEnqueteur.SelectedItem == null) ? (int)StatutEnquete.NON_ASSIGNEE : (int)StatutEnquete.EN_COURS;
                if (enquete == null)
                {
                    Personne plaignant = new Personne(XPlaignantNom.Text, XPlaignantPrenom.Text, XPlaignantMail.Text, XPlaignantVille.Text, XPlaignantRue.Text, XPlaignantNumero.Text);
                    db.Personne.Add(plaignant);
                    Personne infracteur = new Personne(XInfracteurNom.Text, XInfracteurPrenom.Text, XInfracteurMail.Text, XInfracteurVille.Text, XInfracteurRue.Text, XInfracteurNumero.Text);
                    db.Personne.Add(infracteur);
                    db.SaveChanges();
                    Enquete enquete = new Enquete(XNumEnquete.Text, XDepartement.Text, (DateTime)XDateDepot.SelectedDate, infracteur.IdPersonne, plaignant.IdPersonne, XMotif.Text, (int?)XEnqueteur.SelectedValue, statut);
                    db.Enquete.Add(enquete);
                }
                else
                {
                    if(XEnqueteur.SelectedValue != null)
                        enquete.IdEnqueteur = (int)XEnqueteur.SelectedValue;
                    enquete.Statut = statut;
                    enquete.Motif = XMotif.Text;
                }
                db.SaveChanges();
                Close();
            }
        }

        private void XPlaignantNom_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            XPlaignantNom.Text = XPlaignantNom.Text.ToUpper();
            XPlaignantNom.SelectionStart = XPlaignantNom.Text.Length;
        }

        private void XPlaignantPrenom_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            XPlaignantPrenom.Text = Static.FirstLetterToUpper(XPlaignantPrenom.Text);
            XPlaignantPrenom.SelectionStart = XPlaignantPrenom.Text.Length;
        }

        private void XInfracteurNom_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            XInfracteurNom.Text = XInfracteurNom.Text.ToUpper();
            XInfracteurNom.SelectionStart = XInfracteurNom.Text.Length;

        }

        private void XEnqueteur_GotFocus(object sender, RoutedEventArgs e)
        {
            erreur.ResetMsgRetour();
            erreur.TestNonVide(XInfracteurNumero.Text, "Nom du plaignant");
            erreur.TestNonVide(XInfracteurRue.Text, "Nom du plaignant");
            erreur.TestNonVide(XInfracteurVille.Text, "Nom du plaignant");


            if (erreur.IsSafe())
            {
                XEnqueteur.ItemsSource = listTrier(localisation(XInfracteurNumero.Text + " " + XInfracteurRue.Text + " " + XInfracteurVille.Text));

               
            }
            else
            {
                XEnqueteur.ItemsSource = listTrier();
            }
        }

        private List<SpaPersonne> listTrier()
        {
            using var db = new Context();

            return db.SpaPersonne.Where(v => v.IsEnqueteur == true).ToList();
        }

        private void XInfracteurPrenom_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            XInfracteurPrenom.Text = Static.FirstLetterToUpper(XInfracteurPrenom.Text);
            XInfracteurPrenom.SelectionStart = XInfracteurPrenom.Text.Length;
        }
    }
}
