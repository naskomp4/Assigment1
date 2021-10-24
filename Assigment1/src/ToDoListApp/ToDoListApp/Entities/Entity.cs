using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp.Entities
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatorId { get; set; }
        public DateTime DateOfLastChange { get; set; }
        public int LastModifierId { get; set; }
    }
}
