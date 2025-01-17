using System;

namespace ExpenseWise2.Components.Model
{
    public class TagModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
    }
}