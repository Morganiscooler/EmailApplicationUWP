using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.WebUI;
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
        //private Timer timer;

        private DispatcherTimer timer2;

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
            //Timer t = new Timer(new TimerCallback(TimerProc));
            //t.Change(dueTime, 0);

        }
        private async void TimerProc(object state) // TimerProc happens after the 5 seconds
        {
            // The state object is the Timer object.
            //Timer t = (Timer)state;
            //t.Dispose();
            //Debug.WriteLine("The timer callback executes properly.");

            // execute the webview code within here

            //ReadXml(); The FUNCTION we're calling back to
            //string resulthtml = null;
            //try 
            //{
            //    resulthtml = await Gmail.InvokeScriptAsync("eval", new string[] { "document" }); // handle exception error
            //    Debug.WriteLine(resulthtml);
            //} 
            //catch(Exception ex)
            //{
            //    // failed output
            //}

            //ReadXml(); // We need arguments in this function to make it WORK

        }

        // Grabbing information from webview (Gmail) control
        private async Task ReadXml(StorageFile file) 
        {
            string htmlContent = await FileIO.ReadTextAsync(file);
            Debug.WriteLine("This is " + htmlContent);


            XDocument doc = XDocument.Parse(htmlContent);
            var EmailList = doc.Root.Descendants(XName.Get("emails"));
            if (EmailList.Count() > 0)
            {
                string Emails = EmailList.First().Value;
                Emails = Regex.Replace(Emails, @"<[^>]*>", ""); // javascript injection
                // do other things...

            }
        }



        private async void Button_Click(object sender, RoutedEventArgs e) // another potential route = using a button to load in Emails
        {
            try
            {
                string[] arguments = new string[] { @"window.getSelection().toString();" };
                var s = await Gmail.InvokeScriptAsync("eval", arguments);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        //define WebBrowserLoadGmail's function
        private void WebBrowserLoadGmail() // Loading web browser (function)
        {
            string site = @"https://mail.google.com/mail/u/0/#inbox"; // site we are headed towards
            Gmail.Navigate(new Uri(site)); // navigating via webbrowser control

        }


        //Gmail.ContentLoading += Gmail_ContentLoading;
        private void Gmail_ContentLoading(WebView sender, WebViewContentLoadingEventArgs args) // function binded to a event
        {      
            // Show status.
            if (args.Uri != null)
            {   
                { 
                    string output = "Loading content for " + args.Uri.ToString();
                    Debug.WriteLine(output);
                    //StartTimer(10 * 1000); //argument goes in parenthesis

                    DispatcherTimer dispatcherTimer = new DispatcherTimer();
                    dispatcherTimer.Tick += dispatcherTimer_Tick;
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 5); // every 5 seconds, a tick occurs, and the function is called
                    dispatcherTimer.Start();
                }
               
            }
        }
        private async void dispatcherTimer_Tick(object sender, object e)
        {
            Debug.WriteLine("Dispatch Timer Done");
            string resulthtml = await Gmail.InvokeScriptAsync("eval", new string[] { "document.documentElement.outerHTML;" }); // handle exception error
            Debug.WriteLine("resulthtml: " + resulthtml);
            (sender as DispatcherTimer).Stop();
        }

        private void Gmail_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            //var callback = new MainPage();
            //Gmail.AddWebAllowedObject("MainPage", callback);
        }
    }
  }

