using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

        }
    }
}
