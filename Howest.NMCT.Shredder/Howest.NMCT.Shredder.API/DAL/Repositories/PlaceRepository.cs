using Howest.NMCT.Shredder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Howest.NMCT.Shredder.API.DAL.Repositories
{
    public class PlaceRepository
    {
         private ShredderContext context = null;

        public PlaceRepository(ShredderContext context)
        {
            this.context = context;
        }

        public Place AddPlace(Place place)
        {
            if (place.PlaceId == 0)
            {
                return this.context.Places.Add(place);
            }
            else
            {
                Place placeToUpdate = this.context.Places.Where(p => p.PlaceId == place.PlaceId).FirstOrDefault();
                if (placeToUpdate != null)
                {
                    this.context.Entry(placeToUpdate).CurrentValues.SetValues(place);
                }
                return null;
            }   
        }

        public List<Place> GetPlaces()
        {
            var query = (from p in this.context.Places orderby p.Rating descending select p);
            return query.ToList<Place>();
        }

        public Place GetPlaceById(int id)
        {
            var place = context.Places.Single(spot => spot.PlaceId == id);
            return place;
        }

        public Place GetPlaceByLatitude(double lat)
        {
            var query = (from p in this.context.Places where p.Latitude == lat select p);
            return query.SingleOrDefault();
        }
    }
}