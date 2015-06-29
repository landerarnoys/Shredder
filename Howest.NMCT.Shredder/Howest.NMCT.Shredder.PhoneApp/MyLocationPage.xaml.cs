using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Devices.Geolocation;
using System.Device.Location;
using System.Windows.Shapes;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Media;
using Howest.NMCT.Shredder.Lib.ViewModels;
using Howest.NMCT.Shredder.Models;
using System.Windows.Media.Imaging;


namespace Howest.NMCT.Shredder.PhoneApp
{
    public partial class MyLocationPage : PhoneApplicationPage
    {

        private DetailMapPageViewModel _vm = new DetailMapPageViewModel();
        User user = new User();
        GeoCoordinate myTappedLocation = new GeoCoordinate();
        int count = 0;
        bool plaatsPin = false;

        public MyLocationPage()
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
                user.Email = userEmail;
            }

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
                img.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(OnPushTapped);
        
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
                count++;
            }
        }

        private void OnPushTapped(object sender, EventArgs e)
        {
              Image img = sender as Image;
              Place tappedPlace = (Place)img.Tag;

              foreach (Place p in _vm.Places)
              {
                  if (p.Latitude == tappedPlace.Latitude || p.Longitude == tappedPlace.Longitude)
                  {
                      //txtTappedPlace.Text = p.PlaceId.ToString();
                      ActiveUserAndSpot uas = new ActiveUserAndSpot();
                      uas.Place = p;
                      uas.User = _vm.ActiveUser;

                      //user.Email = "lynncallant@telenet.be";

                      NavigationService.Navigate(new Uri("/DetailSpotPage.xaml?user=" + user.Email + "&spotId=" + uas.Place.PlaceId, UriKind.Relative));            
                  }
              }
        }



        private async void ShowMyLocationOnTheMap()
        {
            Geolocator myGeolocator = new Geolocator();
            Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
            Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
            GeoCoordinate myGeoCoordinate = CoordinateConverter.ConvertGeocoordinate(myGeocoordinate);

            this.mapWithMyLocation.Center = myGeoCoordinate;
            //this.mapWithMyLocation.ZoomLevel = 13;

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
            count++;
            plaatsPin = true;
        }

        private void Grid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            bar.Visibility = Visibility.Visible;
            bigbar.Visibility = Visibility.Visible;
        }

        private void closeGrid(object sender, System.Windows.Input.GestureEventArgs e)
        {
            bar.Visibility = Visibility.Collapsed;
            bigbar.Visibility = Visibility.Collapsed;
        }

        private void mapWithMyLocation_Loaded(object sender, RoutedEventArgs e)
        {
            ShowMyLocationOnTheMap();
        }

        private void txtPlacesLoaded_TextChanged(object sender, TextChangedEventArgs e)
        {
            showlocations();
           
           
        }

     
        private void mapWithMyLocation_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {                    
            GeoCoordinate asd = this.mapWithMyLocation.ConvertViewportPointToGeoCoordinate(e.GetPosition(this.mapWithMyLocation));

            Grid MyGrid = new Grid();
            MyGrid.Children.Clear();
            MyGrid.RowDefinitions.Add(new RowDefinition());
            MyGrid.RowDefinitions.Add(new RowDefinition());
            MyGrid.Background = new SolidColorBrush(Colors.Transparent);

            BitmapImage bmi = new BitmapImage(new Uri("/Assets/cross.png", UriKind.Relative));
            Image img = new Image();
       
            img.Source = bmi;
           

            MyGrid.Children.Add(img);


            //Creating a MapOverlay and adding the Grid to it.
            MapOverlay MyOverlay = new MapOverlay();
            MyOverlay.Content = MyGrid;

            MyOverlay.GeoCoordinate = new GeoCoordinate(asd.Latitude, asd.Longitude);
            myTappedLocation = new GeoCoordinate(asd.Latitude, asd.Longitude);

            MyOverlay.PositionOrigin = new Point(0, 0.5);

            //Creating a MapLayer and adding the MapOverlay to it
            MapLayer MyLayer = new MapLayer();
            MyLayer.Add(MyOverlay);

            if (plaatsPin == true)
            {
                try
                {

                    mapWithMyLocation.Layers.RemoveAt(count);


                }
                catch { }
                mapWithMyLocation.Layers.Add(MyLayer);
            }

        }


        private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ShowMyLocationOnTheMap();
        }

        private void Button_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Top10SpotsPage.xaml?user=" + user.Email, UriKind.Relative));
        }

        private void Button_Tap_2(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Place newPlace = new Place();
            //newPlace.UserId = user.UserId;        NIET VERGETEN UIT COMMENTAAR TE ZETTEN
            newPlace.UserId = 4169;
            newPlace.Longitude = myTappedLocation.Longitude;
            newPlace.Latitude = myTappedLocation.Latitude;
            newPlace.Name = txtNewSpotName.Text;
            newPlace.Description = txtDescription.Text;
            newPlace.Rating = 0;
            _vm.saveSpot(newPlace);
            createSpot.Visibility = Visibility.Collapsed;
        }

        private void Button_Tap_3(object sender, System.Windows.Input.GestureEventArgs e)
        {
            createSpot.Visibility = Visibility.Collapsed;
        }

        private void newSpot_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            createSpot.Visibility = Visibility.Visible;
        }

       

    }
}