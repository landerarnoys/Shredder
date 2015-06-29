using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Howest.NMCT.Shredder.Lib.Service;
using Howest.NMCT.Shredder.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.NMCT.Shredder.Lib.ViewModels
{
    public class TopTenPicturesViewModel : ViewModelBase
    {
        private IShredderService shredderService = null;
        private List<Picture> pictures = null;
        private IShredderService service = new ShredderService();
        private Boolean photosLoading;
        private User activeUser;

        public TopTenPicturesViewModel()
        {
            this.shredderService = service;
            this.LoadPictures();
            this.PhotosLoading = true;
        }

        public List<Picture> Pictures
        {
            get { return pictures; }
            set
            {
                pictures = value;
                RaisePropertyChanged("Pictures");
            }
        }

        public async void LoadPictures()
        {
            Pictures = await shredderService.GetPictures();
            this.PhotosLoading = false;
        }

        public User ActiveUser
        {
            get { return activeUser; }
            set
            {
                activeUser = value;
                RaisePropertyChanged("ActiveUser");
            }
        }

        public Boolean PhotosLoading
        {
            get
            {
                return photosLoading;
            }
            set
            {
                photosLoading = value;
                RaisePropertyChanged("PhotosLoading");
            }
        }

    }
}
