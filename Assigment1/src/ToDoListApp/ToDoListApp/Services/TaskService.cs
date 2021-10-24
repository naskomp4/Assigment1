using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoListApp.Data;
using ToDoListApp.Entities;

namespace ToDoListApp.Services
{
   public class TaskService
    {
        public Task CurrentTask { get; set; }
        private readonly TaskStorage _taskStorage;

        public TaskService(TaskStorage fileStorage)
        {
            _taskStorage = fileStorage;
        }
        public void CreateTask(string title,string description, int toDoListId, string isComplete ,int currentUserId)
        {

            if (_taskStorage.ReadAll().Any(t => t.Title == title && t.Description == description))
            {
                throw new Exception($"Task with the same name: {title} and description: {description} already exist");
            }

            int newUniqueId = _taskStorage.GetLastId() + 1;
            DateTime now = DateTime.Now;
            var task = new Task()
            {
                Id = newUniqueId,
                ToDoListId = toDoListId,
                Title = title,
                Description = description,
                IsComplete = isComplete,
                CreatedAt = now,
                CreatorId = currentUserId,
                DateOfLastChange = now,
                LastModifierId = currentUserId,
            };
            _taskStorage.Add(task);
        }
        public void ListAllTasks(int currentUserId, int currentListId)
        {
            var listAll = _taskStorage.ReadAll();
            foreach (var item in listAll)
            {
                if (currentUserId == item.CreatorId && currentListId == item.ToDoListId)
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
            public void DeleteTask(int id)
            {
                if (_taskStorage.ReadAll().Any(u => u.Id == id))
                {
                    _taskStorage.Delete(id);
                }
            }
        public void EditTask(string title, int currentUserId, int currentListId ,int currentTaskId,string description,string isComplete)
        {
            if (_taskStorage.ReadAll().Any(u => u.Id == currentListId))
            {
                throw new Exception($"Task with the same name: {title} and description: {description} already exist");
            }
            DateTime now = DateTime.Now;
            var task = new Task()
            {
                Id = currentTaskId,
                ToDoListId = currentListId,
                Title = title,
                Description = description,
                IsComplete = isComplete,
                CreatedAt = now,
                CreatorId = currentUserId,
                DateOfLastChange = now,
                LastModifierId = currentUserId,
            };
            _taskStorage.Edit(task);
        }
    }
}
