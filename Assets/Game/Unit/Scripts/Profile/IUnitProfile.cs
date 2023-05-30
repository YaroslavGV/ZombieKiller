using StatSystem;
using InventorySystem;
using Weapon;

namespace Unit
{
    using Skin;

    public interface IUnitProfile
    {
        public string Name { get; }
        public StatValues BaseStats { get; }
        public UnitSkin Skin { get; }
        public Equipment Equipment { get; }
        public IAmmoBackpack AmmoBackpack { get; }

        public UnitModel ModelTemplate { get; }
    }
}