using UnityEngine;
using Weapon;

namespace Collection
{
    public class CollectableAmmo : CollectableDrop
    {
        [SerializeField] private AmmoValue _ammo;

        public AmmoValue Ammo => _ammo;
    }
}