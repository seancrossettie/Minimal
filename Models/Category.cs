using System;

namespace Minimal.Models
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public Int16 TotalCategoryItems { get; set; }
        public Int16 TotalCategoryItemsRemoved { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
