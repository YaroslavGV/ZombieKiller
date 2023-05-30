using System.Linq;
using UnityEngine;
using Zenject;
using SaveLoad;

namespace InventorySystem
{
    public class EquipmentSaveLoad : Equipment, IJsonHandle
    {
        private readonly string _key;
        private readonly ItemsContainer<EquipmentItem> _defaultItems;
        private ItemsCollection _dataBase;
        
        public EquipmentSaveLoad (string key, ItemsContainer<EquipmentItem> defaultItems)
        {
            _key = key;
            _defaultItems = defaultItems;
        }

        public string Key => _key;

        public string GetJson () => new ItemsPerformance(Items).ToJson();

        [Inject]
        public void SetDataBase (ItemsCollection dataBase) => _dataBase = dataBase;

        public void SetJson (string json)
        {
            if (string.IsNullOrEmpty(json))
                return;

            ItemsPerformance performance = ItemsPerformance.FromJson(json);
            Item[] items = performance.GetItems(_dataBase).ToArray();
            foreach (Item item in performance.GetItems(_dataBase))
                if (item is EquipmentItem eItem)
                    QuietEquipe(eItem);
                else
                    Debug.LogError(string.Format("Item is not {0} {1}", nameof(EquipmentItem), item));
        }

        public void SetDefaul ()
        {
            if (_defaultItems.items != null)
                foreach (EquipmentItem item in _defaultItems.items)
                    QuietEquipe(item);
        }
    }
}