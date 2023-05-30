using Weapon;

namespace Collection
{
    public class CollectorAmmo : IDropCollector
    {
        private readonly IAmmoBackpack _backpack;

        public CollectorAmmo (IAmmoBackpack backpack)
        {
            _backpack = backpack;
        }

        public bool TryCollect (ICollectobleDrop target)
        {
            if (target is CollectableAmmo ammoTarget)
            {
                _backpack.Add(ammoTarget.Ammo.type, ammoTarget.Ammo.amount);
                return true;
            }
            return false;
        }
    }
}