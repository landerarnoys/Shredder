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
    public class MainPageViewModel : ViewModelBase
    {
        private IShredderService shredderService = null;
        private List<Picture> pictures = null;
        private List<Place> places = null;
        private List<Video> videos = null;
        private IShredderService service = new ShredderService();
        private Boolean photosLoading;
        private Boolean placesLoading;
        private Boolean videosLoading;
        private User activeUser;

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

        public Boolean VideosLoading
        {
            get
            {
                return videosLoading;
            }
            set
            {
                videosLoading = value;
                RaisePropertyChanged("VideosLoading");
            }
        }

        public Boolean PlacesLoading
        {
            get
            {
                return placesLoading;
            }
            set
            {
                placesLoading = value;
                RaisePropertyChanged("PlacesLoading");
            }
        }

        public MainPageViewModel()
        {
            this.shredderService = service;
            this.LoadPictures();
            this.PhotosLoading = true;
            this.LoadPlaces();
            this.PlacesLoading = true;
            this.LoadVideos();
            this.VideosLoading = true;

        }

        public void SaveUser(User user)
        {
            shredderService.AddUser(user);
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

        public List<Video> Videos
        {
            get { return videos; }
            set
            {
                videos = value;
                RaisePropertyChanged("Videos");
            }
        }

        public List<Place> Places
        {
            get { return places; }
            set
            {
                places = value;
                RaisePropertyChanged("Places");
            }
        }

        public async void LoadPictures()
        {
            Pictures = await shredderService.GetPictures();
            this.PhotosLoading = false;
        }

        public async void LoadPlaces()
        {
            Places = await shredderService.GetPlaces();
            this.PlacesLoading = false;
        }

        public async void LoadVideos()
        {
            Videos = await shredderService.GetVideos();
            this.VideosLoading = false;
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


    }
}
