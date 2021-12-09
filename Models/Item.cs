using System;

namespace Minimal.Models
{
    public class Item
    {
        public Guid ItemId { get; set; }
        public Guid UserId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public DateTime TimeOwned { get; set; }
        public Int16 Quantity { get; set; } = 1;
        public bool IsDuplicate { get; set; } = false;
        public bool IsRemoved { get; set; } = false;
        public Int16 NecessityRank { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
