using InventorySystem;

namespace Collection
{
    public class CollectorItem : IDropCollector
    {
        private readonly Inventory _inventory;

        public CollectorItem (Inventory inventory)
        {
            _inventory = inventory;
        }

        public bool TryCollect (ICollectobleDrop target)
        {
            if (target is CollectableItem itemTarget)
            {
                _inventory.AddItem(itemTarget.Сontent);
                return true;
            }
            return false;
        }
    }
}