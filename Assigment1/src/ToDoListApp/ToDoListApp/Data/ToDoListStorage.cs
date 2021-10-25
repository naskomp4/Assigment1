using System;
using System.Collections.Generic;
using System.Text;
using ToDoListApp.Entities;

namespace ToDoListApp.Data
{
    public class ToDoListStorage : FileStorage<ToDoList>
    {
        protected override string FileName => "ToDoListStorage.json";
    }
}
