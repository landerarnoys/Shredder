using GalaSoft.MvvmLight;
using Howest.NMCT.Shredder.Lib.Service;
using Howest.NMCT.Shredder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.NMCT.Shredder.Lib.ViewModels
{
    public class DetailSpotPageViewModel : ViewModelBase
    {
        private IShredderService shredderService = null;
        private IShredderService service = new ShredderService();
        private User activeUser;
        private Place activePlace;
        private List<PlaceComment> placecomments = null;
        private List<Picture> pictures = null;
        private List<Video> videos = null;

        public DetailSpotPageViewModel()
        {
            this.shredderService = service;
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

        public Place ActivePlace
        {
            get { return activePlace; }
            set
            {
                activePlace = value;
                RaisePropertyChanged("ActivePlace");
            }
        }

        public void UpdateSpotRating(Place Place)
        {
            shredderService.AddPlace(Place);
        }

        public List<PlaceComment> PlaceComments
        {
            get { return placecomments; }
            set
            {
                placecomments = value;
                RaisePropertyChanged("PlaceComments");
            }
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

        public async void LoadPlaceComments(int id)
        {
            PlaceComments = await shredderService.GetPlaceComments(id);
        }


        public async void GetUserByEmail(string p)
        {
            ActiveUser = await shredderService.GetUserByEmail(p);
        }

        public async void GetSpotById(int spotId)
        {
            ActivePlace = await shredderService.GetPlaceById(spotId);
        }

        public async void LoadPictures(int placeId)
        {
            Pictures = await shredderService.GetPicturesPerPlace(placeId);
        }

        public void SavePlaceComment(PlaceComment placecomment)
        {
            shredderService.AddPlaceComment(placecomment);
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

        public async void LoadVideos(int placeId)
        {
            Videos = await shredderService.GetVideosPerPlace(placeId);
        }

        public void SavePicture(Picture p)
        {
            shredderService.AddPicture(p);
        }

        public void SaveVideo(Video v)
        {
            shredderService.AddVideo(v);
        }
    }
}
