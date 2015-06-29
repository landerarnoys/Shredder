using Howest.NMCT.Shredder.API.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Howest.NMCT.Shredder.API.DAL.UnitOfWork
{
    public class UnitOfWork
    {
        private PictureCommentRepository pictureCommentRepository = null;
        private PictureRepository pictureRepository = null;
        private PlaceCommentRepository placeCommentRepository = null;
        private PlaceRepository placeRepository = null;
        private UserRepository userRepository = null;
        private VideoCommentRepository videoCommentRepository = null;
        private VideoRepository videoRepository = null;


        private ShredderContext context = null;

        public UnitOfWork(ShredderContext context)
        {
            this.context = context;
        }

        public PictureCommentRepository PictureCommentRepository
        {
            get
            {
                if (pictureCommentRepository == null)
                {
                    pictureCommentRepository = new PictureCommentRepository(this.context);
                }
                return pictureCommentRepository;
            }
        }

        public PictureRepository PictureRepository
        {
            get
            {
                if (pictureRepository == null)
                {
                    pictureRepository = new PictureRepository(this.context);
                }
                return pictureRepository;
            }
        }

        public PlaceCommentRepository PlaceCommentRepository
        {
            get
            {
                if (placeCommentRepository == null)
                {
                    placeCommentRepository = new PlaceCommentRepository(this.context);
                }
                return placeCommentRepository;
            }
        }

        public PlaceRepository PlaceRepository
        {
            get
            {
                if (placeRepository == null)
                {
                    placeRepository = new PlaceRepository(this.context);
                }
                return placeRepository;
            }
        }

        public UserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(this.context);
                }
                return userRepository;
            }
        }

        public VideoCommentRepository VideoCommentRepository
        {
            get
            {
                if (videoCommentRepository == null)
                {
                    videoCommentRepository = new VideoCommentRepository(this.context);
                }
                return videoCommentRepository;
            }
        }

        public VideoRepository VideoRepository
        {
            get
            {
                if (videoRepository == null)
                {
                    videoRepository = new VideoRepository(this.context);
                }
                return videoRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}