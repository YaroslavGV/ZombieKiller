using System;

namespace Weapon
{
    public interface IAmmoBackpack
    {
        event Action<AmmoType, int> Changed;

        public bool InfinityAmmo { get; set; }

        int GetAmount (AmmoType type);
        public void Spend (AmmoType type, int amount = 1);
        public void Add (AmmoType type, int amount);
    }
}