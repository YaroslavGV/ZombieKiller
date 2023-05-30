using UnityEngine;

namespace InventorySystem
{
    public class InventorySystemTest : MonoBehaviour
    {
        [SerializeField] private Item _item;
        [SerializeField] private Item[] _items;
        [SerializeField] private ItemsContainer<EquipmentItem> _equipmentItems;
        [Space]
        [SerializeField] private ItemsCollection _dataBase;

        private void Start ()
        {
            TestAll();
        }

        [ContextMenu("Test All")]
        private void TestAll ()
        {
            ItemParametersTest();
            ItemPerformanceTest();
            ItemsPerformanceTest();
            InventorySaveLoadTest();
            EquipmentSaveLoadTest();
        }

        [ContextMenu("Test Item Parameters")]
        private void ItemParametersTest ()
        {
            if (_item == null)
            {
                Debug.LogError("Require Item");
                return;
            }

            Item itemA = _item.GetCopy();
            string jsonA = itemA.GetParameters();
            Item itemB = _item.GetCopy();
            itemB.SetParameters(jsonA);
            string jsonB = itemB.GetParameters();
            DestroyImmediate(itemA);
            DestroyImmediate(itemB);
            Debug.Log(string.Format("ItemPerformance Test. IsSame:{0}\n{1}\n{2}", jsonA == jsonB, jsonA, jsonB));
        }

        [ContextMenu("Test ItemPerformance")]
        private void ItemPerformanceTest ()
        {
            if (_item == null)
            {
                Debug.LogError("Require Item");
                return;
            }

            ItemPerformance performanceA = new ItemPerformance(_item);
            string jsonA = performanceA.ToJson();
            ItemPerformance performanceB = ItemPerformance.FromJson(jsonA);
            string jsonB = performanceB.ToJson();
            Debug.Log(string.Format("ItemPerformance Test. IsSame:{0}\n{1}\n{2}", jsonA == jsonB, jsonA, jsonB));
        }

        [ContextMenu("Test ItemsPerformance")]
        private void ItemsPerformanceTest ()
        {
            if (_items == null || _items.Length == 0)
            {
                Debug.LogError("Require Items");
                return;
            }

            ItemsPerformance performanceA = new ItemsPerformance(_items);
            string jsonA = performanceA.ToJson();
            ItemsPerformance performanceB = ItemsPerformance.FromJson(jsonA);
            string jsonB = performanceB.ToJson();
            Debug.Log(string.Format("ItemsPerformance Test. IsSame:{0}\n{1}\n{2}", jsonA == jsonB, jsonA, jsonB));
        }

        [ContextMenu("Test InventorySaveLoad")]
        private void InventorySaveLoadTest ()
        {
            if (_items == null || _items.Length == 0)
            {
                Debug.LogError("Require Items");
                return;
            }
            if (_dataBase == null)
            {
                Debug.LogError("Require DataBase");
                return;
            }

            InventorySaveLoad inventoryA = new InventorySaveLoad("", null);
            foreach (Item item in _items)
                inventoryA.AddItem(item);
            string jsonA = inventoryA.GetJson();
            InventorySaveLoad inventoryB = new InventorySaveLoad("", null);
            inventoryB.SetDataBase(_dataBase);
            inventoryB.SetJson(jsonA);
            string jsonB = inventoryA.GetJson();
            Debug.Log(string.Format("EquipmentSaveLoad Test. IsSame:{0}\n{1}\n{2}", jsonA == jsonB, jsonA, jsonB));
        }

        [ContextMenu("Test EquipmentSaveLoad")]
        private void EquipmentSaveLoadTest ()
        {
            if (_equipmentItems.items == null || _equipmentItems.items.Length == 0)
            {
                Debug.LogError("Require Items");
                return;
            }
            if (_dataBase == null)
            {
                Debug.LogError("Require DataBase");
                return;
            }

            EquipmentSaveLoad equipmentA = new EquipmentSaveLoad("", new ItemsContainer<EquipmentItem>());
            foreach (EquipmentItem item in _equipmentItems.Items)
                equipmentA.Equipe(item);
            string jsonA = equipmentA.GetJson();
            EquipmentSaveLoad equipmentB = new EquipmentSaveLoad("", new ItemsContainer<EquipmentItem>());
            equipmentB.SetDataBase(_dataBase);
            equipmentB.SetJson(jsonA);
            string jsonB = equipmentA.GetJson();
            Debug.Log(string.Format("EquipmentSaveLoad Test. IsSame:{0}\n{1}\n{2}", jsonA == jsonB, jsonA, jsonB));
        }
    }
}