using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp
{
    public class CinemaApp
    {
        DataList dataList = new DataList();
        public void BasicMenu()
        {
            dataList.InputMovieHallData();
            dataList.InputMovieHallDetail();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome To Cinema App");
                Console.WriteLine("0. Input data");
                Console.WriteLine("1. View all user data");
                Console.WriteLine("2. View all movie");
                Console.WriteLine("3. View all movie hall Detail");
                Console.WriteLine("4. Login");
                Console.WriteLine("5. Exit app");
                Console.Write("Enter your option: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                string option = Console.ReadLine();
                Console.ResetColor();
                switch (option)
                {
                    case "0":
                        this.InputDataMenu();
                        break;
                    case "1":
                        if (dataList.UserData == null || dataList.UserData.Count < 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("No user data");
                            Console.ResetColor();
                        }
                        else
                        {
                            dataList.ViewUserData();
                        }
                        break;
                    case "2":
                        dataList.ViewMovieData();
                        break;
                    case "3":
                        dataList.ViewMovieHallDetail();
                        break;
                    case "4":
                        this.Login();
                        break;
                    case "5":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Thank You For Using");
                        Console.ResetColor();
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Invalid option");
                        Console.ResetColor();
                        break;
                }
                Console.ReadKey();
            }
        }
        private void InputDataMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome To Cinema Input Data");
                Console.WriteLine("0. Clear all data");
                Console.WriteLine("1. Input user data");
                Console.WriteLine("2. Input movie data");
                Console.WriteLine("3. Back");
                Console.Write("You want: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                string option = Console.ReadLine();
                Console.ResetColor();
                switch (option)
                {
                    case "0":
                        if ((dataList.UserData == null || dataList.UserData.Count < 1) &&
                            (dataList.MovieData == null || dataList.MovieData.Count < 1) &&
                            (dataList.MovieDateTime == null || dataList.MovieDateTime.Count < 1))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("No data can delete");
                            Console.ResetColor();
                        }
                        else
                        {
                            dataList.RemoveAllData();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Successfull delete all data");
                            Console.ResetColor();
                        }
                        break;
                    case "1":
                        if (dataList.UserData == null || dataList.UserData.Count < 1)
                        {
                            dataList.InputUserData();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Successfull input user data");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Already input data");
                            Console.ResetColor();
                        }
                        break;
                    case "2":
                        if (dataList.MovieData == null || dataList.MovieData.Count < 1)
                        {
                            dataList.InputMovieData();
                            dataList.InputMovieDateTime();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Successfull input movie data");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Already input data");
                            Console.ResetColor();
                        }
                        break;
                    case "3":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Going Back");
                        Console.ResetColor();
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Invalid option");
                        Console.ResetColor();
                        break;
                }
                Console.ReadKey();
            }
        }
        private void Login()
        {
            Console.Clear();
            Console.Write("Username: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string username = Console.ReadLine();
            Console.ResetColor();
            Console.Write("Password: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string password = Console.ReadLine();
            Console.ResetColor();

            if(dataList.CheckUserData(username, password))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Loggin in");
                Console.ResetColor();
                this.UserMenu(username);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Incorrect Username / Password");
                Console.Write("Try again later");
                Console.ResetColor();
            }
        }
        private void UserMenu(string username)
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Hi ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(username);
                Console.ResetColor();
                Console.WriteLine(", Welcome To Cinema App");
                Console.WriteLine("1. View all movie");
                Console.WriteLine("2. View all movie hall Detail");
                Console.WriteLine("3. Booking movie");
                Console.WriteLine("4. Logout");
                Console.Write("Enter your option: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                string option = Console.ReadLine();
                Console.ResetColor();
                switch (option)
                {
                    case "1":
                        dataList.ViewMovieData();
                        break;
                    case "2":
                        dataList.ViewMovieHallDetail();
                        break;
                    case "3":
                        dataList.ChooseMovie();
                        break;
                    case "4":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Loggin out");
                        Console.ResetColor();
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Invalid option");
                        Console.ResetColor();
                        break;
                }
                Console.ReadKey();
            }
        }
    }
}
