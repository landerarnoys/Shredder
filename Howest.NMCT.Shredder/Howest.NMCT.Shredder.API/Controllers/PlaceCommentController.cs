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
    public class PlaceCommentController : ApiController
    {
        private UnitOfWork uow = new UnitOfWork(new ShredderContext());

        [HttpGet]


        [HttpPost]
        public PlaceComment AddPlaceComment(PlaceComment placecomment)
        {
            placecomment = uow.PlaceCommentRepository.AddPlaceComment(placecomment);
            uow.Save();
            return placecomment;
        }


        [HttpGet]
        public List<PlaceComment> GetPlaceComments(int placeId)
        {
            return uow.PlaceCommentRepository.GetPlaceComments(placeId);
        }
    }
}
