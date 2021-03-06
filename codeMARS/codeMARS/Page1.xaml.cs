﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;

namespace codeMARS
{
    public partial class Page1 : PhoneApplicationPage
    {
        IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
        int index=0;
        public Page1()
        {
            InitializeComponent();
        }

        // same as UpdateHomePAGE only index number is changed, which we get with the help from QueryString
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string s;
            NavigationContext.QueryString.TryGetValue("id", out s);
           
            switch (s)
            {
                case "0": 
                    index = 0;
                    break;
                case "1":
                    index = 1;
                    break;
                case "2":
                    index = 2;
                    break;
                case "3":
                    index = 3;
                    break;
                case "4":
                    index = 4;
                    break;
                case "5":
                    index = 5;
                    break;
                case "6":
                    index = 6;
                    break;
                case "7":
                    index = 7;
                    break;
                case "8":
                    index = 8;
                    break;
                case "9":
                    index = 9;
                    break;
               
               
                default:
                    break;
            }


            RootObject deserializedData = ReadToObject((string)appSettings["json"]);
            updateHomePage(deserializedData.results[index]);

            base.OnNavigatedTo(e);



        }

        public void updateHomePage(Result r)
        {
            Earth_timeBlock.Text = "Mars Weather on " + month(r.terrestrial_date);

            max_tempBlock.Text = r.max_temp_fahrenheit.ToString("#") + "°F";
            min_tempBlock.Text = r.min_temp_fahrenheit.ToString() + "°F";


            pressureBlock.Text = "Pressure : " + r.pressure.ToString() + " hPa, " + r.pressure_string;

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


            sun_image.Source = new BitmapImage(new Uri(path(r), UriKind.Relative));
            sol_block.Text = "SOL : " + r.sol;

            string img = "/direction";
            string theme = "/light";
            string dr = "NORTH";
            img += theme;

            #region wind direction
            string wd = r.wind_direction;
            if (wd==null)
    	    {
                img += "/n.png";
                dr = "NO DATA";
	        }
            else if (wd == "E")
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

            if (s2 == "01")
            {
                s2 = "January";
            }
            else if (s2 == "02")
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

        public string path(Result r)
        {
            string imagepath = "/light/Cloud-Sun.png";

            if (r.atmo_opacity == null)
            {
                imagepath = "/light/Sun.png";
            }
            else if (r.atmo_opacity == "Sunny")
            {
                imagepath = "/light/Sun.png";
            }
            else if (r.atmo_opacity == "Cloudy")
            {
                imagepath = "/light/Cloud-Sun.png";
            }
            else if (r.atmo_opacity == "Thunder")
            {
                imagepath = "/light/Thunder.png";
            }
            else if (r.atmo_opacity == "Rainy")
            {
                imagepath = "/light/Cloud Rain.png";
            }
            else if (r.atmo_opacity == "Storm")
            {
                imagepath = "/light/Storm.png";
            }

            return imagepath;


        }

        public string season(double d)
        {
            string s = "";

            if (d >= 0 && d < 30)
            {
                s = "Early Northern Spring";

            }
            else if (d >= 30 && d < 60)
            {
                s = "Northern Spring";
            }
            else if (d >= 60 && d < 90)
            {
                s = "Late Northern Spring";
            }
            else if (d >= 90 && d < 120)
            {
                s = "Early Northern Summer";
            }
            else if (d >= 120 && d < 150)
            {
                s = "Northern Summer";
            }
            else if (d >= 150 && d < 180)
            {
                s = "Late Northern Summer";
            }
            else if (d >= 180 && d < 210)
            {
                s = "Early Northern Autumn";
            }
            else if (d >= 210 && d < 240)
            {
                s = "Northern Autumn";
            }
            else if (d >= 240 && d < 270)
            {
                s = "Late Northern Autumn";
            }
            else if (d >= 270 && d < 300)
            {
                s = "Early Northern Winter";
            }
            else if (d >= 300 && d < 330)
            {
                s = "Northern Winter";
            }
            else if (d >= 330)
            {
                s = "Late Northern Winter";
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
        public class SampleResult
        {
            public string ImagePath
            {
                get;
                set;
            }

            public int sol
            {
                get;
                set;
            }

            public string max_temp_fahrenheit { get; set; }
            public string min_temp_fahrenheit { get; set; }

        }
        public static RootObject ReadToObject(string json)
        {
            RootObject deserializedUser = new RootObject();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as RootObject;
            ms.Close();
            return deserializedUser;
        }


    }
}