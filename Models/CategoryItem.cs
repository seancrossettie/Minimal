using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minimal.Models
{
    public class CategoryItem
    {
        public Guid CategoryItemId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ItemId { get; set; }

        public IEnumerable<Item> Item { get; set; }
    }
}
