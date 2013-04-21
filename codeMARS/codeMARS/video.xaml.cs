using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.ComponentModel;
using MyToolkit.Multimedia;
using MyToolkit.Controls;

namespace codeMARS
{
    public partial class video : PhoneApplicationPage
    {
        public video()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (YouTube.CancelPlay()) // used to abort current youtube download
                e.Cancel = true;
            else
            {
                // your code here
            }
            base.OnBackKeyPress(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            YouTube.CancelPlay(); // used to reenable page

            // your code here
            base.OnNavigatedTo(e);
        }

        private void OnButtonClick(object o, RoutedEventArgs e)
        {
            var ctrl = (YouTubeButton)o;

            YouTube.Play(ctrl.YouTubeID, true, YouTubeQuality.Quality480P, x =>
            {
                if (x != null)
                    MessageBox.Show(x.Message);
            });
        }
    }
}