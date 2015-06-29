using Howest.NMCT.Shredder.StoreApp.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;

using Windows.Devices.Geolocation;
using Bing.Maps;
using Howest.NMCT.Shredder.Lib.ViewModels;
using Howest.NMCT.Shredder.Models;
using Windows.UI.Popups;
using System.Diagnostics;




// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Howest.NMCT.Shredder.StoreApp
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class DetailMapPage : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();


        private Geolocator _geolocator = null;
        private CancellationTokenSource _cts = null;
        LocationIcon10m _locationIcon10m;
        LocationIcon100m _locationIcon100m;
        currentLocationPin _currentLocationPin;
        MyLocationIcon _locationIcon;

        Location myTappedLocation = new Location();

        private DetailMapPageViewModel _vm = new DetailMapPageViewModel();

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


        public DetailMapPage()
        {
            this.InitializeComponent();

            DataContext = _vm;



            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;


            _geolocator = new Geolocator();
            _locationIcon10m = new LocationIcon10m();
            _locationIcon100m = new LocationIcon100m();
            _locationIcon = new MyLocationIcon();
            _currentLocationPin = new currentLocationPin();
            goToLocation();



        }





        async private void goToLocation()
        {
            // Remove any previous location icon.
            if (myMap.Children.Count > 0)
            {
                myMap.Children.RemoveAt(0);
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
                    myMap.Children.Add(_currentLocationPin);
                    MapLayer.SetPosition(_currentLocationPin, location);
                    zoomLevel = 15.0f;

                }
                // Else if we have Wi-Fi level accuracy.
                else if (pos.Coordinate.Accuracy <= 100)
                {
                    // Add the 100m icon and zoom a little closer.
                    myMap.Children.Add(_currentLocationPin);
                    MapLayer.SetPosition(_currentLocationPin, location);
                    zoomLevel = 14.0f;

                }

                // Set the map to the given location and zoom level.
                myMap.SetView(location, zoomLevel);

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
            User u = (User)e.Parameter;
            _vm.GetUserByEmail(u.Email);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {

            if (_cts != null)
            {
                _cts.Cancel();
                _cts = null;
            }

            navigationHelper.OnNavigatedFrom(e);

        }

        #endregion

        private void navigateToTopTenSpots(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(TopTenSpotsPage), _vm.ActiveUser);
        }
        

        private void Map_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (myMap.Children.Contains(_locationIcon))
            {
                myMap.Children.Remove(_locationIcon);
            }

            var pos = e.GetPosition(myMap);
            Bing.Maps.Location location;
            myMap.TryPixelToLocation(pos, out location);

            //fill up myTappedLocation var for making a new spot
            myTappedLocation = location;
            MapLayer.SetPosition(_locationIcon, location);
            myMap.Children.Add(_locationIcon);
            myMap.SetView(location);
        }


        private void showImageBig(object sender, TappedRoutedEventArgs e)
        {
            NewPlaceLayOver.Visibility = Visibility.Visible;
        }

        private void closeLayover(object sender, TappedRoutedEventArgs e)
        {
            NewPlaceLayOver.Visibility = Visibility.Collapsed;
        }

        private void goToLocation_Click(object sender, RoutedEventArgs e)
        {
            goToLocation();
        }

        private void OnPushTapped(object sender, RoutedEventArgs e)
        {
            LocationIcon10m locationPin = sender as LocationIcon10m;
            if (locationPin != null)
            {
                var x = MapLayer.GetPosition(locationPin);
                double lat = x.Latitude;
               // _vm.GetSpotByLatitude(lat);
                foreach (Place p in _vm.Places)
                {
                    if (p.Latitude == x.Latitude || p.Longitude == x.Longitude)
                    {
                        //txtTappedPlace.Text = p.PlaceId.ToString();
                        ActiveUserAndSpot uas = new ActiveUserAndSpot();
                        uas.Place = p;
                        uas.User = _vm.ActiveUser;
                        Frame.Navigate(typeof(DetailSpotPage), uas);

                    }
                }
            }
        }

        private void MakeNewSpot(object sender, TappedRoutedEventArgs e)
        {
            Place newPlace = new Place();
            newPlace.UserId = _vm.ActiveUser.UserId;
            newPlace.Longitude = myTappedLocation.Longitude;
            newPlace.Latitude = myTappedLocation.Latitude;
            newPlace.Name = txtNewSpotName.Text;
            newPlace.Description = txtDescription.Text;
            newPlace.Rating = 0;
            _vm.saveSpot(newPlace);
            NewPlaceLayOver.Visibility = Visibility.Collapsed;
        }



        private void txtPlacesLoaded_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (Place p in _vm.Places)
            {
                LocationIcon10m pushpin = new LocationIcon10m();
                MapLayer.SetPosition(pushpin, new Location(p.Latitude, p.Longitude));
                pushpin.Tapped += new TappedEventHandler(OnPushTapped);
                myMap.Children.Add(pushpin);
            }
        }


    }
}
