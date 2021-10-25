using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoListApp.Entities;
using ToDoListApp.Services;
using ToDoListApp.View.ViewHelper;


namespace ToDoListAapp.View
{
    public class IndexView
    {
        private readonly UserService _userService;
        private readonly ToDoListService _toDoListService;
        private readonly TaskService _taskService;
        private readonly ConsoleWriter _consoleWriter;

        public IndexView(UserService userService, ToDoListService toDoListServices, TaskService taskService, ConsoleWriter consoleWriter)
        {
            _userService = userService;
            _toDoListService = toDoListServices;
            _taskService = taskService;
            _consoleWriter = consoleWriter;
        }
        public void Render()
        {
            LoopMenu(() => LoginMenu());
            LoopMenu(() => MainMenu());
            LoopMenu(() => UsersManagmentMenu());
            LoopMenu(() => ToDoListManagmentMenu());
            LoopMenu(() => TasksManagmentMenu());

        }

        private void LoopMenu(Func<bool> menu)
        {
            bool shoudExitMenu = false;
            while (!shoudExitMenu)
            {
                shoudExitMenu = menu();
            }
        }

        private bool LoginMenu()
        {
            _consoleWriter.PrintLoginMenu();
            Console.WriteLine("PLease enter a valid command");
            string userChoise = Console.ReadLine();
            switch (userChoise)
            {
                case "1":
                    LogIn();
                    return true;
                case "2":
                    Environment.Exit(0);
                    return false;
                default:
                    _consoleWriter.PrintUnknownCommand();
                    return false;
            }
        }

        private bool MainMenu()
        {
            if (_userService.CurrentUser.Role == Role.Admin)
            {
                _consoleWriter.PrintMainAdminMenu();
                Console.WriteLine("PLease enter a valid command");
                string userChoise = Console.ReadLine();
                switch (userChoise)
                {
                    case "1":
                        LogOut();
                        return true;
                    case "2":
                        UsersManagmentMenu();
                        return true;
                    case "3":
                        ToDoListManagmentMenu();
                        return true;
                    case "4":
                        Environment.Exit(0);
                        return false;
                    default:
                        _consoleWriter.PrintUnknownCommand();
                        return false;
                }
            }
            else
            {
                _consoleWriter.PrintMainMenu();
                Console.WriteLine("PLease enter a valid command");
                string userChoise = Console.ReadLine();
                switch (userChoise)
                {
                    case "1":
                        LogOut();
                        return true;
                    case "2":
                        ToDoListManagmentMenu();
                        return true;
                    case "3":
                        Environment.Exit(0);
                        return false;
                    default:
                        _consoleWriter.PrintUnknownCommand();
                        return false;
                }
            }
        }
        private bool UsersManagmentMenu()
        {
            _consoleWriter.PrintUsersManegment();
            Console.WriteLine("PLease enter a command");
            string adminUserChoise = Console.ReadLine();

            switch (adminUserChoise)
            {
                case "1":
                    ListAllUsers();
                    return UsersManagmentMenu();
                case "2":
                    AddUser();
                    return UsersManagmentMenu();
                case "3":
                    DeleteUser();
                    return UsersManagmentMenu();
                case "4":
                    EditUser();
                    return UsersManagmentMenu();
                case "5":
                    LoopMenu(() => MainMenu());
                    return false;
                case "6":
                case "exit":
                    Environment.Exit(0);
                    return true;
                default:
                    _consoleWriter.PrintUnknownCommand();
                    return UsersManagmentMenu();
            }
        }

        private bool ToDoListManagmentMenu()
        {
            if (_userService.IsUserLogged())
            {
                _consoleWriter.PrintNotLogged();
                return true;
            }
            _consoleWriter.PrintTodoListManagment();
            Console.WriteLine("PLease enter a command");
            string toDoManagmentUserChoise = Console.ReadLine();
            switch (toDoManagmentUserChoise)
            {
                case "1":
                    ListAllToDoLists();
                    return ToDoListManagmentMenu();
                case "2":
                    AddToDoList();
                    return ToDoListManagmentMenu();
                case "3":
                    DeleteToDoList();
                    return ToDoListManagmentMenu();
                case "4":
                    EditToDoList();
                    return ToDoListManagmentMenu();
                case "5":
                    if (OpenToDoList())
                    {
                        return TasksManagmentMenu();
                    }
                    else
                    {
                        return ToDoListManagmentMenu();
                    }
                case "6":
                    LoopMenu(() => MainMenu());
                    return true;
                case "7":
                case "exit":
                    Environment.Exit(0);
                    return true;
                default:
                    _consoleWriter.PrintUnknownCommand();
                    return ToDoListManagmentMenu();
            }
        }

        private bool TasksManagmentMenu()
        {
            _consoleWriter.PrintTaskManegment();
            Console.WriteLine("PLease enter a command");
            string tasksManagmentUserChoise = Console.ReadLine();
            switch (tasksManagmentUserChoise)
            {
                case "1":
                    ListAllTasks();
                    return TasksManagmentMenu();
                case "2":
                    AddToTask();
                    return TasksManagmentMenu();
                case "3":
                    DeleteTask();
                    return TasksManagmentMenu();
                case "4":
                    EditTask();
                    return TasksManagmentMenu();
                case "5":
                    return ToDoListManagmentMenu();
                case "6":
                case "exit":
                    Environment.Exit(0);
                    return true;
                default:
                    _consoleWriter.PrintUnknownCommand();
                    return TasksManagmentMenu();
            }
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
                _consoleWriter.PrintLoginSuccess();
            }
            catch (Exception)
            {
                _consoleWriter.PrintLoginFailed();
                Render();
            }
        }

        private void LogOut()
        {
            _userService.LogOut();
            _consoleWriter.PrintLogoutSuccess();
            Render();
        }

        private void AddUser()
        {
            try
            {
                Console.WriteLine("Enter a new username:");
                string username = Console.ReadLine();
                Console.WriteLine("Enter a password:");
                string password = Console.ReadLine();
                Console.WriteLine("Enter first name:");
                string firstName = Console.ReadLine();
                Console.WriteLine("Enter last name:");
                string lastName = Console.ReadLine();
                Console.WriteLine("Choose a role: Admin or User");
                var role = Enum.Parse<Role>(Console.ReadLine());
                _userService.CreateUser(username, password, firstName, lastName, role);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"User with name {username} added");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception)
            {
                _consoleWriter.PrintException();
            }
        }
        private void DeleteUser()
        {
            try
            {
                Console.WriteLine("Enter a id of the user");
                int userId = Convert.ToInt32(Console.ReadLine());
                _userService.DeleteUser(userId);
            }

            catch (Exception)
            {
                _consoleWriter.PrintException();
            }
        }

        private void ListAllUsers()
        {
            _userService.ListAllUsers();
        }

        private void EditUser()
        {
            try
            {
                Console.WriteLine("Enter a user id:");
                var id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter a new username:");
                var username = Console.ReadLine();
                Console.WriteLine("Enter a new password:");
                var password = Console.ReadLine();
                Console.WriteLine("Enter a new first name:");
                var firstName = Console.ReadLine();
                Console.WriteLine("Enter a new last name:");
                var lastName = Console.ReadLine();
                _userService.EditUser(id, username, password, firstName, lastName);
            }
            catch (Exception)
            {
                _consoleWriter.PrintException();
            }
        }

        private void AddToDoList()
        {
            Console.WriteLine("Enter a name for the list");
            string title = Console.ReadLine();
            try
            {
                _toDoListService.CrateToDoList(title);
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"List with this name:{title} already exist");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private void ListAllToDoLists()
        {
            _toDoListService.ListAllToDoLists();
        }

        private void DeleteToDoList()
        {
            try 
            {
                Console.WriteLine("Enter a id of the ToDoLIst");
                int listid = Convert.ToInt32(Console.ReadLine());
                _toDoListService.DeleteToDoLIst(listid);
            }
            catch (Exception)
            {
                _consoleWriter.PrintException();
            }
        }

        private void EditToDoList()
        {
            try
            {
                Console.WriteLine("Enter an Id of the list");
                int listId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter a name for the list");
                string title = Console.ReadLine();
                _toDoListService.EditToDoList(title, listId);
            }
            catch (Exception)
            {
                _consoleWriter.PrintException();
            }
        }

        private bool OpenToDoList() 
        {
            try
            {
                Console.WriteLine("Enter an id of the list");
                int listId = Convert.ToInt32(Console.ReadLine());
                if (_taskService.OpenListTasks(listId))
                {
                    return TasksManagmentMenu();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This ToDoList is not yours");
                    Console.ForegroundColor = ConsoleColor.Red;
                    return ToDoListManagmentMenu();
                }
            }
            catch (Exception)
            {
                _consoleWriter.PrintException();
                return ToDoListManagmentMenu();
            }
        }

        private void AddToTask()
        {
            try
            {
                Console.WriteLine("Enter a name for the task");
                string title = Console.ReadLine();
                Console.WriteLine("Enter a description for the task");
                string description = Console.ReadLine();
                Console.WriteLine("Choose the task complition: Finished or Unfinished");
                var istTaslComplete = Enum.Parse<CompletionStatus>(Console.ReadLine());
                _taskService.CreateTask(title, description, istTaslComplete);
            }
            catch (Exception)
            {
                _consoleWriter.PrintException();
            }
        }

        private void ListAllTasks()
        {
            _taskService.ListAllTasks();
        }

        private void DeleteTask()
        {
            try
            {
                Console.WriteLine("Enter a id of the task");
                int taskId = Convert.ToInt32(Console.ReadLine());
                _taskService.DeleteTask(taskId);
            }
            catch (Exception)
            {
                _consoleWriter.PrintException();
            }
        }

        private void EditTask()
        {
            try
            {
                Console.WriteLine("Enter an Id of the task");
                int taskId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter a new name for the task");
                string title = Console.ReadLine();
                Console.WriteLine("Enter a new description for the task");
                string description = Console.ReadLine();
                Console.WriteLine("Choose the task complition: Finished or Unfinished");
                var istTaslComplete = Enum.Parse<CompletionStatus>(Console.ReadLine());
                _taskService.EditTask(title, taskId, description, istTaslComplete);
            }
                 catch (Exception)
            {
                _consoleWriter.PrintException();
            }
        }
    }
}

