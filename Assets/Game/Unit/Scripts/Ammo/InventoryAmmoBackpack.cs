using System;
using System.Collections.Generic;
using InventorySystem;

namespace Weapon
{
    public class InventoryAmmoBackpack : IAmmoBackpack
    {
        public event Action<AmmoType, int> Changed;
        private readonly Inventory _inventory;
        private readonly Dictionary<AmmoType, Item> _ammoDictionary;

        public bool InfinityAmmo { get; set; }

        public InventoryAmmoBackpack (Inventory inventory, ItemsCollection database)
        {
            _inventory = inventory;
            _inventory.OnChange += OnChange;

            AmmoItem[] ammoItems = database.GetItems<AmmoItem>();
            _ammoDictionary = new Dictionary<AmmoType, Item>();
            foreach (AmmoItem item in ammoItems)
                _ammoDictionary.Add(item.Type, item);
        }

        public int GetAmount (AmmoType type) => _inventory.GetStackAmount(GetID(type));

        public void Add (AmmoType type, int amount)
        {
            if (amount < 0)
                AmountIsNegative();
            int id = GetID(type);
            if (_inventory.GetItem(id) == null)
                _inventory.AddItem(GetItem(type, amount));
            else
                _inventory.ChangeStackAmount(GetID(type), amount);
        }

        public void Spend (AmmoType type, int amount = 1)
        {
            if (amount < 0)
                AmountIsNegative();
            _inventory.ChangeStackAmount(GetID(type), -amount);
        }

        private void OnChange (Item item)
        {
            if (item is AmmoItem aItem)
                Changed.Invoke(aItem.Type, aItem.Amount);
        }

        private int GetID (AmmoType type) => _ammoDictionary[type].ID;
        private AmmoItem GetItem (AmmoType type, int amount)
        {
            AmmoItem item = _ammoDictionary[type].GetCopy() as AmmoItem;
            item.Amount = amount;
            return item;
        }

        private void AmountIsNegative () => throw new ArgumentException(string.Format("Amount must be positive value"));
    }
}