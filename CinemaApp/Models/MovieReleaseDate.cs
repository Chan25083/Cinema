using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Models
{
    public class MovieReleaseDate
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
