using EnquteSPA.bo;
using MahApps.Metro.Controls;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;

namespace EnquteSPA
{
    /// <summary>
    /// Interaction logic for LocateEnqueteur.xaml
    /// </summary>
    public partial class LocateEnqueteur : MetroWindow
    {
        public LocateEnqueteur()
        {
            InitializeComponent();
            using var db = new Context();
            var listeEnqueteur = db.SpaPersonne.Where(v => v .IsEnqueteur==true).ToList();
            foreach (SpaPersonne enqueteur in listeEnqueteur)
            {
                addPushPin(new Location(enqueteur.x, enqueteur.y), enqueteur.Nom, enqueteur.Prenom, enqueteur.IsSalarie);
            }
        }

        void addPushPin(Location loc, string nom, string prenom, bool delegueEnqueteur)
        {
            Pushpin te = new Pushpin();
            te.Location = loc;
            var CoordinateTip = new ToolTip();
            CoordinateTip.Content = nom.ToUpper() + " " + prenom.ToUpper();
            te.Content = $"{nom.FirstOrDefault()}{prenom.FirstOrDefault()}";
            te.ToolTip = CoordinateTip;
            if (delegueEnqueteur)
                te.Background = new SolidColorBrush(Colors.Blue);
            myMap.Children.Add(te);
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


    }
}
