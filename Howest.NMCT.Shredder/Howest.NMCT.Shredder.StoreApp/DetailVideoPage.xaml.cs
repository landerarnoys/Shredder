using Howest.NMCT.Shredder.Lib.ViewModels;
using Howest.NMCT.Shredder.Models;
using Howest.NMCT.Shredder.StoreApp.Common;
using MyToolkit.Multimedia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Howest.NMCT.Shredder.StoreApp
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class DetailVideoPage : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private DetailVideoPageViewModel _vm = new DetailVideoPageViewModel();
        private ActiveUserAndVideo uav;

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public DetailVideoPage()
        {
            this.InitializeComponent();
            DataContext = _vm;
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
            uav = (ActiveUserAndVideo)e.Parameter;
            _vm.ActiveVideo = uav.Video;
            _vm.GetUserByEmail(uav.User.Email);
            _vm.LoadVideoComments(uav.Video.VideoId);
            this.LoadVideo(_vm.ActiveVideo.Url);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void Rating_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int oldrating = _vm.ActiveVideo.Rating;
            int r = Convert.ToInt32(ratVideo.Value);
            double som = oldrating + r;
            double newrating = Math.Ceiling(som / 2);
            _vm.ActiveVideo.Rating = Convert.ToInt32(newrating);
            _vm.UpdateVideoRating(_vm.ActiveVideo);
            ratVideo.Value = _vm.ActiveVideo.Rating;
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

        private void Toggle_PlayPressed_Click(object sender, RoutedEventArgs e)
        {
            play.Visibility = Visibility.Collapsed;
            mediaYoutube.Play();
        }


        private void PauseVideo(object sender, TappedRoutedEventArgs e)
        {
            play.Visibility = Visibility.Visible;
            mediaYoutube.Pause();
        }

        private async void LoadVideo(String vidurl)
        {
            var url = await YouTube.GetVideoUriAsync(vidurl, YouTubeQuality.Quality480P);
            mediaYoutube.Source = url.Uri;
            mediaYoutube.AutoPlay = false;
        }
    }
}
