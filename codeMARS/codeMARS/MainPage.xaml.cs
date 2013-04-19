using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Xml.Linq;


namespace codeMARS
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();


            string source = "http://cab.inta-csic.es/rems/rems_weather.xml";

            var webClient=new WebClient();
            webClient.DownloadStringCompleted += webClient_DownloadStringCompleted;
            webClient.DownloadStringAsync(new Uri(source));

        }

        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error==null)
            {
                var feedXML = XDocument.Parse(e.Result);

                var data = from c in feedXML.Descendants("magnitudes")
                           select new weather
                           {
                               min_temp = (string)c.Element("min_temp"),
                               max_temp = (string)c.Element("max_temp"),
                               pressure = (string)c.Element("pressure"),
                               pressure_string = (string)c.Element("pressure_string"),
                               abs_humidity=(string) c.Element("abs_humidity"),
                               wind_speed = (string)c.Element("wind_speed"),
                               wind_direction = (string) c.Element("wind_direction"),
                               atmo_opacity=(string) c.Element("atmo_opacity"),
                               season=(string) c.Element("season"),
                               ls = (string)c.Element("ls"),
                               sunrise=(string) c.Element("sunrise"),
                               sunset=(string) c.Element("sunset")
                           };

                listBox.ItemsSource = data;            

                
            }
        }
    }

    public class weather
    {
        public string min_temp { get; set; }
        public string max_temp { get; set; }
        public string pressure { get; set; }
        public string pressure_string { get; set; }
        public string abs_humidity { get; set; }
        public string wind_speed { get; set; }
        public string wind_direction { get; set; }
        public string atmo_opacity { get; set; }
        public string season { get; set; }
        public string ls { get; set; }
        public string sunrise { get; set; }
        public string sunset { get; set; }

        //public float min_temp { get; set; }
        //public float max_temp { get; set; }
        //public float pressure { get; set; }
        //public string pressure_string { get; set; }
        //public string abs_humidity { get; set; }
        //public float wind_speed { get; set; }
        //public string wind_direction { get; set; }
        //public string atmo_opacity { get; set; }
        //public string season { get; set; }
        //public float ls { get; set; }
        //public string sunrise { get; set; }
        //public string sunset { get; set; }

    }
}