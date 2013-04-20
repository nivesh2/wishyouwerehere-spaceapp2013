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
using MyToolkit.Controls;
using MyToolkit.Multimedia;
using System.ComponentModel;
using System.Windows.Navigation;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
namespace PhoneApp10
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            //MediaPlayerLauncher mediaPlayerLauncher = new MediaPlayerLauncher();
            //mediaPlayerLauncher.Media = new Uri(@"http://ecn.channel9.msdn.com/o9/ch9/7627/13997a12-79ff-4f95-9686-9de901877627/WP7JumpStartSession12_ch9.mp3", UriKind.Absolute);
            //mediaPlayerLauncher.Controls = MediaPlaybackControls.All;
            //mediaPlayerLauncher.Location = MediaLocationType.Data;
            //mediaPlayerLauncher.Show(); 
            
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