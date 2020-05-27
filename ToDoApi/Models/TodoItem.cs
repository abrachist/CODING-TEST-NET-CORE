using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CompletePercentage { get; set; }
        public string Status { get; set; }
    }
}
