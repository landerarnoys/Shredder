using Howest.NMCT.Shredder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Howest.NMCT.Shredder.API.DAL.Repositories
{
    public class PlaceCommentRepository
    {
         private ShredderContext context = null;

        public PlaceCommentRepository(ShredderContext context)
        {
            this.context = context;
        }

        public PlaceComment AddPlaceComment(PlaceComment placecomment)
        {
            return this.context.PlaceComments.Add(placecomment);
        }

        public List<PlaceComment> GetPlaceComments(int id)
        {
            var placecomment = (from pc in this.context.PlaceComments where pc.Place.PlaceId == id select pc);
            return placecomment.ToList<PlaceComment>();
        }
    }
}