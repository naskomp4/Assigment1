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
        private readonly UserService _userService;
        private readonly ToDoListService _toDoListService;
        private readonly ToDoListStorage _toDoListStorage;

        public TaskService(TaskStorage fileStorage, UserService userService, ToDoListService toDoListService, ToDoListStorage toDoListStorage)
        {
            _taskStorage = fileStorage;
            _userService = userService;
            _toDoListService = toDoListService;
            _toDoListStorage = toDoListStorage;

        }
        public void CreateTask(string title, string description, IsComplete isComplete)
        {

            if (_taskStorage.ReadAll().Any(t => t.Title == title && t.Description == description))
            {
                throw new Exception($"Task with the same name: {title} and description: {description} already exist");
            }
            DateTime now = DateTime.Now;
            var task = new Task()
            {
                Id = _taskStorage.GetNextId(),
                ToDoListId = _toDoListService.CurrentToDoList.Id,
                Title = title,
                Description = description,
                IsComplete = isComplete,
                CreatedAt = now,
                CreatorId = _userService.CurrentUser.Id,
                DateOfLastChange = now,
                LastModifierId = _userService.CurrentUser.Id,
            };
            _taskStorage.Add(task);
        }
        public void ListAllTasks()
        {
            var listAll = _taskStorage.ReadAll();
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
        public void DeleteTask(int curentTaskid)
        {
            if (_taskStorage.Read(curentTaskid).CreatorId == _userService.CurrentUser.Id)
            {
                _taskStorage.Delete(curentTaskid);
            }
        }
        public void EditTask(string title, int currentTaskId, string description, IsComplete isComplete)
        {
            var task = _taskStorage.Read(currentTaskId);
            if (task.CreatorId != _userService.CurrentUser.Id && _toDoListService.CurrentToDoList.Id == task.ToDoListId)
            {
                throw new Exception($"You are not the creator of the ToDo list ");
            }
            task.Title = title;
            task.Description = description;
            task.IsComplete = isComplete;
            task.DateOfLastChange = DateTime.Now;
            task.LastModifierId = _userService.CurrentUser.Id;
            _taskStorage.Edit(task);
        }
        public bool OpenListTasks(int openedListId)
        {
            var singleToDoList = _toDoListStorage.Read(openedListId);
            if (singleToDoList.Id == openedListId && _userService.CurrentUser.Id == singleToDoList.CreatorId)
            {
                Console.WriteLine($"List id:                    {singleToDoList.Id}");
                Console.WriteLine($"List name:                  {singleToDoList.Title}");
                Console.WriteLine($"List date of creation:      {singleToDoList.CreatedAt}");
                Console.WriteLine($"List creator id:            {singleToDoList.CreatorId}");
                Console.WriteLine($"List date of last change:   {singleToDoList.DateOfLastChange}");
                Console.WriteLine($"List last modifier id:      {singleToDoList.LastModifierId}\n");
                return true;
            }
            return false;
        }
    }
}
