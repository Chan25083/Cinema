using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Models
{
    public class MovieHall
    {
        public int HallId { get; set; }
        public int MovieId { get; set; }
        public string HallNumber { get; set; }
        public int TotalRow { get; set; }
        public int TotalColoum { get; set; }
        public int TotalSet => TotalRow * TotalColoum;
    }
}
