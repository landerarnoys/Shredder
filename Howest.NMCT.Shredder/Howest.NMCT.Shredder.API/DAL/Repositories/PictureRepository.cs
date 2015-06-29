using Howest.NMCT.Shredder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Howest.NMCT.Shredder.API.DAL.Repositories
{
    public class PictureRepository
    {
         private ShredderContext context = null;

        public PictureRepository(ShredderContext context)
        {
            this.context = context;
        }

        public Picture AddPicture(Picture picture)
        {
            if (picture.PictureId == 0)
            {
                return this.context.Pictures.Add(picture);
            }
            else
            {
                Picture pictureToUpdate = this.context.Pictures.Where(p => p.PictureId == picture.PictureId).FirstOrDefault();
                if (pictureToUpdate != null)
                {
                    this.context.Entry(pictureToUpdate).CurrentValues.SetValues(picture);                    
                }
                return null;
            }   
        }

        public List<Picture> GetPictures()
        {
            var query = (from p in this.context.Pictures orderby p.Rating descending select p);
            return query.ToList<Picture>();
        }

        public Picture GetPictureById(int id)
        {
            //var picture = (from p in this.context.Pictures where p.PictureId == id select p);
            //return picture.SingleOrDefault();

            var picture = context.Pictures.Single(foto => foto.PictureId == id);
            return picture;
        }

        public List<Picture> GetPicturesPerPlace(int placeId)
        {
            var query = (from p in this.context.Pictures where p.PlaceId == placeId select p);
            return query.ToList<Picture>();
        }

    }
}