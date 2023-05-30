using System;
using System.Collections.Generic;

namespace InventorySystem
{
    public class Equipment : IItemsContainer
    {
        public event Action<EquipmentItem> OnAdd;
        public event Action<EquipmentItem> OnRemove;
        protected readonly List<EquipmentItem> items = new List<EquipmentItem>();

        public override string ToString () => string.Join(Environment.NewLine, items);

        public IEnumerable<Item> Items => items;
        public ItemsContainer<EquipmentItem> EquipmentItems => new ItemsContainer<EquipmentItem>(items);

        /// <returns> Return item that was equipped in same slot </returns>
        public EquipmentItem Equipe (EquipmentItem item)
        {
            item = Validate(item, out EquipmentItem current);
            items.Add(item);
            OnAdd?.Invoke(item);
            return current;
        }

        public EquipmentItem GetItem (string slot)
        {
            for (int i = 0; i < items.Count; i++)
                if (items[i].Slot.Name == slot)
                    return items[i];
            return null;
        }

        protected void QuietEquipe (EquipmentItem item)
        {
            item = Validate(item, out _);
            items.Add(item);
        }

        private void Remove (EquipmentItem item)
        {
            items.Remove(item);
            OnRemove(item);
        }

        private EquipmentItem Validate (EquipmentItem item, out EquipmentItem current)
        {
            if (item.IsInstance)
                item = item.GetCopy() as EquipmentItem;
            current = GetItem(item.Slot.Name);
            if (current != null)
                Remove(current);
            return item;
        }
    }
}