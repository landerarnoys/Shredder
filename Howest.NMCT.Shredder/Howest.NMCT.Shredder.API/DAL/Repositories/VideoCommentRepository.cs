using Howest.NMCT.Shredder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Howest.NMCT.Shredder.API.DAL.Repositories
{
    public class VideoCommentRepository
    {
                 private ShredderContext context = null;

        public VideoCommentRepository(ShredderContext context)
        {
            this.context = context;
        }

        public VideoComment AddVideoComment(VideoComment videocomment)
        {
            return this.context.VideoComments.Add(videocomment);
        }

        public List<VideoComment> GetVideoComments(int id)
        {
            var videocomments = (from vc in this.context.VideoComments where vc.Video.VideoId == id select vc);
            return videocomments.ToList<VideoComment>();
        }
    }
}