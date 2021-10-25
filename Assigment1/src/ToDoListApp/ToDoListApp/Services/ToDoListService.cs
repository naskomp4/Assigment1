using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoListApp.Data;
using ToDoListApp.Entities;


namespace ToDoListApp.Services
{
    public class ToDoListService
    {
        public User CurrentToDoList { get; private set; }
        private readonly ToDoListStorage _toDoListStorage;
        private readonly UserService _userService;

        public ToDoListService(ToDoListStorage fileStorage, UserService userService)
        {
            _toDoListStorage = fileStorage;
            _userService = userService;
        }


        public void CrateToDoList(string title)
        {
            if (_toDoListStorage.ReadAll().Any(l => l.Title == title && l.CreatorId == _userService.CurrentUser.Id))
            {
                throw new Exception($"ToDo list with the same name:{title} already exist");
            }
            DateTime now = DateTime.Now;
            var toDoList = new SingleToDoList()
            {
                Title = title,
                Id = _toDoListStorage.GetNextId(),
                CreatedAt = now,
                CreatorId = _userService.CurrentUser.Id,
                DateOfLastChange = now,
                LastModifierId = _userService.CurrentUser.Id,
            };
            _toDoListStorage.Add(toDoList);
        }
        public void ListAllToDoLists()
        {
            var listAll = _toDoListStorage.ReadAll();
            foreach (var item in listAll)
            {
                if (_userService.CurrentUser.Id == item.CreatorId)
                {
                    Console.WriteLine($"List id:                    {item.Id}");
                    Console.WriteLine($"List name:                  {item.Title}");
                    Console.WriteLine($"List date of creation:      {item.CreatedAt}");
                    Console.WriteLine($"List creator id:            {item.CreatorId}");
                    Console.WriteLine($"List date of last change:   {item.DateOfLastChange}");
                    Console.WriteLine($"List last modifier id:      {item.LastModifierId}\n");
                }
            }
        }
        public void DeleteToDoLIst(int currentListId)
        {
            if (_toDoListStorage.Read(currentListId).CreatorId == _userService.CurrentUser.Id)
            {
                _toDoListStorage.Delete(currentListId);
            }
        }
        public void EditToDoList(string title, int currentListId)
        {
            var singleToDoList = _toDoListStorage.Read(currentListId);
            if (singleToDoList.CreatorId != _userService.CurrentUser.Id)
            {
                throw new Exception($"You are not the creator of the ToDo list ");
            }
            singleToDoList.Title = title;
            singleToDoList.DateOfLastChange = DateTime.Now;
            singleToDoList.LastModifierId = _userService.CurrentUser.Id;
            _toDoListStorage.Edit(singleToDoList);
        }
    }
}
