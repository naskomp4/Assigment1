using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListApp.Entities
{
    public class Task : Entity
    {
        public int ToDoListId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public CompletionStatus IsComplete { get; set; }
    }
}
