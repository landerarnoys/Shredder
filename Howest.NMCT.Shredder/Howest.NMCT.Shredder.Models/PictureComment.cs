using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Howest.NMCT.Shredder.Models
{
    [KnownType(typeof(PictureComment))]
    public class PictureComment : ObservableObject
    {
        public int PictureCommentId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }
        public int PictureId { get; set; }
        public virtual Picture Picture { get; set; }
        public string Comment { get; set; }
    }
}