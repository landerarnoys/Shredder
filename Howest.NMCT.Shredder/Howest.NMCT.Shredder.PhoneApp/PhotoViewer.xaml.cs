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

namespace Howest.NMCT.Shredder.PhoneApp
{
    public partial class PhotoViewer : PhoneApplicationPage
    {
        private DetailPhotoPageViewModel _vm = new DetailPhotoPageViewModel();

        public PhotoViewer()
        {
            InitializeComponent();
            DataContext = _vm;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string id;


            if (NavigationContext.QueryString.TryGetValue("pictureId", out id))
            {
                int pictureId = Convert.ToInt16(id);
                _vm.GetPictureById(pictureId);
            }





        }
    }
}