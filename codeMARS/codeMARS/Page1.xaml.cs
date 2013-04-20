using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Runtime.Serialization;

namespace codeMARS
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
            work();
        }
        public void work()
        {
            var client = new WebClient();
            client.DownloadStringCompleted += (sender, args) =>
            {
               if (args.Error ==null)
	           {
                string json = args.Result;
                RootObject deserializedData = ReadToObject(json);
                MessageBox.Show(deserializedData.report.pressure_string);
              }
            };
            client.DownloadStringAsync(new Uri("http://marsweather.ingenology.com/v1/latest/?format=json"));

        }


        // Deserialize a JSON stream to a RootObject.
        public static RootObject ReadToObject(string json)
        {
            RootObject deserializedUser = new RootObject();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as RootObject;
            ms.Close();
            return deserializedUser;
        }




        public void load()
        {
            
        }



    }

    [DataContract]
    public class Report
    {
         [DataMember]
        public string terrestrial_date { get; set; }
         [DataMember]
        public int sol { get; set; }
         [DataMember]
        public double ls { get; set; }
         [DataMember]
        public double min_temp { get; set; }
         [DataMember]
        public double min_temp_fahrenheit { get; set; }
         [DataMember]
        public double max_temp { get; set; }
         [DataMember]
        public double max_temp_fahrenheit { get; set; }
         [DataMember]
        public double pressure { get; set; }
         [DataMember]
        public string pressure_string { get; set; }
         [DataMember]
        public object abs_humidity { get; set; }
         [DataMember]
        public double wind_speed { get; set; }
         [DataMember]
        public string wind_direction { get; set; }
         [DataMember]
        public string atmo_opacity { get; set; }
         [DataMember]
        public string season { get; set; }
         [DataMember]
        public string sunrise { get; set; }
         [DataMember]
        public string sunset { get; set; }
    }

    public class RootObject
    {
        public Report report { get; set; }
    }
}