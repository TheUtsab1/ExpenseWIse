using System;

namespace ExpenseWise2.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public Tag()
        {
            CreatedAt = DateTime.Now;
        }
    }
}