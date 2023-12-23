using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MUXC = Microsoft.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EmailApplicationUWPVersion
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Timer timer;
        public MainPage()
        {
            this.InitializeComponent();
        
            // This will automatically adjust the window size to 75% of the screen it loads on.
            // You can't disable resizing.
            DisplayInformation displayInformation = DisplayInformation.GetForCurrentView();
            //Debug.WriteLine(displayInformation.ScreenWidthInRawPixels);
            //Debug.WriteLine(displayInformation.ScreenHeightInRawPixels);
            ApplicationView.PreferredLaunchViewSize = new Size(displayInformation.ScreenWidthInRawPixels * 0.75, displayInformation.ScreenHeightInRawPixels * 0.75);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            WebBrowserLoadGmail();
            
        }

        // I'm confused about these two functions 
        private void StartTimer(int dueTime) // arg: takes in a number (dueTime)
        {
            Timer t = new Timer(new TimerCallback(TimerProc));
            t.Change(dueTime, 0);

        }
        private void TimerProc(object state) // arg: takes 1 argument for object (class)
        {
            // The state object is the Timer object.
            Timer t = (Timer)state;
            t.Dispose();
            Debug.WriteLine("The timer callback executes properly.");
        }


        //define WebBrowserLoadGmail's function
        private void WebBrowserLoadGmail() // Loading web browser (function)
        {
            string site = @"https://mail.google.com/mail/u/0/#inbox"; // site we are headed towards
            Gmail.Navigate(new Uri(site)); // navigating via webbrowser control

        }


        //Gmail.ContentLoading += Gmail_ContentLoading;
        private void Gmail_ContentLoading(WebView sender, WebViewContentLoadingEventArgs args)
        {      
            // Show status.
            if (args.Uri != null)
            {
                string output = "Loading content for " + args.Uri.ToString();                    
                Debug.WriteLine(output);
                StartTimer(1 * 1000); //argument goes in parenthesis
            }
        }
         
      
    }
  }

