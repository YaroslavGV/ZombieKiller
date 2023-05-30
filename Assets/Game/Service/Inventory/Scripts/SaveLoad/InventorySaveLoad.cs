using Zenject;
using Defective.JSON;
using SaveLoad;

namespace InventorySystem
{
    public class InventorySaveLoad : Inventory, IJsonHandle
    {
        private readonly string _key;
        private readonly IItemsContainer _defaultItems;
        private ItemsCollection _dataBase;

        public InventorySaveLoad (string key, IItemsContainer defaultItems)
        {
            _key = key;
            _defaultItems = defaultItems;
        }

        public string Key => _key;

        [Inject]
        public void SetDataBase (ItemsCollection dataBase) => _dataBase = dataBase;

        public string GetJson ()
        {
            JSONObject jObj = new JSONObject();
            foreach (var slot in slots)
                jObj.AddField(slot.Key.ToString(), new JSONObject(new ItemPerformance(slot.Value).ToJson()));
            return jObj.ToString();
        }

        public void SetJson (string json)
        {
            if (string.IsNullOrEmpty(json))
                return;
            
            JSONObject jObj = new JSONObject(json);
            foreach (string key in jObj.keys)
            {
                if (int.TryParse(key, out int slotIndex))
                {
                    JSONObject jFile = jObj.GetField(key);
                    if (jFile.isObject)
                    {
                        ItemPerformance performance = ItemPerformance.FromJson(jFile.ToString());
                        Item item = _dataBase.GetItem(performance.id);
                        if (item != null)
                        {
                            item.SetParameters(performance.parameters);
                            QuietAddItem(item, slotIndex);
                        }
                    }
                    else
                        UnityEngine.Debug.Log(string.Format("Field \"{0}\" value is not JsjonObject\n{1}", key, jObj.ToString()));
                }
                else
                    UnityEngine.Debug.Log(string.Format("Field \"{0}\" key is not int\n{1}", key, jObj.ToString()));
            }
        }

        public void SetDefaul ()
        {
            if (_defaultItems != null)
                foreach (Item item in _defaultItems.Items)
                    QuietAddItem(item, -1);
        }
    }
}