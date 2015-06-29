using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Howest.NMCT.Shredder.Lib.Service;
using Howest.NMCT.Shredder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.NMCT.Shredder.Lib.ViewModels
{
    public class TopTenVideosViewModel : ViewModelBase
    {
        private IShredderService shredderService = null;
        private List<Video> videos = null;
        private User activeUser;
        private IShredderService service = new ShredderService();
        private Boolean videosLoading;


        public TopTenVideosViewModel()
        {
            this.shredderService = service;
            this.LoadVideos();
            this.videosLoading = true;
        }

        public List<Video> Videos
        {
            get { return videos; }
            set
            {
                videos = value;
                RaisePropertyChanged("Videos");
            }
        }

        public User ActiveUser
        {
            get { return activeUser; }
            set
            {
                activeUser = value;
                RaisePropertyChanged("ActiveUser");
            }
        }

        public async void LoadVideos()
        {
            Videos = await shredderService.GetVideos();
            this.VideosLoading = false;
        }

        public Boolean VideosLoading
        {
            get
            {
                return videosLoading;
            }
            set
            {
                videosLoading = value;
                RaisePropertyChanged("VideosLoading");
            }
        }

    }
}
