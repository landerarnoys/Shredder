using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Howest.NMCT.Shredder.Lib.Service;
using Howest.NMCT.Shredder.Models;

namespace Howest.NMCT.Shredder.Lib.ViewModels
{
    public class DetailMapPageViewModel : ViewModelBase
    {
        private IShredderService shredderService = null;
        private IShredderService service = new ShredderService();
        private List<Place> places = null;
        private User activeUser;
        private Place tappedPlace;

        public DetailMapPageViewModel()
        {
            this.shredderService = service;
            this.LoadPlaces();         
        }
    
        public void saveSpot(Place place)
        {
           shredderService.AddPlace(place);
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

        public async void LoadPlaces()
        {
            Places = await shredderService.GetPlaces();
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

        public async void GetUserByEmail(string email)
        {
            ActiveUser = await shredderService.GetUserByEmail(email);
        }

        public async void GetSpotByLatitude(double lat)
        {
            TappedPlace = await shredderService.GetPlaceByLatitude(lat);
        }


        public Place TappedPlace
        {
            get { return tappedPlace; }
            set
            {
                tappedPlace = value;
                RaisePropertyChanged("TappedPlace");
            }
        }


    }
}
