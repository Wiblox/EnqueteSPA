using MahApps.Metro.Controls;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Net;
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
            Geocode("39 rue charles de gaulle seremange","vivien KORPYS",true);
            Geocode("7 impasse maurice ravel Morhange","Viviane JEAN",false);
        }

        void addPushPin(Location loc, string name,bool delegueEnqueteur)
        {
            Pushpin te = new Pushpin();
            te.Location = loc;
            var CoordinateTip = new ToolTip();
            CoordinateTip.Content = name;

            te.Content =name;
            te.ToolTip = CoordinateTip;
            if (delegueEnqueteur) { 
            te.Background = new SolidColorBrush(Colors.Blue);
            }

            myMap.Children.Add(te);
        }

        // Geocode an address and return a latitude and longitude
        public void Geocode(string addressQuery,string name,bool delegueEnqueteur)
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
                string latitude = displayGeocodePoints[0].SelectSingleNode(".//rest:Latitude", nsmgr).InnerText;
                string longitude = displayGeocodePoints[0].SelectSingleNode(".//rest:Longitude", nsmgr).InnerText;
                addPushPin(new Location(Convert.ToDouble(latitude, System.Globalization.CultureInfo.InvariantCulture), Convert.ToDouble(longitude, System.Globalization.CultureInfo.InvariantCulture)),name, delegueEnqueteur);
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


    }
}
