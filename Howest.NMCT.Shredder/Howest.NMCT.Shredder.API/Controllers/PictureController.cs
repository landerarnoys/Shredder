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
    public class PictureController : ApiController
    {
        private UnitOfWork uow = new UnitOfWork(new ShredderContext());

        [HttpGet]
        public List<Picture> GetPictures()
        {
            return uow.PictureRepository.GetPictures();
        }

        [HttpGet]
        public List<Picture> GetPicturesPerPlace(int placeId)
        {
            return uow.PictureRepository.GetPicturesPerPlace(placeId);
        }

        [HttpGet]
        public Picture GetPictureById(int id)
        {
            return uow.PictureRepository.GetPictureById(id);
        }

        [HttpPost]
        public Picture AddPicture(Picture picture)
        {
            picture = uow.PictureRepository.AddPicture(picture);
            uow.Save();
            return picture;
        }

    }
}
