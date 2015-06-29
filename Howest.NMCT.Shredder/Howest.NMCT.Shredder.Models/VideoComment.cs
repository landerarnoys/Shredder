using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Howest.NMCT.Shredder.Models
{
    [KnownType(typeof(VideoComment))]
    public class VideoComment : ObservableObject
    {
        public int VideoCommentId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }
        public int VideoId { get; set; }
        public virtual Video Video { get; set; }
        public string Comment { get; set; }
    }
}
