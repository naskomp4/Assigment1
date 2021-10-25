using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListApp.View.ViewHelper
{
    public class ConsoleWriter
    {
        public void PrintLoginMenu()
        {
            PrintColoredString("--------Login Menu--------");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Exit");
        }

        public void PrintMainMenu()
        {
            PrintColoredString("--------Main Menu--------");
            Console.WriteLine("1. Logout");
            Console.WriteLine("2. ToDo List Management");
            Console.WriteLine("3. Exit");
        }

        public void PrintMainAdminMenu()
        {
            PrintColoredString("--------Main Menu--------");
            Console.WriteLine("1. Logout");
            Console.WriteLine("2. Users Managment");
            Console.WriteLine("3. ToDo List Management");
            Console.WriteLine("4. Exit");
        }

        public void PrintUsersManegment()
        {
            PrintColoredString("--Users Managment Menu--");
            Console.WriteLine("1. List all users");
            Console.WriteLine("2. Add user");
            Console.WriteLine("3. Delete user");
            Console.WriteLine("4. Edit user");
            Console.WriteLine("5. Back");
            Console.WriteLine("6. Exit ");
        }

        public void PrintTodoListManagment()
        {
            PrintColoredString("--ToDo List Managment Menu--");
            Console.WriteLine("1. List all ToDo lists");
            Console.WriteLine("2. Add a ToDo list");
            Console.WriteLine("3. Delete a ToDo list");
            Console.WriteLine("4. Edit a Todo list");
            Console.WriteLine("5. Open Task managment menu");
            Console.WriteLine("6. Back");
            Console.WriteLine("7. Exit ");
        }
        public void PrintTaskManegment()
        {
            PrintColoredString("-- Tasks Managment Menu--");
            Console.WriteLine("1. List all Tasks");
            Console.WriteLine("2. Add a Tasks");
            Console.WriteLine("3. Delete a Tasks");
            Console.WriteLine("4. Edit a Tasks");
            Console.WriteLine("5. Back");
            Console.WriteLine("6. Exit ");
        }

        private void PrintColoredString(string str , ConsoleColor consoleColor= ConsoleColor.DarkMagenta)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void PrintUnknownCommand()
        {
            PrintColoredString("Unknown command", ConsoleColor.Red);
        }

        public void PrintNotLogged()
        {
            PrintColoredString("You are not logged in", ConsoleColor.Red);
        }
       
        public void PrintLogoutSuccess()
        {
            PrintColoredString("Logged out successfully", ConsoleColor.Green);
        }

        public void PrintException()
        {
            PrintColoredString("Wrong input", ConsoleColor.Red);
        }

        public void PrintLoginSuccess()
        {
            PrintColoredString("Logged in successfully", ConsoleColor.Green);
        }

        public void PrintLoginFailed()
        {
            PrintColoredString("Login failed", ConsoleColor.Red);
        }
    }
}
