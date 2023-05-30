using UnityEngine;
using InventorySystem;

namespace Collection
{
    public class CollectableItem : CollectableDrop
    {
        [SerializeField] private Item _item;
        [Header("Stack Amount")]
        [SerializeField] private int _min = 1;
        [SerializeField] private int _max = 1;

        public Item Сontent
        {
            get
            {
                Item item = _item.GetCopy();
                if (item is StackableItem sItem)
                    sItem.Amount = Random.Range(_min, _max + 1);
                return item;
            }
        }
    }
}