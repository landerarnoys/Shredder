using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Howest.NMCT.Shredder.Models
{
    [KnownType(typeof(Place))]
    public class Place : ObservableObject
    {
        public int PlaceId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
    }
}
