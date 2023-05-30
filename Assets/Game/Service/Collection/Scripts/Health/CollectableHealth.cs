using UnityEngine;

namespace Collection
{
    public class CollectableHealth : CollectableDrop
    {
        [SerializeField] private int _amount = 10;

        public int Amount => _amount;
    }
}