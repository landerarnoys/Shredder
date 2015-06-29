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
using Howest.NMCT.Shredder.Models;

namespace Howest.NMCT.Shredder.PhoneApp
{
    public partial class DetailVideoPage : PhoneApplicationPage
    {
        private DetailVideoPageViewModel _vm = new DetailVideoPageViewModel();
        public DetailVideoPage()
        {
            InitializeComponent();
            DataContext = _vm;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);


            string userEmail;
            string id;


            if (NavigationContext.QueryString.TryGetValue("user", out userEmail))
            {
                _vm.GetUserByEmail(userEmail);
            }

            if (NavigationContext.QueryString.TryGetValue("videoId", out id))
            {
                int videoId = Convert.ToInt16(id);
                _vm.GetVideoById(videoId);
                _vm.LoadVideoComments(videoId);
            }





        }

        private void btnPlaceComment_Click(object sender, RoutedEventArgs e)
        {
            if (!txtComment.Text.Equals(""))
            {
                VideoComment vc = new VideoComment();
                vc.Comment = txtComment.Text;
                vc.VideoId = _vm.ActiveVideo.VideoId;
                vc.PlaceId = _vm.ActiveVideo.Place.PlaceId;
                vc.UserId = _vm.ActiveUser.UserId;
                _vm.SaveVideoComment(vc);
                txtComment.Text = "";
            }
        }

        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            goToVideo();
        }

        private void goToVideo()
        {
            string id = txtVideoId.Text;
            NavigationService.Navigate(new Uri("/VideoPlayer.xaml?videoId=" + id, UriKind.Relative));
        }

        private void imagePhoto_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            goToVideo();
        }
    }
}