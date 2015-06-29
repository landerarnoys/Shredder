using Howest.NMCT.Shredder.API.DAL;
using Howest.NMCT.Shredder.API.DAL.UnitOfWork;
using Howest.NMCT.Shredder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Howest.NMCT.Shredder.API.Controllers
{
    public class PlaceController : ApiController
    {
        private UnitOfWork uow = new UnitOfWork(new ShredderContext());

        [HttpGet]
        public List<Place> GetPlaces()
        {
            return uow.PlaceRepository.GetPlaces();
        }

        [HttpPost]
        public Place AddPlace(Place place)
        {
            place = uow.PlaceRepository.AddPlace(place);
            uow.Save();
            return place;
        }

        [HttpGet]
        public Place GetPlaceById(int id)
        {
            return uow.PlaceRepository.GetPlaceById(id);
        }

        [HttpGet]
        public Place GetPlaceByLatitude(double lat)
        {
            return uow.PlaceRepository.GetPlaceByLatitude(lat);
        }

    }
}
