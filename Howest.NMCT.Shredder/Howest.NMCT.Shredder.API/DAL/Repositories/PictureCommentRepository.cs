using Howest.NMCT.Shredder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Howest.NMCT.Shredder.API.DAL.Repositories
{
    public class PictureCommentRepository
    {
                 private ShredderContext context = null;

        public PictureCommentRepository(ShredderContext context)
        {
            this.context = context;
        }

        public PictureComment AddPictureComment(PictureComment picturecomment)
        {
            return this.context.PictureComments.Add(picturecomment);
        }

        public List<PictureComment> GetComments()
        {
            return this.context.PictureComments.ToList<PictureComment>();
        }



        public List<PictureComment> GetPictureComments(int pictureId)
        {
            var picturecomments = (from pc in this.context.PictureComments where pc.Picture.PictureId == pictureId select pc);
            return picturecomments.ToList<PictureComment>();
        }
    }
}