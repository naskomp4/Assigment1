using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListApp.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Role Role { get; set; }
    }
}
