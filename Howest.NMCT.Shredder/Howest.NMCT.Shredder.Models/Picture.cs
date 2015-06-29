using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Howest.NMCT.Shredder.Models
{
    [KnownType(typeof(Picture))]
    public class Picture : ObservableObject
    {
        public int PictureId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }

    }
}
