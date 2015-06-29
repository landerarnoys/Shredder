using Howest.NMCT.Shredder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Howest.NMCT.Shredder.API.DAL.Repositories
{
    public class VideoRepository
    {
         private ShredderContext context = null;

        public VideoRepository(ShredderContext context)
        {
            this.context = context;
        }

        public Video AddVideo(Video video)
        {
            if (video.VideoId == 0)
            {
                return this.context.Videos.Add(video);
            }
            else
            {
                Video videoToUpdate = this.context.Videos.Where(v => v.VideoId == video.VideoId).FirstOrDefault();
                if (videoToUpdate != null)
                {
                    this.context.Entry(videoToUpdate).CurrentValues.SetValues(video);
                }
                return null;
            }
            
        }

        public List<Video> GetVideos()
        {
            var query = (from v in this.context.Videos orderby v.Rating descending select v);
            return query.ToList<Video>();
        }

        public Video GetVideoById(int id)
        {
            var video = (from v in this.context.Videos where v.VideoId == id select v);
            return video.SingleOrDefault();
        }

        public List<Video> GetVideosPerPlace(int placeId)
        {
            var query = (from v in this.context.Videos where v.PlaceId == placeId select v);
            return query.ToList<Video>();
        }
    }
}