using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Howest.NMCT.Shredder.Lib.ViewModels;
using MyToolkit.Multimedia;
using Howest.NMCT.Shredder.Models;

namespace Howest.NMCT.Shredder.PhoneApp
{
    public partial class VideoPlayer : PhoneApplicationPage
    {
        private DetailVideoPageViewModel _vm = new DetailVideoPageViewModel();
        Video video = new Video();
        public VideoPlayer()
        {
            InitializeComponent();
            DataContext = _vm;

        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string id;


            if (NavigationContext.QueryString.TryGetValue("videoId", out id))
            {
                int videoId = Convert.ToInt16(id);
                _vm.GetVideoById(videoId);

            }
        }

        private async void loadVideo()
        {
            try
            {
                var deelUrl = TxtVideoUrl.Text;
                var url = await YouTube.GetVideoUriAsync(deelUrl, YouTubeQuality.Quality480P);
                mediaYoutube.Source = url.Uri;
                mediaYoutube.AutoPlay = false;
            }
            catch
            {
                
            }

        }

        private void playVideo(object sender, System.Windows.Input.GestureEventArgs e)
        {

            mediaYoutube.Play();
            btnPlay.Visibility = Visibility.Collapsed;
        }


        private void VideoTapped(object sender, System.Windows.Input.GestureEventArgs e)
        {

            mediaYoutube.Pause();
            btnPlay.Visibility = Visibility.Visible;
        }

        private void btnLoad_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void TxtVideoUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadVideo();
        }

        
    }
}