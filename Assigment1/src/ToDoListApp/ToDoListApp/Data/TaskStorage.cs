using System;
using System.Collections.Generic;
using System.Text;
using ToDoListApp.Data;
using ToDoListApp.Entities;

namespace ToDoList.Data
{
    public class TaskStorage : FileStorage<Task>
    {
        protected override string FileName => "TaskStorage.json";
    }
}
