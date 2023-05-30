using System.Collections.Generic;

namespace InventorySystem
{
    public interface IItemsContainer
    {
        public IEnumerable<Item> Items { get; }
    }
}