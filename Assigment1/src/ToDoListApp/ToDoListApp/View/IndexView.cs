using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoListApp.Entities;
using ToDoListApp.Services;

namespace ToDoListAapp.View
{
    public class IndexView
    {
        private readonly UserService _userService;
        private readonly ToDoListService _toDoListService;
        private readonly TaskService _taskService;
        //private readonly List<MenuItem> _menuItems = new List<MenuItem>();
        // private readonly Dictionary<MenuItemEnum, MenuItem> items;
        // create enum with all possible menu item values
        // foreach on the dictionary to display the items 
        // update the menu item based on the enum key when needed

        public IndexView(UserService userService, ToDoListService toDoListServices, TaskService taskService)
        {
            _userService = userService;
            _toDoListService = toDoListServices;
            _taskService = taskService;
            MainMenu();
            //  InitializeMenuItems(() => UsersManagmentMenu());
        }

        /*    private void InitializeMenuItems(Func<bool> action)
            {
                _menuItems.Add(new MenuItem() { Name = "LogIn", Show = true });
                _menuItems.Add(new MenuItem() { Name = "LogOut", Show = false });
                _menuItems.Add(new MenuItem() { Name = "Exit", Show = true });
                action();
                // add all options
            }*/

        public void Render()
        {
            bool shouldExitMainMenu = false;
            while (!shouldExitMainMenu)
            {
                shouldExitMainMenu = MainMenu();
            }
        }

        private bool MainMenu()
        {
            RenderMainMenu();
            Console.WriteLine("PLease enter a valid command");
            string userChoise = Console.ReadLine();

            switch (userChoise)
            {
                case "1":
                    if (_userService.CurrentUser == null)
                    {
                        LogIn();
                        //items[MenuItemEnum.Login].Show = false;
                    }
                    else
                    {
                        LogOut();
                    }
                    return false;
                case "2":
                    bool shouldExitUsersManagmentMenu = false;
                    while (!shouldExitUsersManagmentMenu)
                    {
                        shouldExitUsersManagmentMenu = UsersManagmentMenu();
                    }
                    return false;
                case "3":
                    bool shouldExitToDoListManagmentMenu = false;
                    while (!shouldExitToDoListManagmentMenu)
                    {
                        shouldExitToDoListManagmentMenu = ToDoListManagmentMenu();
                    }
                    return false;
                case "4":
                case "exit":
                    Environment.Exit(0);
                    return true;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unknown command");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;

            }
        }

        private void RenderMainMenu()
        {

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("--------Main Menu--------");
            Console.ForegroundColor = ConsoleColor.White;
            /*            var menuItems = _menuItems.Where(menuItem => menuItem.Show == true).ToList();

                        for (int i = 1; i <= menuItems.Count; i++)
                        {
                            Console.WriteLine($"{i}. {menuItems[i - 1].Name}");
                        }*/


            if (_userService.IsUserLogged())
            {
                Console.WriteLine("1. LogIn ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You are logged in as: {_userService.CurrentUser.Name}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1. LogOut");
            }
            Console.WriteLine("2. Users Managment");
            Console.WriteLine("3. ToDo List Management");
            Console.WriteLine("4. Exit (exit)");

        }
        private bool UsersManagmentMenu()
        {
            if (_userService.CurrentUser == null || !_userService.CurrentUser.Role.ToString().Equals("Admin"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Only admin can access this menu");
                Console.ForegroundColor = ConsoleColor.White;
                return true;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You are logged as an admin");
            Console.ForegroundColor = ConsoleColor.White;
            RenderUsersManagmentMenu();
            Console.WriteLine("PLease enter a command");
            string adminUserChoise = Console.ReadLine();

            switch (adminUserChoise)
            {
                case "1":
                    ListAllUsers();
                    return false;
                case "2":
                    AddUser();
                    return false;
                case "3":
                    DeleteUser();
                    return false;
                case "4":
                    EditUser();
                    return false;
                case "5":
                    return true;
                case "6":
                case "exit":
                    Environment.Exit(0);
                    return true;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unknown command");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
            }
        }
        private static void RenderUsersManagmentMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("--Users Managment Menu--");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. List all users");
            Console.WriteLine("2. Add user");
            Console.WriteLine("3. Delete user");
            Console.WriteLine("4. Edit user");
            Console.WriteLine("5. Back");
            Console.WriteLine("6. Exit (exit)");
        }

        private bool ToDoListManagmentMenu()
        {
            if (_userService.IsUserLogged())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You are not logged in");
                Console.ForegroundColor = ConsoleColor.White;
                return true;
            }
            RenderToDoListManagmentMenu();
            Console.WriteLine("PLease enter a command");
            string toDoManagmentUserChoise = Console.ReadLine();
            switch (toDoManagmentUserChoise)
            {
                case "1":
                    ListAllToDoLists();
                    return false;
                case "2":
                    AddToDoList();
                    return false;
                case "3":
                    DeleteToDoList();
                    return false;
                case "4":
                    EditToDoList();
                    return false;
                case "5":
                    bool shouldExitTasksManagmentMenu = false;
                    while (!shouldExitTasksManagmentMenu)
                    {
                        shouldExitTasksManagmentMenu = TasksManagmentMenu();
                    }
                    return false;
                case "6":
                    return true;
                case "7":
                case "exit":
                    Environment.Exit(0);
                    return true;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unknown command");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
            }
        }
        private static void RenderToDoListManagmentMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("--ToDo List Managment Menu--");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. List all ToDo lists");
            Console.WriteLine("2. Add a ToDo list");
            Console.WriteLine("3. Delete a ToDo list");
            Console.WriteLine("4. Edit a Todo list");
            Console.WriteLine("5. Open Task menu");
            Console.WriteLine("6. Back");
            Console.WriteLine("7. Exit (exit)");
        }

        private bool TasksManagmentMenu()
        {
            RenderTaskstManagmentMenu();
            Console.WriteLine("PLease enter a command");
            string tasksManagmentUserChoise = Console.ReadLine();
            switch (tasksManagmentUserChoise)
            {
                case "1":
                    ListAllTasks();
                    return false;
                case "2":
                    AddToTask();
                    return false;
                case "3":
                    DeleteTask();
                    return false;
                case "4":
                    EditTask();
                    return false;
                case "5":
                    return true;
                case "6":
                case "exit":
                    Environment.Exit(0);
                    return true;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unknown command");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
            }
        }
        private static void RenderTaskstManagmentMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("-- Tasks Managment Menu--");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. List all Tasks");
            Console.WriteLine("2. Add a Tasks");
            Console.WriteLine("3. Delete a Tasks");
            Console.WriteLine("4. Edit a Tasks");
            Console.WriteLine("5. Back");
            Console.WriteLine("6. Exit (exit)");
        }

        private void LogIn()
        {
            try
            {
                Console.WriteLine("Enter your user name:");
                string userName = Console.ReadLine();
                Console.WriteLine("Enter your password:");
                string userPassword = Console.ReadLine();
                _userService.LogIn(userName, userPassword);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Login successful.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Login failed.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        private void LogOut()
        {
            _userService.LogOut();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Logged out successfully");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void AddUser()
        {
            Console.WriteLine("Enter a new username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter a password:");
            string password = Console.ReadLine();
            Console.WriteLine("Enter first name:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter last name:");
            string lastName = Console.ReadLine();

            try
            {
                Console.WriteLine("Choose a role: Admin or User");
                var role = Enum.Parse<Role>(Console.ReadLine());
                _userService.CreateUser(username, password, firstName, lastName, role);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"User with name '{username}' added");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (ArgumentException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid role");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private void DeleteUser()
        {
            Console.WriteLine("Enter a id of the user");
            int id = Convert.ToInt32(Console.ReadLine());
            _userService.DeleteUser(id);
        }
        private void ListAllUsers()
        {
            _userService.ListAllUsers();
        }
        private void EditUser()
        {
            Console.WriteLine("Enter a user id:");
            var id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter a new username:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter a new password:");
            var password = Console.ReadLine();
            Console.WriteLine("Enter a new first name:");
            var firstName = Console.ReadLine();
            Console.WriteLine("Enter a new last name:");
            var lastName = Console.ReadLine();
            _userService.EditUser(id, name, password, firstName, lastName);
        }

        private void AddToDoList()
        {
            Console.WriteLine("Enter a name for the list");
            string title = Console.ReadLine();
            int userId = _userService.GetUserId();

            _toDoListService.CrateToDoList(title, userId);
        }

        private void ListAllToDoLists()
        {
            int userid = _userService.GetUserId();
            _toDoListService.ListAllToDoLists(userid);
        }
        private void DeleteToDoList()
        {
            Console.WriteLine("Enter a id of the ToDoLIst");
            int listid = Convert.ToInt32(Console.ReadLine());
            int userid = _userService.GetUserId();
            _toDoListService.DeleteToDoLIst(listid, userid);
        }
        private void EditToDoList()
        {
            Console.WriteLine("Enter an Id of the list");
            int listId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter a name for the list");
            string title = Console.ReadLine();
            int userid = _userService.GetUserId();
            _toDoListService.EditToDoList(title, userid, listId);
        }
        private void AddToTask()
        {
            Console.WriteLine("Enter a name for the task");
            string title = Console.ReadLine();
            Console.WriteLine("Enter a description for the task");
            string description = Console.ReadLine();
            int toDoListId = _toDoListService.GetToDoListId();
            int currentUserId = _userService.GetUserId();
            Console.WriteLine("Is the task complete (yes or no)");
            string isComplete = Console.ReadLine();
            if (!isComplete.Equals("yes") || !isComplete.Equals("no"))
            {
                Console.WriteLine("Invalid input");
            }
            else
            {
                _taskService.CreateTask(title, description, toDoListId, isComplete, currentUserId);
            }
        }
        private void ListAllTasks()
        {
            int userid = _userService.GetUserId();
            int toDoListId = _toDoListService.GetToDoListId();
            _taskService.ListAllTasks(userid, toDoListId);
        }
        private void DeleteTask()
        {
            Console.WriteLine("Enter a id of the task");
            int id = Convert.ToInt32(Console.ReadLine());
            _taskService.DeleteTask(id);
        }
        private void EditTask()
        {
            Console.WriteLine("Enter an Id of the task");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter a new name for the task");
            string title = Console.ReadLine();
            Console.WriteLine("Enter a new description for the task");
            string description = Console.ReadLine();
            int userid = _userService.GetUserId();
            int listId = _toDoListService.GetToDoListId();
            Console.WriteLine("Is the task complete (yes or no)");
            string isComplete = Console.ReadLine();
            if (!isComplete.Equals("yes") || !isComplete.Equals("no"))
            {
                Console.WriteLine("Invalid input");
            }
            else
            {
                _taskService.EditTask(title, userid, listId, id, description, isComplete);
            }
        }
    }
}

