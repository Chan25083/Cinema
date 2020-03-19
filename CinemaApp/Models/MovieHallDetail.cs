using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Models
{
    public class MovieHallDetail
    {
        public int Id { get; set; }
        public int HallId { get; set; }
        public int MovieDateTimeId { get; set; }
        public int Row { get; set; }
        public int Coloum { get; set; }
        public Booked booked { get; set; }
        public enum Booked
        {
            Empty,Taked
        }
    }
}
