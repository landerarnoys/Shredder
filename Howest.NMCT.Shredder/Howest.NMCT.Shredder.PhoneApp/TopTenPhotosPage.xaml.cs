using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Howest.NMCT.Shredder.Models;
using Howest.NMCT.Shredder.Lib.ViewModels;

namespace Howest.NMCT.Shredder.PhoneApp
{
    public partial class TopTenPhotosPage : PhoneApplicationPage
    {
        private TopTenPicturesViewModel _vm = new TopTenPicturesViewModel();
        string userEmail;
        public TopTenPhotosPage()
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
            ActiveUserAndPicture uap = new ActiveUserAndPicture();
            Picture picture = (Picture)PhotoList.SelectedItem;

            NavigationService.Navigate(new Uri("/DetailPicturePage.xaml?user=" + userEmail + "&pictureId=" + picture.PictureId, UriKind.Relative));
        }
    }
}