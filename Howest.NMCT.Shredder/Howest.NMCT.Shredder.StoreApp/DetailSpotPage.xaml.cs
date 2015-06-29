using Howest.NMCT.Shredder.StoreApp.Common;
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
using Howest.NMCT.Shredder.Models;
using Howest.NMCT.Shredder.Lib.ViewModels;
using Bing.Maps;
using System.Diagnostics;
using Windows.Storage.Pickers;
using Windows.Storage;
//using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure.Storage.Auth;
//using Microsoft.WindowsAzure.Storage.Blob;
using System.Text.RegularExpressions;
using Windows.ApplicationModel.DataTransfer;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Howest.NMCT.Shredder.StoreApp
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class DetailSpotPage : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private DetailSpotPageViewModel _vm = new DetailSpotPageViewModel();
        private ActiveUserAndSpot uas;
        //private CloudStorageAccount _storageAccount = null;
        private StorageFile file = null;
        private static readonly Regex YoutubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:(.*)v(/|=)|(.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase);
        private DataTransferManager dataTransferManager;

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


        public DetailSpotPage()
        {
            this.InitializeComponent();
            DataContext = _vm;
           // _storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=shredder;AccountKey=TI86ZnrNivB7ITMKmIHzYDu7kr/am7PYT6DWiZKfSlIDw2R6TDsSYxTBUDRFjXhyXoMq6DD7hGIvQM566f1cIw==");
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
            dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += dataTransferManager_DataRequested;
        }

        private void dataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            Place p = _vm.ActivePlace;
            DataRequest req = args.Request;
            req.Data.Properties.Title = p.Name + " is an awesome spot!";
            req.Data.Properties.Description = "Share this spot now.";
            req.Data.SetText("I went to " + p.Name + " and this spot is great. Here are the GPS coordinates if u want to check it out yourself. Latitude: " + p.Latitude + " Longitude: " + p.Longitude);
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
            uas = (ActiveUserAndSpot)e.Parameter;
            _vm.ActivePlace = uas.Place;
            _vm.GetUserByEmail(uas.User.Email);
            _vm.LoadPlaceComments(_vm.ActivePlace.PlaceId);
            _vm.LoadPictures(_vm.ActivePlace.PlaceId);
            _vm.LoadVideos(_vm.ActivePlace.PlaceId);
            bingMap.SetView(new Location(_vm.ActivePlace.Latitude, _vm.ActivePlace.Longitude));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
            dataTransferManager.DataRequested -= dataTransferManager_DataRequested;
        }

        #endregion


        private void Rating_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int oldrating = _vm.ActivePlace.Rating;
            int r = Convert.ToInt32(rateSpot.Value);
            double som = oldrating + r;
            double newrating = Math.Ceiling(som / 2);
            _vm.ActivePlace.Rating = Convert.ToInt32(newrating);
            _vm.UpdateSpotRating(_vm.ActivePlace);
            rateSpot.Value = _vm.ActivePlace.Rating;
        }

        private void btnPlaceComment_Click(object sender, RoutedEventArgs e)
        {
            if (!txtComment.Text.Equals(""))
            {
                PlaceComment pc = new PlaceComment();
                pc.Comment = txtComment.Text;
                pc.PlaceId = uas.Place.PlaceId;
                pc.UserId = _vm.ActiveUser.UserId;
                _vm.SavePlaceComment(pc);
                txtComment.Text = "";                
            }
        }

        private void lsvPictures_ItemClick(object sender, ItemClickEventArgs e)
        {
            ActiveUserAndPicture uap = new ActiveUserAndPicture();
            uap.Picture = (Picture)e.ClickedItem;
            uap.User = _vm.ActiveUser;
            Frame.Navigate(typeof(DetailPhotoPage), uap);
        }

        private void lsvVideos_ItemClick(object sender, ItemClickEventArgs e)
        {
            ActiveUserAndVideo uav = new ActiveUserAndVideo();
            uav.Video = (Video)e.ClickedItem;
            uav.User = _vm.ActiveUser;
            Frame.Navigate(typeof(DetailVideoPage), uav);
        }

        private async void btnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                NewPictureLayOver.Visibility = Visibility.Visible;
            }
        }

        private void btnAddVideo_Click(object sender, RoutedEventArgs e)
        {
            NewVideoLayOver.Visibility = Visibility.Visible;
        }

        private async void btnMakeNewPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (!txtPhotoName.Text.Equals("") && !txtPhotoDescription.Text.Equals(""))
            {
                var randomAccessStream = await file.OpenReadAsync();
                Stream stream = randomAccessStream.AsStreamForRead();
                Windows.Storage.Streams.IInputStream inStream = stream.AsInputStream();

                //Create BLOBClient
                //CloudBlobClient blobClient = _storageAccount.CreateCloudBlobClient();

                //Retrieve a reference to a container that we want to use
                //CloudBlobContainer container = blobClient.GetContainerReference("pictures");
                //CloudBlockBlob blockBlob = container.GetBlockBlobReference(file.Name);
                await blockBlob.UploadFromStreamAsync(inStream);

                Picture p = new Picture();
                p.Title = txtPhotoName.Text;
                p.Description = txtPhotoDescription.Text;
                p.UserId = _vm.ActiveUser.UserId;
                p.PlaceId = _vm.ActivePlace.PlaceId;
                p.Url = "http://shredder.blob.core.windows.net/pictures/" + file.Name;
                p.Rating = 0;
                _vm.SavePicture(p);
                NewPictureLayOver.Visibility = Visibility.Collapsed;
            }
        }

        private Boolean IsYoutubeLink(String url)
        {
            Match youtubeMatch = YoutubeVideoRegex.Match(url);
            if (youtubeMatch.Success)
            {
                return true;
            }
            else {
                return false;
            }
        }


        private void btnMakeNewVideo_Click(object sender, RoutedEventArgs e)
        {
            String link = txtYoutube.Text;
            if (!txtVideoName.Text.Equals("") && !txtVideoDescription.Text.Equals("") && !txtYoutube.Text.Equals("") && IsYoutubeLink(link))
            {
                int index = link.IndexOf('=');
                string yt = link.Substring(index + 1);
                Video v = new Video();
                v.Title = txtVideoName.Text;
                v.Description = txtVideoDescription.Text;
                v.Url = yt;
                v.UserId = _vm.ActiveUser.UserId;
                v.PlaceId = _vm.ActivePlace.PlaceId;
                v.Rating = 0;
                v.Thumbnail = "http://img.youtube.com/vi/" + yt + "/0.jpg";
                _vm.SaveVideo(v);
                NewVideoLayOver.Visibility = Visibility.Collapsed;
            }
        }

        private void closePictureLayover(object sender, TappedRoutedEventArgs e)
        {
            NewPictureLayOver.Visibility = Visibility.Collapsed;
        }

        private void closeVideoLayover(object sender, TappedRoutedEventArgs e)
        {
            NewVideoLayOver.Visibility = Visibility.Collapsed;
        }

    }
}
