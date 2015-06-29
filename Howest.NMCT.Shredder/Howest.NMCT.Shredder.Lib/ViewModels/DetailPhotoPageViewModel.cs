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
    public class DetailPhotoPageViewModel : ViewModelBase
    {
        private IShredderService shredderService = null;
        private IShredderService service = new ShredderService();
        private Picture _activePicture;
        private User _activeUser;
        private List<PictureComment> picturecomments = null;

        public DetailPhotoPageViewModel()
        {
            this.shredderService = service;
        }

        public Picture ActivePicture
        {
            get { return _activePicture; }
            set {
                _activePicture = value;
                RaisePropertyChanged("ActivePicture");
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

        public List<PictureComment> PictureComments
        {
            get { return picturecomments; }
            set
            {
                picturecomments = value;
                RaisePropertyChanged("PictureComments");
            }
        }

        public async void LoadPictureComments(int id)
        {
            PictureComments = await shredderService.GetPictureComments(id);
        }

        public async void GetUserByEmail(string email)
        {
            ActiveUser = await shredderService.GetUserByEmail(email);
        }

        public async void GetPictureById(int pictureId)
        {
            ActivePicture = await shredderService.GetPictureById(pictureId);
        }

        public void SavePictureComment(PictureComment picturecomment)
        {
            shredderService.AddPictureComment(picturecomment);
        }

        public void UpdatePictureRating(Picture picture)
        {
            shredderService.AddPicture(picture);
        }
    }
}
