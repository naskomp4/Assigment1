using System;
using System.Collections.Generic;
using System.Text;
using ToDoListApp.Entities;

namespace ToDoListApp.Data
{
    public class UserStorage : FileStorage<User>
    {
        protected override string FileName => "UserFileStorage.json";
    }
}
