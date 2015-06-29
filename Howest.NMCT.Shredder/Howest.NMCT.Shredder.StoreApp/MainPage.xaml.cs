using Howest.NMCT.Shredder.Lib.ViewModels;
using Howest.NMCT.Shredder.Models;
using Microsoft.Live;
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
using MyToolkit.Multimedia; //namespace for youtube video's
using Bing.Maps;
using Windows.Devices.Geolocation;
using System.Threading;
using System.Threading.Tasks;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Howest.NMCT.Shredder.StoreApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private MainPageViewModel _vm = new MainPageViewModel();
        private Geolocator _geolocator = null;
        private CancellationTokenSource _cts = null;
        LocationIcon10m _locationIcon10m;
        LocationIcon100m _locationIcon100m;
        currentLocationPin _currentLocationPin;
        public MainPage()
        {
            this.InitializeComponent();
            _geolocator = new Geolocator();
            _locationIcon10m = new LocationIcon10m();
            _locationIcon100m = new LocationIcon100m();
            _currentLocationPin = new currentLocationPin();
            DataContext = _vm;
        }

        private async void getGpsLocation()
        {
            // Remove any previous location icon.
            if (bingMap.Children.Count > 0)
            {
                bingMap.Children.RemoveAt(0);
            }

            try
            {
                // Get the cancellation token.
                _cts = new CancellationTokenSource();
                CancellationToken token = _cts.Token;

                // Get the location.
                Geoposition pos = await _geolocator.GetGeopositionAsync().AsTask(token);
                Location location = new Location(pos.Coordinate.Latitude, pos.Coordinate.Longitude);

                // Now set the zoom level of the map based on the accuracy of our location data.
                // Default to IP level accuracy. We only show the region at this level - No icon is displayed.
                double zoomLevel = 13.0f;

                // if we have GPS level accuracy
                if (pos.Coordinate.Accuracy <= 10)
                {
                    // Add the 10m icon and zoom closer.
                    bingMap.Children.Add(_currentLocationPin);
                    MapLayer.SetPosition(_currentLocationPin, location);
                    zoomLevel = 15.0f;
                }
                // Else if we have Wi-Fi level accuracy.
                else if (pos.Coordinate.Accuracy <= 100)
                {
                    // Add the 100m icon and zoom a little closer.
                    bingMap.Children.Add(_currentLocationPin);
                    MapLayer.SetPosition(_currentLocationPin, location);
                    zoomLevel = 14.0f;
                }

                // Set the map to the given location and zoom level.
                bingMap.SetView(location, zoomLevel);
            }
            catch (System.UnauthorizedAccessException)
            {

            }
            catch (TaskCanceledException)
            {

            }
            finally
            {
                _cts = null;
            }

        }

        //Method to load the youtube video on the mainpage
        private async void LoadVideo()
        {
            string videoSource = txtVideoSource.Text;
            var url = await YouTube.GetVideoUriAsync(videoSource, YouTubeQuality.Quality480P);
            mediaYoutube.Source = url.Uri;
            mediaYoutube.AutoPlay = false;
        }

        private void Toggle_PlayPressed_Click(object sender, RoutedEventArgs e)
        {
            play.Visibility = Visibility.Collapsed;
            mediaYoutube.Play();
        }


        private void pauzeVideo(object sender, TappedRoutedEventArgs e)
        {
            play.Visibility = Visibility.Visible;
            mediaYoutube.Pause();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            getGpsLocation();
            try
            {
                LiveAuthClient auth = new LiveAuthClient();
                LiveLoginResult initializeResult = await auth.InitializeAsync();
                try
                {
                    LiveLoginResult loginResult = await auth.LoginAsync(new string[] { "wl.basic", "wl.emails" });
                    if (loginResult.Status == LiveConnectSessionStatus.Connected)
                    {
                        LiveConnectClient connect = new LiveConnectClient(auth.Session);
                        LiveOperationResult operationResult = await connect.GetAsync("me");
                        dynamic result = operationResult.Result;
                        if (result != null)
                        {
                            User u = new User();
                            u.FirstName = result.first_name;
                            u.LastName = result.last_name;
                            u.Email = result.emails.account;
                            _vm.SaveUser(u);
                            _vm.ActiveUser = u;
                        }
                    }
                }
                catch (LiveAuthException exception)
                {
                }
                catch (LiveConnectException exception)
                {
                }
            }
            catch (LiveAuthException exception)
            {
            }
        }

        private void goToMaps(object sender, TappedRoutedEventArgs e)
        {
            if (_vm.ActiveUser == null)
            {

            }
            else
            {
                Frame.Navigate(typeof(DetailMapPage), _vm.ActiveUser);
            }        
        }


        private void navigateToTopTenPhoto(object sender, TappedRoutedEventArgs e)
        {
            if (_vm.ActiveUser == null)
            {

            }
            else
            {
                Frame.Navigate(typeof(TopTenPhotoPage), _vm.ActiveUser);
            }
        }

        private void navigateToTopTenSpots(object sender, TappedRoutedEventArgs e)
        {
            if (_vm.ActiveUser == null)
            {

            }
            else
            {
                Frame.Navigate(typeof(TopTenSpotsPage), _vm.ActiveUser);
            }
        }


        private void navigateToTopTenVideos(object sender, TappedRoutedEventArgs e)
        {
            if (_vm.ActiveUser == null)
            {

            }
            else
            {
                Frame.Navigate(typeof(TopTenVideosPage), _vm.ActiveUser);
            }
        }

        private void bingmap1_TextChanged(object sender, TextChangedEventArgs e)
        {
            double latitude = Convert.ToDouble(bingmap1Lat.Text);
            double longitude = Convert.ToDouble(bingmap1Long.Text);
            bingMapTop1.SetView(new Location(latitude, longitude));

            //fill up big bing map
            foreach (Place p in _vm.Places)
            {
                //Pushpin pushpin = new Pushpin();
                //MapLayer.SetPosition(pushpin, new Location(p.Latitude, p.Longitude));
                //bingMap.Children.Add(pushpin);

                LocationIcon10m loc = new LocationIcon10m();
                MapLayer.SetPosition(loc, new Location(p.Latitude, p.Longitude));
                bingMap.Children.Add(loc);
            }
        }

        private void bingmap2_TextChanged(object sender, TextChangedEventArgs e)
        {
            double latitude = Convert.ToDouble(bingmap2Lat.Text);
            double longitude = Convert.ToDouble(bingmap2Long.Text);
            bingMapTop2.SetView(new Location(latitude, longitude));

        }

        private void bingmap3_TextChanged(object sender, TextChangedEventArgs e)
        {
            double latitude = Convert.ToDouble(bingmap3Lat.Text);
            double longitude = Convert.ToDouble(bingmap3Long.Text);
            bingMapTop3.SetView(new Location(latitude, longitude));

        }

        private void Picture1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (_vm.ActiveUser == null)
            {

            }
            else
            {
                ActiveUserAndPicture uap = new ActiveUserAndPicture();
                uap.Picture = _vm.Pictures[0];
                uap.User = _vm.ActiveUser;
                Frame.Navigate(typeof(DetailPhotoPage), uap);
            }
        }

        private void Picture2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (_vm.ActiveUser == null)
            {

            }
            else
            {
                ActiveUserAndPicture uap = new ActiveUserAndPicture();
                uap.Picture = _vm.Pictures[1];
                uap.User = _vm.ActiveUser;
                Frame.Navigate(typeof(DetailPhotoPage), uap);
            }
        }

        private void Picture3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (_vm.ActiveUser == null)
            {

            }
            else
            {
                ActiveUserAndPicture uap = new ActiveUserAndPicture();
                uap.Picture = _vm.Pictures[2];
                uap.User = _vm.ActiveUser;
                Frame.Navigate(typeof(DetailPhotoPage), uap);
            }
        }

        private void Spot1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (_vm.ActiveUser == null)
            {

            }
            else
            {
                ActiveUserAndSpot uas = new ActiveUserAndSpot();
                uas.Place = _vm.Places[0];
                uas.User = _vm.ActiveUser;
                Frame.Navigate(typeof(DetailSpotPage), uas);
            }
        }

        private void Spot2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (_vm.ActiveUser == null)
            {

            }
            else
            {
                ActiveUserAndSpot uas = new ActiveUserAndSpot();
                uas.Place = _vm.Places[1];
                uas.User = _vm.ActiveUser;
                Frame.Navigate(typeof(DetailSpotPage), uas);
            }
        }

        private void Spot3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (_vm.ActiveUser == null)
            {

            }
            else
            {
                ActiveUserAndSpot uas = new ActiveUserAndSpot();
                uas.Place = _vm.Places[2];
                uas.User = _vm.ActiveUser;
                Frame.Navigate(typeof(DetailSpotPage), uas);
            }
        }

        private void txtVideoSource_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.LoadVideo();
        }
    }
}
