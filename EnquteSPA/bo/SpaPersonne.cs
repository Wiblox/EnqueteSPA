using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Windows;
using System.Xml;

namespace EnquteSPA
{
    [Table("SpaPersonne")]
    public class SpaPersonne
    {
        public SpaPersonne() { }

        public SpaPersonne(string nom, string prenom, string mail, string ville, string rue, string numero, bool isSalarie, bool isEnqueteur, bool etat)
        {
            Nom = nom;
            Prenom = prenom;
            Mail = mail;
            Ville = ville;
            Rue = rue;
            Numero = numero;
            IsSalarie = isSalarie;
            IsEnqueteur = isEnqueteur;
            Etat = etat;
            Point position = localisation(numero + " "+ rue+" "+ ville+ " "+   "France");
            x = position.X;
            y = position.Y;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSpaPersonne { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Mail { get; set; }

        public string Ville { get; set; }

        public string Rue { get; set; }
        public Double x { get; set; }
        public Double y { get; set; }

        public string Numero { get; set; }
        
        public bool IsSalarie { get; set; }

        public bool IsEnqueteur { get; set; }
        
        public bool Etat { get; set; }

        public override bool Equals(object obj)
        {
            return obj is SpaPersonne personne &&
                   IdSpaPersonne == personne.IdSpaPersonne &&
                   Nom == personne.Nom &&
                   Prenom == personne.Prenom &&
                   Mail == personne.Mail &&
                   Ville == personne.Ville &&
                   Rue == personne.Rue &&
                   Numero == personne.Numero &&
                   IsSalarie == personne.IsSalarie &&
                   IsEnqueteur == personne.IsEnqueteur &&
                   Etat == personne.Etat;
        }

        private static XmlDocument GetXmlResponse(string requestUrl)
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

        public static Point localisation(string addressQuery)
        {
            //Create REST Services geocode request using Locations API
            string geocodeRequest = "http://dev.virtualearth.net/REST/v1/Locations/" + addressQuery + "?o=xml&key=" + "magxKWMjHaUtTsRgF1lW~MRGGMAW5GbjaUW6sUk7-Cw~AtgoUxOrpiDSHWcYdPwQMWlLB71ydq7H2smazBVWmL3vt28stY2eAw3bv40wZBiM";

            //Make the request and get the response
            XmlDocument geocodeResponse = GetXmlResponse(geocodeRequest);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(geocodeResponse.NameTable);
            nsmgr.AddNamespace("rest", "http://schemas.microsoft.com/search/local/ws/rest/v1");
            XmlNodeList locationElements = geocodeResponse.SelectNodes("//rest:Location", nsmgr);
            double latitude= 0;
            double longitude=0;
            if (locationElements.Count == 0)
            {

            }
            else
            {
                //Get the geocode points that are used for display (UsageType=Display)
                XmlNodeList displayGeocodePoints =
                        locationElements[0].SelectNodes(".//rest:GeocodePoint/rest:UsageType[.='Display']/parent::node()", nsmgr);
                latitude = double.Parse(displayGeocodePoints[0].SelectSingleNode(".//rest:Latitude", nsmgr).InnerText.Replace('.', ','));
                longitude = double.Parse(displayGeocodePoints[0].SelectSingleNode(".//rest:Longitude", nsmgr).InnerText.Replace('.', ','));
            }


            return new Point(latitude, longitude);
        }

        public string GetLocalisation()
        {
            return $"{Numero} {Rue} {Ville} France";
        }
    }
}