using System;
using System.Collections.Generic;

namespace Minimal.Models
{
    public class CategoryItem
    {
        public Guid CategoryItemId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ItemId { get; set; } 
    }
}
