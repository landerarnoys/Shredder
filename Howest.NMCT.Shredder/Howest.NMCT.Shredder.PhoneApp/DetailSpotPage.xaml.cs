using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Howest.NMCT.Shredder.Lib.ViewModels;
using Howest.NMCT.Shredder.Models;
using Windows.Storage.Pickers;
using Windows.Storage;
using Microsoft.Phone.Tasks;
using System.IO;
//using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.Storage;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location;
using System.Windows.Media;
using System.Text.RegularExpressions;

namespace Howest.NMCT.Shredder.PhoneApp
{

    public partial class DetailSpotPage : PhoneApplicationPage
    {
        private DetailSpotPageViewModel _vm = new DetailSpotPageViewModel();
        User user = new User();
        PhotoChooserTask photoChooserTask;
      //  private CloudStorageAccount _storageAccount = null;
        StorageFile file = null;
        private static readonly Regex YoutubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:(.*)v(/|=)|(.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase);
     

        public DetailSpotPage()
        {
            InitializeComponent();
            DataContext = _vm;
          //  _storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=shredder;AccountKey=TI86ZnrNivB7ITMKmIHzYDu7kr/am7PYT6DWiZKfSlIDw2R6TDsSYxTBUDRFjXhyXoMq6DD7hGIvQM566f1cIw==");
            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);


            string userEmail;
            string id;


            if (NavigationContext.QueryString.TryGetValue("user", out userEmail))
            {
                _vm.GetUserByEmail(userEmail);
                user.Email = userEmail;
            }

            if (NavigationContext.QueryString.TryGetValue("spotId", out id))
            {
                int spotId = Convert.ToInt16(id);
                _vm.GetSpotById(spotId);
                _vm.LoadPlaceComments(spotId);
                _vm.LoadPictures(spotId);
                _vm.LoadVideos(spotId);
            }

        }

        private void btnPlaceComment_Click(object sender, RoutedEventArgs e)
        {
            if (!txtComment.Text.Equals(""))
            {
                PlaceComment pc = new PlaceComment();
                pc.Comment = txtComment.Text;
                pc.PlaceId = _vm.ActivePlace.PlaceId;
                pc.UserId = _vm.ActiveUser.UserId;
                _vm.SavePlaceComment(pc);
                txtComment.Text = "";
            }
        }

        private void Grid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Picture picture = (Picture)photoList.SelectedItem;
            NavigationService.Navigate(new Uri("/DetailPicturePage.xaml?user=" + user.Email + "&pictureId=" + picture.PictureId, UriKind.Relative));            
        }

        private void test(object sender, System.Windows.Input.GestureEventArgs e)
        {
            photoChooserTask.Show();
            createPhoto.Visibility = Visibility.Visible;
        }



      
        private async void Button_Tap_2(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //adden
            if (!txtFotoName.Text.Equals("") && !txtDescription.Text.Equals(""))
            {
               // var randomAccessStream = await file.OpenReadAsync();
               // Stream stream = randomAccessStream.AsStreamForRead();
               // Windows.Storage.Streams.IInputStream inStream = stream.AsInputStream();
               // //Create BLOBClient
               //// CloudBlobClient blobClient = _storageAccount.CreateCloudBlobClient();
               // //Retrieve a reference to a container that we want to use
               //// CloudBlobContainer container = blobClient.GetContainerReference("pictures");
               // //CloudBlockBlob blockBlob = container.GetBlockBlobReference(file.Name);
               // //await blockBlob.UploadFromStreamAsync(inStream);
               // Picture p = new Picture();
               // p.Title = txtFotoName.Text;
               // p.Description = txtDescription.Text;
               // p.UserId = _vm.ActiveUser.UserId;
               // p.PlaceId = _vm.ActivePlace.PlaceId;
               // p.Url = "http://shredder.blob.core.windows.net/pictures/" + file.Name;
               // p.Rating = 0;
               // _vm.SavePicture(p);
               // createPhoto.Visibility = Visibility.Collapsed;
            }  
        }


       
        private async void photoChooserTask_Completed(object sender, PhotoResult e)
        {
             // no photo selected
             if (e.ChosenPhoto == null) return;
             
            // get the file stream and file name
            Stream photoStream = e.ChosenPhoto;
            string fileName = Path.GetFileName(e.OriginalFileName);

              // persist data into isolated storage
            file = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
           
            using (Stream current = await file.OpenStreamForWriteAsync())
                {
                  await photoStream.CopyToAsync(current);
                
                }

            // how to read the data later
            file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
            Stream imageStream = await file.OpenStreamForReadAsync();

            
            // display the file as image
            BitmapImage bi = new BitmapImage();
            bi.SetSource(imageStream);
   
        }

        private void Button_Tap_3(object sender, System.Windows.Input.GestureEventArgs e)
        {
            createPhoto.Visibility = Visibility.Collapsed;
        }

        private void createPhoto_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            createPhoto.Visibility = Visibility.Visible;
        }

        private void txtlat_TextChanged(object sender, TextChangedEventArgs e)
        {
            double latitude = Convert.ToDouble(txtlat.Text);
            double longitude = Convert.ToDouble(txtLon.Text);

            Grid MyGrid = new Grid();
            MyGrid.RowDefinitions.Add(new RowDefinition());
            MyGrid.RowDefinitions.Add(new RowDefinition());
            MyGrid.Background = new SolidColorBrush(Colors.Transparent);

            BitmapImage bmi = new BitmapImage(new Uri("/Assets/pushpinPhone.png", UriKind.Relative));
            Image img = new Image();
            img.Source = bmi;


            MyGrid.Children.Add(img);


            //Creating a MapOverlay and adding the Grid to it.
            MapOverlay MyOverlay = new MapOverlay();
            MyOverlay.Content = MyGrid;

            MyOverlay.GeoCoordinate = new GeoCoordinate(latitude, longitude);


            MyOverlay.PositionOrigin = new Point(0, 0.5);

            //Creating a MapLayer and adding the MapOverlay to it
            MapLayer MyLayer = new MapLayer();
            MyLayer.Add(MyOverlay);
            mapWithMyLocation.Layers.Add(MyLayer);

            mapWithMyLocation.ZoomLevel = 15;


            this.mapWithMyLocation.Center = MyOverlay.GeoCoordinate;
           
        }

        private Boolean IsYoutubeLink(String url)
        {
            Match youtubeMatch = YoutubeVideoRegex.Match(url);
            if (youtubeMatch.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void AddVideo(object sender, System.Windows.Input.GestureEventArgs e)
        {
            String link = txtYoutube.Text;
            if (!txtVideoName.Text.Equals("") && !txtVideoDescription.Text.Equals("") && !txtYoutube.Text.Equals("") && IsYoutubeLink(link))
            {
                int index = link.IndexOf('=');
                string yt = link.Substring(index + 1);
                Video v = new Video();
                v.Title = txtVideoName.Text;
                v.Description = txtVideoDescription.Text;
                v.Url = yt;
                v.UserId = _vm.ActiveUser.UserId;
                v.PlaceId = _vm.ActivePlace.PlaceId;
                v.Rating = 0;
                v.Thumbnail = "http://img.youtube.com/vi/" + yt + "/0.jpg";
                _vm.SaveVideo(v);
                createVideo.Visibility = Visibility.Collapsed;
            }
        }

        private void Cancel(object sender, System.Windows.Input.GestureEventArgs e)
        {
            createVideo.Visibility = Visibility.Collapsed;
        }

        private void OpenAddVideo(object sender, System.Windows.Input.GestureEventArgs e)
        {
            createVideo.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }



        
    }
}