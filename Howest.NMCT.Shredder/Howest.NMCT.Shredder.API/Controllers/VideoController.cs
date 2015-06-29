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
    public class VideoController : ApiController
    {
        private UnitOfWork uow = new UnitOfWork(new ShredderContext());

        [HttpGet]
        public List<Video> GetVideos()
        {
            return uow.VideoRepository.GetVideos();
        }

        [HttpPost]
        public Video AddVideo(Video video)
        {
            video = uow.VideoRepository.AddVideo(video);
            uow.Save();
            return video;
        }

        [HttpGet]
        public List<Video> GetVideosPerPlace(int placeId)
        {
            return uow.VideoRepository.GetVideosPerPlace(placeId);
        }

        [HttpGet]
        public Video GetVideoById(int id)
        {
            return uow.VideoRepository.GetVideoById(id);
        }
    }
}
