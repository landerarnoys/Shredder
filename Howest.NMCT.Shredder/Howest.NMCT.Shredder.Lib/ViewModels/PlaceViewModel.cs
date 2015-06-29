using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Howest.NMCT.Shredder.Lib.Service;
using Howest.NMCT.Shredder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.NMCT.Shredder.Lib.ViewModels
{
    public class PlaceViewModel: ViewModelBase
    {
        private IShredderService shredderService = null;
        private List<Place> _places = null;
        private Place newPlace = null;

        public RelayCommand SavePlaceCommand { get; set; }
        public RelayCommand LoadPlacesCommand { get; set; }

        public PlaceViewModel(IShredderService service)
        {
            this.shredderService = service;
            LoadPlacesCommand = new RelayCommand(LoadPlaces);
            SavePlaceCommand = new RelayCommand(SavePlace);
            NewPlace = new Place();
        }

        public List<Place> Places
        {
            get { return _places; }
            set
            {
                _places = value;
                RaisePropertyChanged("Places");
            }
        }

        private async void LoadPlaces()
        {
            Places = await shredderService.GetPlaces();
        }

        public Place NewPlace
        {
            get
            {
                return newPlace;
            }
            set
            {
                Set<Place>(ref newPlace, value);
            }
        }

        private void SavePlace()
        {
            shredderService.AddPlace(NewPlace);
        }


    }
}
