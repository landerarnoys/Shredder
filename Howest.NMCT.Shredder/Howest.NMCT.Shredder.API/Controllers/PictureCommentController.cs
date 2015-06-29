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
    public class PictureCommentController : ApiController
    {
        private UnitOfWork uow = new UnitOfWork(new ShredderContext());

        [HttpGet]
        public List<PictureComment> GetPictureComments(int pictureId)
        {
            return uow.PictureCommentRepository.GetPictureComments(pictureId);
            
        }

        public List<PictureComment> GetComments()
        {
            return uow.PictureCommentRepository.GetComments();

        }

        [HttpPost]
        public PictureComment AddPictureComment(PictureComment picturecomment)
        {
            picturecomment = uow.PictureCommentRepository.AddPictureComment(picturecomment);
            uow.Save();
            return picturecomment;
        }
    }
}
