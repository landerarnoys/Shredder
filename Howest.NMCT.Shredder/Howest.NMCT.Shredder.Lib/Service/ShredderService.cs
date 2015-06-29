using Howest.NMCT.Shredder.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Howest.NMCT.Shredder.Lib.Service
{
    public class ShredderService : Howest.NMCT.Shredder.Lib.Service.IShredderService
    {
        private const string URL = @"http://shredder.azurewebsites.net/api/";



        //ADDUSER & LIST OF USERS
        public async void AddUser(User user)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = string.Format("{0}{1}", URL, "user/add");
                    string json = JsonConvert.SerializeObject(user);
                    HttpContent content = new StringContent(json);
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/json");
                    await client.PostAsync(url, content);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<User> GetUserById(int id)
        {
            User user = new User();

            using (HttpClient client = new HttpClient())
            {
                string url = string.Format("{0}{1}", URL, "/user?id=" + id);
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        user = await JsonConvert.DeserializeObjectAsync<User>(content);
                    }
                }
            }
            return user;
        }

        public async Task<User> GetUserByEmail(String email)
        {
            User user = new User();
            using (HttpClient client = new HttpClient())
            {
                string url = string.Format("{0}{1}", URL, "/user?email=" + email);
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        user = await JsonConvert.DeserializeObjectAsync<User>(content);
                    }
                }
            }
            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            List<User> users = new List<User>();
            using (HttpClient client = new HttpClient())
            {
                string url = string.Format("{0}{1}", URL, "/user");
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        users = await JsonConvert.DeserializeObjectAsync<List<User>>(content);
                    }
                }
            }
            return users;
        }

        //ADDPICTURE & LIST OF PICTURES
        public async void AddPicture(Picture picture)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = string.Format("{0}{1}", URL, "picture");
                    string json = JsonConvert.SerializeObject(picture);
                    HttpContent content = new StringContent(json);
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/json");
                    await client.PostAsync(url, content);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<Picture>> GetPictures()
        {
            List<Picture> pictures = new List<Picture>();
            using (HttpClient client = new HttpClient())
            {
                string url = string.Format("{0}{1}", URL, "/picture");
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        pictures = await JsonConvert.DeserializeObjectAsync<List<Picture>>(content);
                    }
                }
            }
            return pictures;
        }

        public async Task<Picture> GetPictureById(int id)
        {
            Picture picture = new Picture();
            using (HttpClient client = new HttpClient())
            {
                string url = string.Format("{0}{1}", URL, "/picture?id=" + id);
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        picture = await JsonConvert.DeserializeObjectAsync<Picture>(content);
                    }
                }
            }
            return picture;
        }

        //ADDPICTURECOMMENT & LIST OF PICTURECOMMENTS
        public async void AddPictureComment(PictureComment picturecomment)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = string.Format("{0}{1}", URL, "picturecomment");
                    string json = JsonConvert.SerializeObject(picturecomment);
                    HttpContent content = new StringContent(json);
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/json");
                    await client.PostAsync(url, content);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<PictureComment>> GetComments()
        {
            List<PictureComment> comments = new List<PictureComment>();
            using (HttpClient client = new HttpClient())
            {
                string url = string.Format("{0}{1}", URL, "/picturecomment");
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        comments = await JsonConvert.DeserializeObjectAsync<List<PictureComment>>(content);
                    }
                }
            }
            return comments;
        }





        public async Task<List<PictureComment>> GetPictureComments(int pictureId)
        {
            List<PictureComment> picturecomments = new List<PictureComment>();
            using (HttpClient client = new HttpClient())
            {
                string url = string.Format("{0}{1}", URL, "/picturecomment?pictureId=" + pictureId);
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        picturecomments = await JsonConvert.DeserializeObjectAsync<List<PictureComment>>(content);
                    }
                }
            }
            return picturecomments;
        }

        public async Task<List<Picture>> GetPicturesPerPlace(int placeId)
        {
            List<Picture> pictures = new List<Picture>();
            using (HttpClient client = new HttpClient())
            {
                string url = string.Format("{0}{1}", URL, "/picture?placeId=" + placeId);
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        pictures = await JsonConvert.DeserializeObjectAsync<List<Picture>>(content);
                    }
                }
            }
            return pictures;
        }

        //ADDVIDEO & LIST OF VIDEOS
        public async void AddVideo(Video video)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = string.Format("{0}{1}", URL, "video");
                    string json = JsonConvert.SerializeObject(video);
                    HttpContent content = new StringContent(json);
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/json");
                    await client.PostAsync(url, content);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<Video>> GetVideos()
        {
            List<Video> videos = new List<Video>();
            using (HttpClient client = new HttpClient())
            {
                string url = string.Format("{0}{1}", URL, "/video");
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        videos = await JsonConvert.DeserializeObjectAsync<List<Video>>(content);
                    }
                }
            }
            return videos;
        }

        public async Task<Video> GetVideoById(int id)
        {
            Video video = new Video();
            using (HttpClient client = new HttpClient())
            {
                string url = string.Format("{0}{1}", URL, "/video?id=" + id);
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        video = await JsonConvert.DeserializeObjectAsync<Video>(content);
                    }
                }
            }
            return video;
        }

        public async Task<List<Video>> GetVideosPerPlace(int placeId)
        {
            List<Video> videos = new List<Video>();
            using (HttpClient client = new HttpClient())
            {
                string url = string.Format("{0}{1}", URL, "/video?placeId=" + placeId);
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        videos = await JsonConvert.DeserializeObjectAsync<List<Video>>(content);
                    }
                }
            }
            return videos;
        }

        //ADDVIDEOCOMMENT & LIST OF VIDEOCOMMENTS
        public async void AddVideoComment(VideoComment videocomment)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = string.Format("{0}{1}", URL, "videocomment");
                    string json = JsonConvert.SerializeObject(videocomment);
                    HttpContent content = new StringContent(json);
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/json");
                    await client.PostAsync(url, content);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<VideoComment>> GetVideoComments(int videoId)
        {
            List<VideoComment> videocomments = new List<VideoComment>();
            using (HttpClient client = new HttpClient())
            {
                string url = string.Format("{0}{1}", URL, "/videocomment?videoId=" + videoId);
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        videocomments = await JsonConvert.DeserializeObjectAsync<List<VideoComment>>(content);
                    }
                }
            }
            return videocomments;
        }

        //ADDPLACE & LIST OF PLACES
        public async void AddPlace(Place place)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = string.Format("{0}{1}", URL, "place");
                    string json = JsonConvert.SerializeObject(place);
                    HttpContent content = new StringContent(json);
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/json");
                    await client.PostAsync(url, content);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<Place>> GetPlaces()
        {
            List<Place> places = new List<Place>();
            using (HttpClient client = new HttpClient())
            {
                string url = string.Format("{0}{1}", URL, "/place");
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        places = await JsonConvert.DeserializeObjectAsync<List<Place>>(content);
                    }
                }
            }
            return places;
        }

        public async Task<Place> GetPlaceById(int id)
        {
            Place place = new Place();
            using (HttpClient client = new HttpClient())
            {
                string url = string.Format("{0}{1}", URL, "/place?id=" + id);
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        place = await JsonConvert.DeserializeObjectAsync<Place>(content);
                    }
                }
            }
            return place;
        }

        public async Task<Place> GetPlaceByLatitude(double lat)
        {
            Place place = new Place();
            using (HttpClient client = new HttpClient())
            {
                string url = string.Format("{0}{1}", URL, "/place?latitude=" + lat);
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        place = await JsonConvert.DeserializeObjectAsync<Place>(content);
                    }
                }
            }
            return place;
        }

        //ADDPLACECOMMENT & LIST OF PLACECOMMENTS
        public async void AddPlaceComment(PlaceComment placecomment)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = string.Format("{0}{1}", URL, "placecomment");
                    string json = JsonConvert.SerializeObject(placecomment);
                    HttpContent content = new StringContent(json);
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/json");
                    await client.PostAsync(url, content);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<PlaceComment>> GetPlaceComments(int placeId)
        {
            List<PlaceComment> placecomments = new List<PlaceComment>();
            using (HttpClient client = new HttpClient())
            {
                string url = string.Format("{0}{1}", URL, "/placecomment?placeId=" + placeId);
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        placecomments = await JsonConvert.DeserializeObjectAsync<List<PlaceComment>>(content);
                    }
                }
            }
            return placecomments;
        }


    }
}
