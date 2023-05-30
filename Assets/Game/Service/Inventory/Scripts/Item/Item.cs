using System;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        [NonSerialized] private bool _isInstance = true;
        public event Action<Item> OnConsume;
        public event Action<Item> OnChange;
        [SerializeField] private int _id = 0;
        [SerializeField] private string _name = "Unknown";
        [SerializeField] private Sprite _icon;

        public bool IsInstance => _isInstance;
        public int ID => _id;
        public virtual string Name => _name;
        public virtual Sprite Icon => _icon;

        public virtual string GetParameters () => "";
        public virtual void SetParameters (string json) { }

        public override string ToString ()
        {
            string parameters = GetParameters();
            if (string.IsNullOrEmpty(parameters))
                return string.Format("{0} {1}", _id, Name);
            else
                return string.Format("{0} {1} {2}", _id, Name, parameters);
        }

        public Item GetCopy ()
        {
            Item newItem = Instantiate(this);
            newItem.SetCopyFlag();
            return newItem;
        }

        public void SetCopyFlag () => _isInstance = false;

        protected void Consume () => OnConsume?.Invoke(this);
        protected void Change () => OnChange?.Invoke(this);
    }
}