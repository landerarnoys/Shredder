using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Howest.NMCT.Shredder.PhoneApp.Resources;
using MyToolkit.Multimedia;
using Howest.NMCT.Shredder.Lib.ViewModels;
using Howest.NMCT.Shredder.Models;

using Microsoft.Phone.Maps.Controls;
using System.Device.Location; // Provides the GeoCoordinate class.
using Windows.Devices.Geolocation; //Provides the Geocoordinate class.
using System.Windows.Shapes;
using System.Windows.Media;
using Microsoft.Live;
using Microsoft.Phone.Maps.Toolkit;
using System.Windows.Media.Imaging;


namespace Howest.NMCT.Shredder.PhoneApp
{

    public partial class MainPage : PhoneApplicationPage
    {
        private MainPageViewModel _vm = new MainPageViewModel();
        User u = new User();
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            DataContext = _vm;

            ShowMyLocationOnTheMap();
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            login();
            loadVideo();
           
        }

        private void showlocations()
        {
            foreach (Place p in _vm.Places)
            {
                Grid MyGrid = new Grid();
                MyGrid.RowDefinitions.Add(new RowDefinition());
                MyGrid.RowDefinitions.Add(new RowDefinition());
                MyGrid.Background = new SolidColorBrush(Colors.Transparent);

                BitmapImage bmi = new BitmapImage(new Uri("/Assets/pushpinPhone.png", UriKind.Relative));
                Image img = new Image();
                img.Tag = (p);
                img.Source = bmi;
               
        
                MyGrid.Children.Add(img);


                //Creating a MapOverlay and adding the Grid to it.
                MapOverlay MyOverlay = new MapOverlay();
                MyOverlay.Content = MyGrid;

                MyOverlay.GeoCoordinate = new GeoCoordinate(p.Latitude, p.Longitude);


                MyOverlay.PositionOrigin = new Point(0, 0.5);

                //Creating a MapLayer and adding the MapOverlay to it
                MapLayer MyLayer = new MapLayer();
                MyLayer.Add(MyOverlay);
                mapWithMyLocation.Layers.Add(MyLayer);
            }
        }

        private async void ShowMyLocationOnTheMap()
        {
            Geolocator myGeolocator = new Geolocator();
            Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
            Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
            GeoCoordinate myGeoCoordinate =
            CoordinateConverter.ConvertGeocoordinate(myGeocoordinate);


            // Make my current location the center of the Map.
            this.mapWithMyLocation.Center = myGeoCoordinate;
            this.mapWithMyLocation.ZoomLevel = 13;

            // Create a small circle to mark the current location.
            BitmapImage bmi = new BitmapImage(new Uri("/Assets/pushpinRood.png", UriKind.Relative));
            Image img = new Image();
            img.Source = bmi;

            // Create a MapOverlay to contain the circle.
            MapOverlay myLocationOverlay = new MapOverlay();
            myLocationOverlay.Content = img;
            myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
            myLocationOverlay.GeoCoordinate = myGeoCoordinate;

            // Create a MapLayer to contain the MapOverlay.
            MapLayer myLocationLayer = new MapLayer();
            myLocationLayer.Add(myLocationOverlay);

            // Add the MapLayer to the Map.
            mapWithMyLocation.Layers.Add(myLocationLayer);


 
        }


        private async void login()
        {
            try
            {
                string clientId = "000000004C10C962";
                LiveAuthClient auth = new LiveAuthClient(clientId);
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
        


      


        private async void loadVideo()
        {
            try
            {
                var url = await YouTube.GetVideoUriAsync("VYh9YDmCPoA", YouTubeQuality.Quality480P);
                mediaYoutube.Source = url.Uri;
                mediaYoutube.AutoPlay = false;
            }
            catch
            {
                
            }

        }

        private void sampleMap_Loaded(object sender, RoutedEventArgs e)
        {
           

        }

        //private void playVideo()
        //{            
        //    mediaYoutube.Play();
        //    btnPlay.Visibility = Visibility.Collapsed;
        //}

        private void playVideo(object sender, System.Windows.Input.GestureEventArgs e)
        {
            mediaYoutube.Play();
            btnPlay.Visibility = Visibility.Collapsed;
        }

        //private void pauzeVideo()
        //{

        //    mediaYoutube.Pause();
        //    btnPlay.Visibility = Visibility.Visible;
        //}

       

        //private void btnPlayTapped(object sender, System.Windows.Input.GestureEventArgs e)
        //{
        //    playVideo();
        //}

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
            mediaYoutube.Pause();
            btnPlay.Visibility = Visibility.Visible;
        }

        private void NavigateToTop10Photos(object sender, System.Windows.Input.GestureEventArgs e)
        {

            NavigationService.Navigate(new Uri("/TopTenPhotosPage.xaml?user=" + u.Email, UriKind.Relative));
        }

        private void NavigateToTop10Videos(object sender, System.Windows.Input.GestureEventArgs e)
        {

            NavigationService.Navigate(new Uri("/Top10VideosPage.xaml?user=" + u.Email, UriKind.Relative));
        }

        private void navigateToTopSpotsPage(object sender, System.Windows.Input.GestureEventArgs e)
        {

            NavigationService.Navigate(new Uri("/Top10SpotsPage.xaml?user=" + u.Email, UriKind.Relative));
        }

        private void NavigateToMyLocationPage(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MyLocationPage.xaml", UriKind.Relative));
            
        }

        private void bingmap1_TextChanged(object sender, TextChangedEventArgs e)
        {
            showlocations();

            double latitude = Convert.ToDouble(bingmap1Lat.Text);
            double longitude = Convert.ToDouble(bingmap1Long.Text);


             Grid MyGrid = new Grid();
                MyGrid.RowDefinitions.Add(new RowDefinition());
                MyGrid.RowDefinitions.Add(new RowDefinition());
                MyGrid.Background = new SolidColorBrush(Colors.Transparent);

                BitmapImage bmi = new BitmapImage(new Uri("/Assets/pushpinPhone.png", UriKind.Relative));
                Image img = new Image();
                img.Source = bmi;
             
        
                MyGrid.Children.Add(img);


                //Creating a MapOverlay and adding the Grid to it.
                MapOverlay MyOverlay = new MapOverlay();
                MyOverlay.Content = MyGrid;

                MyOverlay.GeoCoordinate = new GeoCoordinate(latitude, longitude);


                MyOverlay.PositionOrigin = new Point(0, 0.5);

                //Creating a MapLayer and adding the MapOverlay to it
                MapLayer MyLayer = new MapLayer();
                MyLayer.Add(MyOverlay);
                topspotMap.Layers.Add(MyLayer);

                topspotMap.ZoomLevel = 15;


                this.topspotMap.Center = MyOverlay.GeoCoordinate;
                ShowMyLocationOnTheMap();
        }

        private void txtVideoSource_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

   

        private void topspotMap_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ActiveUserAndSpot uas = new ActiveUserAndSpot();
            uas.Place = _vm.Places[1];
            uas.User = _vm.ActiveUser;
            u.Email = "lynncallant@telenet.be";
            NavigationService.Navigate(new Uri("/DetailSpotPage.xaml?user=" + u.Email + "&spotId=" + uas.Place.PlaceId, UriKind.Relative));        
        }
      

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}