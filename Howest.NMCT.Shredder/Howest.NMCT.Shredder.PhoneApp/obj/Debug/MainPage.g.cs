﻿#pragma checksum "C:\Shredder\ShredderApp\src\Howest.NMCT.Shredder\Howest.NMCT.Shredder.PhoneApp\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0A7D5975DCA0A5B620E173A7EE62FF65"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Howest.NMCT.Shredder.PhoneApp {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Grid workaroundBingMapsAndYoutubeVideo;
        
        internal System.Windows.Controls.TextBox bingmap1Lat;
        
        internal System.Windows.Controls.TextBox bingmap1Long;
        
        internal System.Windows.Controls.TextBox txtVideoSource;
        
        internal Microsoft.Phone.Maps.Controls.Map mapWithMyLocation;
        
        internal Microsoft.Phone.Maps.Controls.Map topspotMap;
        
        internal System.Windows.Controls.MediaElement mediaYoutube;
        
        internal System.Windows.Controls.Button btnPlay;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Howest.NMCT.Shredder.PhoneApp;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.workaroundBingMapsAndYoutubeVideo = ((System.Windows.Controls.Grid)(this.FindName("workaroundBingMapsAndYoutubeVideo")));
            this.bingmap1Lat = ((System.Windows.Controls.TextBox)(this.FindName("bingmap1Lat")));
            this.bingmap1Long = ((System.Windows.Controls.TextBox)(this.FindName("bingmap1Long")));
            this.txtVideoSource = ((System.Windows.Controls.TextBox)(this.FindName("txtVideoSource")));
            this.mapWithMyLocation = ((Microsoft.Phone.Maps.Controls.Map)(this.FindName("mapWithMyLocation")));
            this.topspotMap = ((Microsoft.Phone.Maps.Controls.Map)(this.FindName("topspotMap")));
            this.mediaYoutube = ((System.Windows.Controls.MediaElement)(this.FindName("mediaYoutube")));
            this.btnPlay = ((System.Windows.Controls.Button)(this.FindName("btnPlay")));
        }
    }
}

