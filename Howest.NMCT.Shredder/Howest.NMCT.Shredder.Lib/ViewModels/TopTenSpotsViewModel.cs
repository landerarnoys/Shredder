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
    public class TopTenSpotsViewModel : ViewModelBase
    {
        private IShredderService shredderService = null;
        private List<Place> places = null;
        private List<PlaceComment> placecomments = null;
        private IShredderService service = new ShredderService();
        private Place _selectedPlace;
        private Boolean spotsLoading;
        private User activeUser;

        public RelayCommand getCommentsCommand
        {
            get;
            set;
        }

        private void getComments()
        {
            LoadPlaceComments(SelectedPlace.PlaceId);

        }

        public TopTenSpotsViewModel()
        {
            this.shredderService = service;
            this.LoadPlaces();
            this.spotsLoading = true;
            getCommentsCommand = new RelayCommand(getComments);
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

        public List<PlaceComment> PlaceComments
        {
            get { return placecomments; }
            set
            {
                placecomments = value;
                RaisePropertyChanged("PlaceComments");
            }
        }

        public async void LoadPlaces()
        {
            Places = await shredderService.GetPlaces();
            this.spotsLoading = false;
        }


        public async void LoadPlaceComments(int id)
        {

            PlaceComments = await shredderService.GetPlaceComments(id);
        }

        public Place SelectedPlace
        {
            get { return _selectedPlace; }
            set { _selectedPlace = value; RaisePropertyChanged("SelectedPlace"); }
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

        public Boolean SpotsLoading
        {
            get
            {
                return spotsLoading;
            }
            set
            {
                spotsLoading = value;
                RaisePropertyChanged("SpotsLoading");
            }
        }

    }
}
