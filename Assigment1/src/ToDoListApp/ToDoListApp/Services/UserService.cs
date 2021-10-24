﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoListApp.Data;
using ToDoListApp.Entities;

namespace ToDoListApp.Services
{
    public class UserService
    {

        public User CurrentUser { get; private set; }
        private readonly UserStorage _userStorage;

        public UserService(UserStorage fileStorage)
        {
            _userStorage = fileStorage;
            if (_userStorage.IsEmpty())
            {
                CreateUser("admin", "adminpassword", "Admin", "Admin", Role.Admin);
            }
        }

        public void EditUser(int id, string name, string password, string firstName, string lastName)
        {
            if (_userStorage.ReadAll().Any(u => u.Id == id))
            {
                DateTime now = DateTime.Now;
                var data = new User()
                {
                    Id = id,
                    Name = name,
                    Password = password,
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfLastChange = now,
                    LastModifierId = CurrentUser != null ? CurrentUser.Id : 0,
                };
                _userStorage.Edit(data);
            }
            else
            {
                Console.WriteLine($"Entity with id:{id} does not exist");
            }
        }
        public void CreateUser(string name, string password, string firstName, string lastname, Role role)
        {

            if (_userStorage.ReadAll().Any(u => u.Name == name))
            {
                throw new Exception($"User with name:{name} already exist");
            }

            int newUniqueId = _userStorage.GetLastId() + 1;
            DateTime now = DateTime.Now;
            var user = new User()
            {
                Name = name,
                Password = password,
                Id = newUniqueId,
                CreatedAt = now,
                FirstName = firstName,
                LastName = lastname,
                Role = role,
                CreatorId = CurrentUser != null ? CurrentUser.Id : 1,
                DateOfLastChange = now,
                LastModifierId = CurrentUser != null ? CurrentUser.Id : 1,
            };
            _userStorage.Add(user);

        }

        public void DeleteUser(int id)
        {
            if (_userStorage.ReadAll().Any(u => u.Id == id))
            {
                _userStorage.Delete(id);
            }
        }

        public void ListAllUsers()
        {
            var listAll = _userStorage.ReadAll();
            foreach (var item in listAll)
            {
                Console.WriteLine($"User id:                    {item.Id}");
                Console.WriteLine($"User username:              {item.Name}");
                Console.WriteLine($"User password:              {item.Password}");
                Console.WriteLine($"User first name:            {item.FirstName}");
                Console.WriteLine($"User last name:             {item.LastName}");
                Console.WriteLine($"User role:                  {item.Role}");
                Console.WriteLine($"User date of creation:      {item.CreatedAt}");
                Console.WriteLine($"User creator id:            {item.CreatorId}");
                Console.WriteLine($"User date of last change:   {item.DateOfLastChange}");
                Console.WriteLine($"User last modifier id:      {item.LastModifierId}\n");
            }
        }

        public void LogIn(string userName, string userPassword)
        {
            var user = _userStorage.ReadAll().FirstOrDefault(u => u.Name == userName && u.Password == userPassword);
            if (user != null)
            {
                CurrentUser = user;
                return;
            }
            throw new Exception("No such user");
        }

        public void LogOut()
        {
            CurrentUser = null;
        }

        public bool IsUserLogged()
        {
            return CurrentUser == null;
        }
        public int GetUserId()
        {
            return CurrentUser.Id;
        }
        
    }
}