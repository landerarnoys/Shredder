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
    public class VideoCommentController : ApiController
    {
        private UnitOfWork uow = new UnitOfWork(new ShredderContext());

        [HttpGet]
        public List<VideoComment> GetVideoComments(int videoId)
        {
            return uow.VideoCommentRepository.GetVideoComments(videoId);

        }

        [HttpPost]
        public VideoComment AddVieoComment(VideoComment videocomment)
        {
            videocomment = uow.VideoCommentRepository.AddVideoComment(videocomment);
            uow.Save();
            return videocomment;
        }
    }
}
