using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyToolkit.Multimedia;
using Howest.NMCT.Shredder.Lib.ViewModels;
using Howest.NMCT.Shredder.Models;

namespace Howest.NMCT.Shredder.PhoneApp
{
    public partial class Top10VideosPage : PhoneApplicationPage
    {

        private TopTenVideosViewModel _vm = new TopTenVideosViewModel();
        string userEmail;

        public Top10VideosPage()
        {
            InitializeComponent();
            DataContext = _vm;
        }

      
        private void VideoTapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //if (btnPlay.Visibility == Visibility.Collapsed)
            //{
            //    pauzeVideo();
            //}
            //else if (btnPlay.Visibility == Visibility.Visible)
            //{
            //    playVideo();
            //}
         
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (NavigationContext.QueryString.TryGetValue("user", out userEmail))
            {
                string blablaalb = userEmail;
            }


        }

        private void playVideo(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void Grid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Video video = (Video)VideoList.SelectedItem;

            NavigationService.Navigate(new Uri("/DetailVideoPage.xaml?user=" + userEmail + "&videoId=" + video.VideoId, UriKind.Relative));

        }



     
    
    }
}