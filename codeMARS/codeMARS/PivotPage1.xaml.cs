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
using System.Windows.Media.Imaging;

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
                    updateWindDirection(deserializedData.results[0]);

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
            Earth_timeBlock.Text = "Mars Weather on " + month(r.terrestrial_date);

            max_tempBlock.Text = r.max_temp_fahrenheit.ToString("#") + "°F";
            min_tempBlock.Text = r.min_temp_fahrenheit.ToString() + "°F";


            pressureBlock.Text = "Pressure : " + r.pressure.ToString("#") + " Pa, " + r.pressure_string;

            ls_Block.Text = season(r.ls);
            ls_Block2.Text = "Ls = " + r.ls.ToString() + "°";
            sol_block2.Text = season2(r.ls);

            if (r.atmo_opacity == null)
            {
                atm_opacityBlock.Text = "Sunny";

            }
            else
            {
                atm_opacityBlock.Text = r.atmo_opacity;
            }
            
            //sol_block.Text = "Mars Date: " + r.sol.ToString() + " sol ";
        }

        public void updateArchiveList(Result r)
        {
             
        }

        public void updateWindDirection(Result r)
        {
            string img = "/direction";
            string theme="/light";
            string dr ="North";
            img += theme;
            
            #region wind direction
            string wd = r.wind_direction;
            
            if (wd == "E")
            {
                img += "/e.png"; 
                dr = "EAST";
            }
            else if (wd == "S")
            {
                img += "/s.png";
                dr = "SOUTH";
                
            }
            else if (wd == "N")
            {
                img += "/n.png";
                dr = "North";
            }
            else if (wd == "W")
            {
                img += "/w.png";
                dr = "WEST";
            }
            else if (wd == "NW")
            {
                img += "/nw.png";
                dr = "North-West";
            }
            else if (wd == "NE")
            {
                img += "/ne.png";
                dr = "North-East";
            }
            else if (wd == "SW")
            {
                img += "/sw.png";
                dr = "South-West";
            }
            else if (wd == "SE")
            {
                img += "/se.png";
                dr = "South-East";          
            }
#endregion




            direction_image.Source = new BitmapImage(new Uri(img, UriKind.Relative));
            wind_directionBlock.Text = "Direction : " + dr;
            wind_speedBlock.Text = "Wind Speed : " + r.wind_speed.ToString() + " m/s";
            wind_dateBlock.Text = "Record Date : " + month(r.terrestrial_date);

        }

        public string month(string s)
        {
            string s1 = s.Substring(0, 4);
            string s2 = s.Substring(5, 2);
            string s3 = s.Substring(8, 2);

            if (s2=="01")
            {
                s2 = "January";
            }
            else if (s2=="02")
            {
                s2 = "February";
            }
            else if (s2 == "03")
            {
                s2 = "March";
            }
            else if (s2 == "04")
            {
                s2 = "April";
            }
            else if (s2 == "05")
            {
                s2 = "May";
            }
            else if (s2 == "06")
            {
                s2 = "June";
            }
            else if (s2 == "07")
            {
                s2 = "July";
            }
            else if (s2 == "08")
            {
                s2 = "August";
            }
            else if (s2 == "09")
            {
                s2 = "September";
            }
            else if (s2 == "10")
            {
                s2 = "October";
            }
            else if (s2 == "11")
            {
                s2 = "November";
            }
            else if (s2 == "12")
            {
                s2 = "December";
            }

            return s3 + " " + s2 + " " + s1;
 
        }
        public string season(double d)
        {
            string s="";

            if (d>=0 && d<30 )
            {
                s = "Early NH Spring";
                
            }
            else if (d>=30 && d<60)
            {
                s = "NH Spring";
            }
            else if (d>=60 && d<90)
            {
                s = "Late NH Spring";                
            }
            else if (d >= 90 && d < 120)
            {
                s = "Early NH Summer";
            }
            else if (d >= 120 && d < 150)
            {
                s = "NH Summer";
            }
            else if (d >= 150 && d < 180)
            {
                s = "Late NH Summer";
            }
            else if (d >= 180 && d < 210)
            {
                s = "Early NH Fall";
            }
            else if (d >= 210 && d < 240)
            {
                s = "NH Fall";
            }
            else if (d >= 240 && d < 270)
            {
                s = "Late NH Fall";
            }
            else if (d >= 270 && d < 300)
            {
                s = "Early NH Winter";
            }
            else if (d >= 300 && d < 330)
            {
                s = "NH Winter";
            }
            else if (d>=330 )
            {
                s = "Late NH Winter";
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