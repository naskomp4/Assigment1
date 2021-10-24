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
        public SingleToDoList CurrentToDolist { get; private set; }
        private readonly ToDoListStorage _toDoListStorage;
        private readonly UserStorage _userStorage;

        public ToDoListService(ToDoListStorage fileStorage)
        {
            _toDoListStorage = fileStorage;
        }
    

        public void CrateToDoList(string title,int currentUserId)
        {

            if (_toDoListStorage.ReadAll().Any(l => l.Title == title))
            {
                throw new Exception($"ToDo list with the same name:{title} already exist");
            }

            int newUniqueId = _toDoListStorage.GetLastId() + 1;
            DateTime now = DateTime.Now;
            var toDoList = new SingleToDoList()
            {
                Title = title,
                Id = newUniqueId,
                CreatedAt = now,
                CreatorId = currentUserId,
                DateOfLastChange = now,
                LastModifierId = currentUserId,
            };
            _toDoListStorage.Add(toDoList);
        }
        public void ListAllToDoLists(int currentUserId)
        {
            var listAll = _toDoListStorage.ReadAll();
            foreach (var item in listAll)
            {
                if (currentUserId == item.CreatorId)
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

        public void DeleteToDoLIst(int currentListId, int currentUserId)
        {
            if (!_toDoListStorage.ReadAll().Any(u => u.CreatorId == currentUserId && u.Id == currentListId))
            {
                _toDoListStorage.Delete(currentListId);
            }
        }
    
        public void EditToDoList(string title ,int currentUserId , int currentListId)
        {
            if (!_toDoListStorage.ReadAll().Any(u => u.CreatorId == currentUserId && u.Id == currentListId))
            {
                Console.WriteLine("You are not the creator of the list");
                // throw new Exception($"You are not the creator of the ToDo list ");
                return;
            }
            DateTime now = DateTime.Now;
            var toDoList = new SingleToDoList()
            {
                Title = title,
                Id = currentListId,
                CreatedAt = now,
                DateOfLastChange = now,
                LastModifierId = currentUserId,
            };
            _toDoListStorage.Edit(toDoList);
        }
        public int GetToDoListId()
        {
            return CurrentToDolist.Id;
        }
    }
}
