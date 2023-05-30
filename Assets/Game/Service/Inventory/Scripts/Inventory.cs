using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class Inventory : IItemsContainer
    {
        public Action<Item> OnAdd;
        public Action<Item> OnRemove;
        public Action<Item> OnChange;
        protected readonly Dictionary<int, Item> slots = new Dictionary<int, Item>();

        public override string ToString () => string.Join(", "+Environment.NewLine, slots.Select(s => string.Format("{0}: {1}", s.Key, s.Value)));

        public IEnumerable<Item> Items => slots.Values;

        public void AddItem (Item item)
        {
            if (item.IsInstance)
                item = item.GetCopy();
            if (ContainItem(item, out _))
            {
                IsContain(item);
                return;
            }
            if (TryAddStackToExist(item) == false)
            {
                QuietAddItem(item, -1);
                OnAdd?.Invoke(item);
            }
        }

        public bool RemoveItem (Item item)
        {
            if (ContainItem(item, out int slotIndex) == false)
                return false;
            item.OnConsume -= ItemConsumed;
            item.OnChange -= ItemChanged;
            slots.Remove(slotIndex);
            OnRemove?.Invoke(item);
            return true;
        }

        public bool ContainItem (Item item, out int slotIndex)
        {
            slotIndex = 0;
            foreach (var slot in slots)
                if (slot.Value == item)
                {
                    slotIndex = slot.Key;
                    return true;
                }
            return false;
        }

        public Item GetItemFromSlot (int index) => slots.ContainsKey(index) ? slots[index] : null;

        public Item GetItem (int id)
        {
            foreach (var slot in slots)
                if (slot.Value.ID == id)
                    return slot.Value;
            return null;
        }

        public int GetStackAmount (int id)
        {
            Item item = GetItem(id);
            if (item == null)
                return 0;

            else if (item is StackableItem sItem)
                return sItem.Amount;
            else
                IsNotStackableType(id);
            return 0;
        }

        /// <summary> Add or spend amount </summary>
        public void ChangeStackAmount (int id, int amount)
        {
            Item item = GetItem(id);
            if (item == null)
                IsNotContain(id);
            else if (item is StackableItem sItem)
                sItem.Amount += amount;
            else
                IsNotStackableType(id);
        }

        protected void QuietAddItem (Item item, int slotIndex = -1)
        {
            if (item.IsInstance)
                item = item.GetCopy();

            if (slotIndex < 0)
                slotIndex = GetFreeSlotIndex();
            if (slots.ContainsKey(slotIndex))
            {
                Debug.LogWarning(string.Format("Slot {0} already busy", slotIndex));
                slotIndex = GetFreeSlotIndex();
            }

            item.OnConsume += ItemConsumed;
            item.OnChange += ItemChanged;
            slots.Add(slotIndex, item);
        }

        private void ItemConsumed (Item item) => RemoveItem(item);

        private void ItemChanged (Item item) => OnChange?.Invoke(item);

        private bool TryAddStackToExist (Item item)
        {
            if (item is StackableItem sItem)
                if (GetItem(sItem.ID) != null)
                {
                    ChangeStackAmount(sItem.ID, sItem.Amount);
                    return true;
                }
            return false;
        }

        private int GetFreeSlotIndex ()
        {
            int index = 0;
            int[] indexes = slots.Keys.ToArray();
            Array.Sort(indexes);
            for (int i = 0; i < indexes.Length; i++)
            {
                if (index == indexes[i])
                    index++;
                else
                    return index;
            }
            return index;
        }

        private void IsContain (Item item) => Debug.LogWarning("Inventory already contain item ", item);
        private void IsNotContain (int id) => throw new Exception(string.Format("Inventory not contain item with ID:{0}", id));
        private void IsNotStackableType (int id) => Debug.LogWarning(string.Format("Item with ID:{0} is not {1} type", id, nameof(StackableItem)));
    }
}