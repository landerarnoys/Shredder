using System;
namespace Howest.NMCT.Shredder.Lib.Service
{
    public interface IShredderService
    {
        void AddPicture(Howest.NMCT.Shredder.Models.Picture picture);
        void AddPictureComment(Howest.NMCT.Shredder.Models.PictureComment picturecomment);
        void AddPlace(Howest.NMCT.Shredder.Models.Place place);
        void AddPlaceComment(Howest.NMCT.Shredder.Models.PlaceComment placecomment);
        void AddUser(Howest.NMCT.Shredder.Models.User user);
        void AddVideo(Howest.NMCT.Shredder.Models.Video video);
        void AddVideoComment(Howest.NMCT.Shredder.Models.VideoComment videocomment);
        System.Threading.Tasks.Task<System.Collections.Generic.List<Howest.NMCT.Shredder.Models.PictureComment>> GetComments();
        System.Threading.Tasks.Task<System.Collections.Generic.List<Howest.NMCT.Shredder.Models.PictureComment>> GetPictureComments(int pictureId);
        System.Threading.Tasks.Task<System.Collections.Generic.List<Howest.NMCT.Shredder.Models.Picture>> GetPictures();
        System.Threading.Tasks.Task<System.Collections.Generic.List<Howest.NMCT.Shredder.Models.Picture>> GetPicturesPerPlace(int placeId);
        System.Threading.Tasks.Task<System.Collections.Generic.List<Howest.NMCT.Shredder.Models.PlaceComment>> GetPlaceComments(int placeId);
        System.Threading.Tasks.Task<System.Collections.Generic.List<Howest.NMCT.Shredder.Models.Place>> GetPlaces();
        System.Threading.Tasks.Task<Howest.NMCT.Shredder.Models.User> GetUserByEmail(string email);
        System.Threading.Tasks.Task<Howest.NMCT.Shredder.Models.User> GetUserById(int id);
        System.Threading.Tasks.Task<System.Collections.Generic.List<Howest.NMCT.Shredder.Models.User>> GetUsers();
        System.Threading.Tasks.Task<System.Collections.Generic.List<Howest.NMCT.Shredder.Models.VideoComment>> GetVideoComments(int videoId);
        System.Threading.Tasks.Task<System.Collections.Generic.List<Howest.NMCT.Shredder.Models.Video>> GetVideos();
        System.Threading.Tasks.Task<System.Collections.Generic.List<Howest.NMCT.Shredder.Models.Video>> GetVideosPerPlace(int placeId);

        
        System.Threading.Tasks.Task<Howest.NMCT.Shredder.Models.Picture> GetPictureById(int id);

        System.Threading.Tasks.Task<Howest.NMCT.Shredder.Models.Place> GetPlaceById(int id);
        System.Threading.Tasks.Task<Howest.NMCT.Shredder.Models.Place> GetPlaceByLatitude(double lat);

        System.Threading.Tasks.Task<Howest.NMCT.Shredder.Models.Video> GetVideoById(int id);
    }
}
