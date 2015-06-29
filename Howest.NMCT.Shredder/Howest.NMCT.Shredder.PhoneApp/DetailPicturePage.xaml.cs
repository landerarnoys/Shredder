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
    public partial class DetailPicturePage : PhoneApplicationPage
    {
        private DetailPhotoPageViewModel _vm = new DetailPhotoPageViewModel();

        public DetailPicturePage()
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

            if (NavigationContext.QueryString.TryGetValue("pictureId", out id))
            {
                int pictureId = Convert.ToInt16(id);
                _vm.GetPictureById(pictureId);
                _vm.LoadPictureComments(pictureId);
            }





        }

        private void btnPlaceComment_Click(object sender, RoutedEventArgs e)
        {
            if (!txtComment.Text.Equals(""))
            {
                PictureComment pc = new PictureComment();
                pc.Comment = txtComment.Text;
                pc.PictureId = _vm.ActivePicture.PictureId;
                pc.PlaceId = _vm.ActivePicture.Place.PlaceId;
                pc.UserId = _vm.ActiveUser.UserId;
                _vm.SavePictureComment(pc);
                txtComment.Text = "";
            }
        }

        private void imagePhoto_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string id = txtPictureId.Text;
            NavigationService.Navigate(new Uri("/PhotoViewer.xaml?pictureId=" + id, UriKind.Relative));

        }
    }
}