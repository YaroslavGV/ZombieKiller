using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Defective.JSON;

namespace InventorySystem
{
    [Serializable]
    public struct ItemPerformance
    {
        public int id;
        public string parameters;

        public ItemPerformance (int id, string parameters)
        {
            this.id = id;
            this.parameters = parameters;
        }

        public ItemPerformance (Item item) : this(item.ID, item.GetParameters())
        {
        }

        public override string ToString () => string.IsNullOrEmpty(parameters) ? id.ToString() : string.Format("{0} {1}", id, parameters);

        public Item GetItem (ItemsCollection dataBase)
        {
            Item newItem = dataBase.GetItem(id);
            if (newItem != null)
            {
                newItem.SetParameters(parameters);
                return newItem;
            }
            Debug.LogError("Unknown item id: "+id);
            return null;
        }

        public string ToJson ()
        {
            JSONObject jObj = new JSONObject();
            jObj.AddField(nameof(id), id);
            jObj.AddField(nameof(parameters), new JSONObject(parameters));
            return jObj.ToString();
        }

        public static ItemPerformance FromJson (string json) 
        {
            ItemPerformance performance = new ItemPerformance();
            JSONObject jObj = new JSONObject(json);
            string idName = nameof(performance.id);
            string parametersName = nameof(performance.parameters);

            if (jObj.HasField(idName))
                performance.id = jObj.GetField(idName).intValue;
            if (jObj.HasField(parametersName))
                performance.parameters = jObj.GetField(parametersName).ToString();
            return performance;
        }
    }

    [Serializable]
    public struct ItemsPerformance
    {
        public ItemPerformance[] items;

        public ItemsPerformance (IEnumerable<ItemPerformance> items) => this.items = items.ToArray();

        public ItemsPerformance (IEnumerable<Item> items) : this(items.Select(i => new ItemPerformance(i)))
        {
        }

        public override string ToString () => string.Join(", ", items.Select(i => i.ToString()));

        public IEnumerable<Item> GetItems (ItemsCollection dataBase)
        {
            List<Item> dItems = new List<Item>();
            foreach (var item in items)
            {
                Item newItem = item.GetItem(dataBase);
                if (newItem != null)
                    dItems.Add(newItem);
            }
            return dItems;
        }

        public string ToJson ()
        {
            JSONObject jObj = new JSONObject();
            JSONObject jItems = new JSONObject();
            foreach (ItemPerformance performance in items)
                jItems.Add(new JSONObject(performance.ToJson()));
            jObj.AddField(nameof(items), jItems);
            return jObj.ToString();
        }

        public static ItemsPerformance FromJson (string json)
        {
            ItemsPerformance performance = new ItemsPerformance();
            JSONObject jObj = new JSONObject(json);
            string itemsName = nameof(performance.items);
            if (jObj.HasField(itemsName))
            {
                JSONObject jItems = jObj.GetField(itemsName);
                if (jItems.isArray)
                {
                    List<ItemPerformance> items = new List<ItemPerformance>();
                    foreach (JSONObject obj in jItems)
                        items.Add(ItemPerformance.FromJson(obj.ToString()));
                    performance.items = items.ToArray();
                }
                else if (jItems.isNull)
                {
                    performance.items = new ItemPerformance[0];
                    Debug.LogWarning(string.Format("{0} field is null\n{1}", itemsName, json));
                }
                else
                    Debug.LogError(string.Format("{0} field is not Array\n{1}", itemsName, json));
            }
            else
                Debug.LogError(string.Format("field {0} is missing\n{1}", itemsName, json));
            return performance;
        }
    }

    [Serializable]
    public struct ItemsContainer : IItemsContainer
    {
        public Item[] items;

        public ItemsContainer (IEnumerable<Item> items) : this() => this.items = items.ToArray();

        public IEnumerable<Item> Items => items;

        public ItemsPerformance ItemsPerformance => new ItemsPerformance(items);

        public override string ToString () => string.Join(", ", items.Select(i => i.ToString()));
    }

    [Serializable]
    public struct ItemsContainer<T> : IItemsContainer where T : Item
    {
        public T[] items;

        public ItemsContainer (IEnumerable<T> items) : this() => this.items = items.ToArray();

        public IEnumerable<Item> Items => items;

        public ItemsPerformance ItemsPerformance => new ItemsPerformance(items);

        public override string ToString () => string.Join(", ", items.Select(i => i.ToString()));
    }

    [Serializable]
    public struct ItemAmount
    {
        public Item item;
        public int amount;

        public Item Item
        {
            get
            {
                Item newItem = item.IsInstance ? item.GetCopy() : item;
                if (newItem is StackableItem sItem)
                    sItem.Amount = amount;
                return newItem;
            }
        }
    }

    [Serializable]
    public struct ItemsAmount : IItemsContainer
    {
        public ItemAmount[] items;

        public IEnumerable<Item> Items => items.Select(i => i.Item);
    }
}