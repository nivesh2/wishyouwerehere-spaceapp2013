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
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace codeMARS
{
    public partial class PivotPage1 : PhoneApplicationPage
    {
        public PivotPage1()
        {
            InitializeComponent();
            work();
        }

        public void work()
        {
            var client = new WebClient();
            client.DownloadStringCompleted += (sender, args) =>
            {
                if (args.Error == null)
                {
                    string json = args.Result;
                    RootObject deserializedData = ReadToObject(json);
                    updateHomePage(deserializedData.results[0]);
                }
            };
            client.DownloadStringAsync(new Uri("http://marsweather.ingenology.com/v1/archive/?format=json"));

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

        public void updateHomePage(Result r)
        {
            Earth_timeBlock.Text = "Last Updated on: " + r.terrestrial_date.ToString();

            max_tempBlock.Text = r.max_temp_fahrenheit.ToString("#") + "°F";
            min_tempBlock.Text = r.min_temp_fahrenheit.ToString() + "°F";


            pressureBlock.Text = "MARS Pressure is " + r.pressure_string + " around: " + r.pressure.ToString("#") + " Pa";


            ls_Block.Text = season(r.ls) + r.ls.ToString() + "°";
            sol_block2.Text = season2(r.ls);

            if (r.atmo_opacity == null)
            {
                atm_opacityBlock.Text = "Sunny";

            }
            else
            {
                atm_opacityBlock.Text = r.atmo_opacity;
            }
            
            sol_block.Text = "Mars Date: " + r.sol.ToString() + " sol";
        }

        public void updateArchiveList(Result r)
        {
             
        }


        public string season(double d)
        {
            string s="";

            if (d>=0 && d<90 )
            {
                s = "It's Spring Equinox on MARS, Ls = ";
                
            }
            else if (d>=90 && d<180)
            {
                s = "It's Summer Solstice on MARS, Ls = ";
            }
            else if (d>=180 && d<270)
            {
                s = "It's Autumn Equinox on MARS, Ls = ";                
            }
            else if (d>=270 )
            {
                s = "It's  Winter Solstice on MARS, Ls = ";
            }


            return s;

        }
        public string season2(double d)
        {
            string s = "";
            
            if (d >= 180 && d < 210)
            {
                s = "Dust Storm Season begins";
            }
            else if (d >= 210 && d < 330)
            {
                s = "Dust Storm Season";
            }
            else if (d >= 330)
            {
                s = "Dust Storm Season ends";
            }


            return s;

        }
        

    }// end of main class


    public class Result
    {
        public string terrestrial_date { get; set; }
        public int sol { get; set; }
        public double ls { get; set; }
        public double min_temp { get; set; }
        public double min_temp_fahrenheit { get; set; }
        public double max_temp { get; set; }
        public double max_temp_fahrenheit { get; set; }
        public double pressure { get; set; }
        public string pressure_string { get; set; }
        public object abs_humidity { get; set; }
        public double wind_speed { get; set; }
        public string wind_direction { get; set; }
        public string atmo_opacity { get; set; }
        public string season { get; set; }
        public string sunrise { get; set; }
        public string sunset { get; set; }
    }

    public class RootObject
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public List<Result> results { get; set; }
    }
}