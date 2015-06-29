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
    public partial class Top10SpotsPage : PhoneApplicationPage
    {
        private TopTenSpotsViewModel _vm = new TopTenSpotsViewModel();
        string userEmail;
        public Top10SpotsPage()
        {
            InitializeComponent();
            DataContext = _vm;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (NavigationContext.QueryString.TryGetValue("user", out userEmail))
            {
                string blablaalb = userEmail;
            }


        }

        private void Grid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Place place = (Place)SpotList.SelectedItem;

            NavigationService.Navigate(new Uri("/DetailSpotPage.xaml?user=" + userEmail + "&spotId=" + place.PlaceId, UriKind.Relative));

        }
    }
}