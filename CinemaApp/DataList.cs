using CinemaApp.Models;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp
{
    public class DataList
    {
        public List<User> UserData { get; set; }
        public List<Movie> MovieData { get; set; }
        public List<MovieReleaseDate> MovieDateTime { get; set; }
        public List<MovieHall> HallData { get; set; }
        public List<MovieHallDetail> HallDetailList { get; set; }

        //=======================================================================================================

        //Input Data
        public List<User> InputUserData()
        {
            this.UserData = new List<User>()
            {
                new User() { Id = 1, UserName = "John", Password = "123456" },
                new User() { Id = 2, UserName = "Mike", Password = "654321" }
            };
            return this.UserData;
        }
        public List<Movie> InputMovieData()
        {
            this.MovieData = new List<Movie>()
            {
                new Movie(){MovieId=1, MovieTitle="Avenger", HallId=1},
                new Movie(){MovieId=2, MovieTitle="Justice League", HallId=2},
                new Movie(){MovieId=3, MovieTitle="Godzilla", HallId=3}
            };
            return this.MovieData;
        }
        public void InputMovieDateTime()
        {
            this.MovieDateTime = new List<MovieReleaseDate>()
            {
                new MovieReleaseDate(){Id=1, MovieId=1, ReleaseDate=new DateTime(2019,4,24,3,0,0)},
                new MovieReleaseDate(){Id=2, MovieId=1, ReleaseDate=new DateTime(2019,4,24,15,0,0)},
                new MovieReleaseDate(){Id=3, MovieId=1, ReleaseDate=new DateTime(2019,4,24,21,0,0)},
                new MovieReleaseDate(){Id=4, MovieId=2, ReleaseDate=new DateTime(2019,4,24,4,0,0)},
                new MovieReleaseDate(){Id=5, MovieId=2, ReleaseDate=new DateTime(2019,4,24,12,0,0)},
                new MovieReleaseDate(){Id=6, MovieId=3, ReleaseDate=new DateTime(2019,4,24,8,0,0)},
            };
            this.HallDetailList.Clear();
            this.InputMovieHallDetail();
        }
        public List<MovieHall> InputMovieHallData()
        {
            this.HallData = new List<MovieHall>()
            {
                new MovieHall(){HallId=1, MovieId=1, HallNumber="One", TotalRow=5, TotalColoum=10},
                new MovieHall(){HallId=2, MovieId=2, HallNumber="Two", TotalRow=5, TotalColoum=6},
                new MovieHall(){HallId=3, MovieId=3, HallNumber="Three", TotalRow=4, TotalColoum=5}
            };
            return this.HallData;
        }
        public List<MovieHallDetail> InputMovieHallDetail()
        {
            this.HallDetailList = new List<MovieHallDetail>();
            int id = 0;

            if (this.MovieData == null || this.MovieData.Count < 1)
            {
                foreach (var item in this.HallData)
                {
                    for (int r = 1; r <= item.TotalRow; r++)
                    {
                        for (int c = 1; c <= item.TotalColoum; c++)
                        {
                            id++;
                            this.HallDetailList.Add(new MovieHallDetail() { Id = id, HallId = item.HallId, Row = r, Coloum = c, booked = 0 });
                        }
                    }
                }
            }
            else
            {
                var movieDateTime = (from dt in this.MovieDateTime
                                     join m in this.MovieData on dt.MovieId equals m.MovieId
                                     join h in this.HallData on m.HallId equals h.HallId
                                     select new { dt.Id, h.HallId, h.TotalRow, h.TotalColoum }).ToList();

                foreach (var item in movieDateTime)
                {
                    for (int r = 1; r <= item.TotalRow; r++)
                    {
                        for (int c = 1; c <= item.TotalColoum; c++)
                        {
                            id++;
                            this.HallDetailList.Add(new MovieHallDetail() { Id = id, HallId = item.HallId, MovieDateTimeId = item.Id, Row = r, Coloum = c, booked = 0 });
                        }
                    }
                }
            }
            return this.HallDetailList;
        }

        //=======================================================================================================

        //Remove Data
        public void RemoveAllData()
        {
            if (this.UserData != null || this.UserData.Count > 0) this.UserData.Clear();
            if (this.MovieData != null || this.MovieData.Count > 0) this.MovieData.Clear();
            if (this.MovieDateTime != null || this.MovieDateTime.Count > 0) this.MovieDateTime.Clear();
            this.HallDetailList.Clear();
            this.InputMovieHallDetail();
        }

        //=======================================================================================================

        //View Data
        public void ViewUserData()
        {
            var table = new ConsoleTable("ID", "Username", "Password");
            foreach (var item in this.UserData)
            {
                table.AddRow(item.Id, item.UserName, item.Password);
            }
            table.Write();
        }
        public void ViewMovieData()
        {
            if (this.MovieData == null || this.MovieData.Count < 1)
            {
                var table = new ConsoleTable("ID", "Movie Title", "Date & Time", "Hall Number");
                foreach (var item in this.HallData)
                {
                    table.AddRow(item.HallId, "No movie show", "-", item.HallNumber);
                }
                table.Write();
            }
            else
            {
                var movieDataList = (from m in this.MovieData
                                     join dt in this.MovieDateTime on m.MovieId equals dt.MovieId
                                     join h in this.HallData on m.HallId equals h.HallId
                                     select new { dt.Id, m.MovieTitle, dt.ReleaseDate, h.HallNumber }).ToList();

                var table = new ConsoleTable("ID", "Movie Title", "Date & Time", "Hall Number");
                foreach (var item in movieDataList)
                {
                    table.AddRow(item.Id, item.MovieTitle, item.ReleaseDate, item.HallNumber);
                }
                table.Write();
            }
        }
        public void ViewMovieHallDetail()
        {
            if (this.MovieData == null || this.MovieData.Count < 1)
            {
                foreach (var item in this.HallData)
                {
                    Console.WriteLine("");
                    Console.WriteLine("=====================================================");
                    Console.WriteLine("");
                    Console.WriteLine($"Movie Title: -  Hall Number: {item.HallNumber}");
                    Console.WriteLine("Date & Time: -");
                    Console.WriteLine($"Total Set: {item.TotalSet}  E: Empty  T: Taked");

                    foreach (var set in this.HallDetailList.Where(hd=>hd.HallId==item.HallId))
                    {
                        Console.Write($"({set.Row}, {set.Coloum})");
                        if (set.Coloum < item.TotalColoum)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("E ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("E");
                            Console.ResetColor();
                        }
                    }
                }
            }
            else
            {
                var hallDetailList = (from m in this.MovieData
                                     join dt in this.MovieDateTime on m.MovieId equals dt.MovieId
                                     join h in this.HallData on m.HallId equals h.HallId
                                     select new { 
                                         m.MovieTitle, dt.ReleaseDate, dt.Id,
                                         h.HallId, h.HallNumber, h.TotalSet, h.TotalColoum
                                     }).ToList();

                foreach (var item in hallDetailList)
                {
                    Console.WriteLine("");
                    Console.WriteLine("=====================================================");
                    Console.WriteLine("");
                    Console.WriteLine($"Movie Title: {item.MovieTitle}  Hall Number: {item.HallNumber}");
                    Console.WriteLine($"Date & Time: {item.ReleaseDate}");
                    Console.WriteLine($"Total Set: {item.TotalSet}  E: Empty  T: Taked");

                    var hallDetailTotalSet = this.HallDetailList.Where(hd => hd.HallId == item.HallId && hd.MovieDateTimeId == item.Id);
                    foreach (var set in hallDetailTotalSet)
                    {
                        Console.Write($"({set.Row}, {set.Coloum})");
                        if (set.Coloum < item.TotalColoum)
                        {
                            if (set.booked == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("E ");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("T ");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            if (set.booked == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("E");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("T");
                                Console.ResetColor();
                            }
                        }
                    }
                }
            }
        }

        //For User
        private void ViewMovieList()
        {
            if (this.MovieData == null || this.MovieData.Count < 1)
            {
                var table = new ConsoleTable("ID", "Movie Title", "Hall Number");
                foreach (var item in this.HallData)
                {
                    table.AddRow(item.HallId, "No movie show", item.HallNumber);
                }
                table.Write();
            }
            else
            {
                var movieDataList = (from m in this.MovieData
                                     join h in this.HallData on m.HallId equals h.HallId
                                     select new { h.HallId, m.MovieTitle, h.HallNumber }).ToList();

                var table = new ConsoleTable("ID", "Movie Title", "Hall Number");
                foreach (var item in movieDataList)
                {
                    table.AddRow(item.HallId, item.MovieTitle, item.HallNumber);
                }
                table.Write();
            }
        }
        private void ViewTheMovieDateTime(int hallId)
        {
                var movieDataList = (from m in this.MovieData
                                     join dt in this.MovieDateTime on m.MovieId equals dt.MovieId
                                     where dt.MovieId == hallId
                                     select new { dt.Id, m.MovieTitle, dt.ReleaseDate }).ToList();

                var table = new ConsoleTable("ID", "Movie Title", "Date & Time");
                foreach (var item in movieDataList)
                {
                    table.AddRow(item.Id, item.MovieTitle, item.ReleaseDate);
                }
                table.Write();
        }
        private void ViewTheMovieHallDetail(int hallId, int movieDateTimeId)
        {
            var hallDetailList = (from m in this.MovieData
                                  join dt in this.MovieDateTime on m.MovieId equals dt.MovieId
                                  join h in this.HallData on m.HallId equals h.HallId
                                  where m.MovieId == hallId && dt.Id == movieDateTimeId
                                  select new
                                  {
                                      m.MovieTitle,
                                      dt.ReleaseDate,
                                      dt.Id,
                                      h.HallId,
                                      h.HallNumber,
                                      h.TotalSet,
                                      h.TotalColoum
                                  }).ToList();

            foreach (var item in hallDetailList)
            {
                Console.WriteLine($"Movie Title: {item.MovieTitle}  Hall Number: {item.HallNumber}");
                Console.WriteLine($"Date & Time: {item.ReleaseDate}");
                Console.WriteLine($"Total Set: {item.TotalSet}  E: Empty  T: Taked");

                var hallDetailTotalSet = this.HallDetailList.Where(hd => hd.HallId == item.HallId && hd.MovieDateTimeId == item.Id);
                foreach (var set in hallDetailTotalSet)
                {
                    Console.Write($"({set.Row}, {set.Coloum})");
                    if (set.Coloum < item.TotalColoum)
                    {
                        if (set.booked == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("E ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("T ");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        if (set.booked == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("E");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("T");
                            Console.ResetColor();
                        }
                    }
                }
            }
        }

        //=======================================================================================================

        //Check Login
        public bool CheckUserData(string username, string password)
        {
            var getUserData = (from u in this.UserData
                                 where u.UserName == username && u.Password == password
                                 select u).SingleOrDefault();

            if (getUserData == null) return false;
            return true;
        }

        //=======================================================================================================

        //Booking Movie Set
        public void ChooseMovie()
        {
            Console.Clear();
            this.ViewMovieList();
            Console.Write("Choose your movie: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string opt = Console.ReadLine();
            Console.ResetColor();

            bool numberOpt = int.TryParse(opt, out int hallId);
            if (numberOpt == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Enter number");
                Console.ResetColor();
            }
            else
            {
                var movieData = (from h in this.HallData
                                 where h.HallId == hallId
                                 select h).SingleOrDefault();
                if (movieData == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Invalid movie option");
                    Console.ResetColor();
                }
                else
                {
                    if (this.MovieData == null || this.MovieData.Count < 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("No movie show in this hall");
                        Console.ResetColor();
                    }
                    else this.ChooseMovieDateTime(movieData.HallId);
                }
            }
        }
        private void ChooseMovieDateTime(int hallId) 
        {
            Console.Clear();
            this.ViewTheMovieDateTime(hallId);
            Console.Write("Choose your movie's date time: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string opt = Console.ReadLine();
            Console.ResetColor();

            bool numberOpt = int.TryParse(opt, out int dateTimeId);
            if (numberOpt == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Enter number");
                Console.ResetColor();
            }
            else
            {
                var movieDateTime = (from dt in this.MovieDateTime
                                     where dt.MovieId == hallId && dt.Id == dateTimeId
                                     select dt).SingleOrDefault();
                if (movieDateTime == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Invalid date time option");
                    Console.ResetColor();
                }
                else this.ChooseMovieSet(hallId, movieDateTime.Id);
            }
        }
        private void ChooseMovieSet(int hallId, int movieDateTimeId)
        {
            Console.Clear();
            this.ViewTheMovieHallDetail(hallId, movieDateTimeId);
            Console.WriteLine("\nChoose your Set: (Row, Coloum)");

            Console.Write("Row: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string setRow = Console.ReadLine();
            Console.ResetColor();

            Console.Write("Coloum: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string setColoum = Console.ReadLine();
            Console.ResetColor();

            bool numberRow = int.TryParse(setRow, out int row);
            bool numberColoum = int.TryParse(setColoum, out int coloum);
            if (numberRow == false || numberColoum == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Enter number");
                Console.ResetColor();
            }
            else
            {
                var hallSet = (from hd in this.HallDetailList
                               where hd.MovieDateTimeId == movieDateTimeId && hd.Row == row && hd.Coloum == coloum
                               select hd).SingleOrDefault();

                if (hallSet == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("No this set in this movie hall");
                    Console.ResetColor();
                }
                else
                {
                    if (hallSet.booked != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("This set already taked");
                        Console.ResetColor();
                    }
                    else
                    {
                        hallSet.booked = MovieHallDetail.Booked.Taked;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Success booked set");
                        Console.ResetColor();
                    }
                }
            }
        }
    }
}
