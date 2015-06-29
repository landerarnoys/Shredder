using GalaSoft.MvvmLight;
using Howest.NMCT.Shredder.Lib.Service;
using Howest.NMCT.Shredder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.NMCT.Shredder.Lib.ViewModels
{
    public class DetailVideoPageViewModel : ViewModelBase
    {
         private IShredderService shredderService = null;
        private IShredderService service = new ShredderService();
        private Video _activeVideo;
        private User _activeUser;
        private List<VideoComment> videocomments = null;

        public DetailVideoPageViewModel()
        {
            this.shredderService = service;
        }

        public Video ActiveVideo
        {
            get { return _activeVideo; }
            set {
                _activeVideo = value;
                RaisePropertyChanged("ActiveVideo");
            }
        }

        public User ActiveUser
        {
            get { return _activeUser; }
            set
            {
                _activeUser = value;
                RaisePropertyChanged("ActiveUser");
            }
        }

        public List<VideoComment> VideoComments
        {
            get { return videocomments; }
            set
            {
                videocomments = value;
                RaisePropertyChanged("VideoComments");
            }
        }

        public async void LoadVideoComments(int id)
        {
            VideoComments = await shredderService.GetVideoComments(id);
        }

        public async void GetUserByEmail(string email)
        {
            ActiveUser = await shredderService.GetUserByEmail(email);
        }

        public async void GetVideoById(int videoId)
        {
            ActiveVideo = await shredderService.GetVideoById(videoId);
        }

        public void SaveVideoComment(VideoComment videocomment)
        {
            shredderService.AddVideoComment(videocomment);
        }

        public void UpdateVideoRating(Video video)
        {
            shredderService.AddVideo(video);
        }
    }
}
