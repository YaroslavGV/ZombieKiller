using UnityEngine;
using StatSystem;
using InventorySystem;
using Collection;

namespace Unit
{
    using Skin;
    using Weapon;

    [CreateAssetMenu(fileName = "UnitProfile", menuName = "Unit/UnitProfile")]
    public class UnitProfile : ScriptableObject, IUnitProfile, IDropTableContainer
    {
        [SerializeField] private string _name;
        [SerializeField] private StatValues _baseStats;
        [SerializeField] private UnitSkin _skin;
        [SerializeField] private ItemsContainer<EquipmentItem> _equipment;
        [SerializeField] private bool _infinityAmmo;
        [SerializeField] private AmmoValue[] _ammoAmount;
        [Space]
        [SerializeField] private UnitModel _modelTemplate;
        [Space]
        [SerializeField] private DropTable _dropTable;

        public string Name => _name;
        public StatValues BaseStats => _baseStats;
        public UnitSkin Skin => _skin;
        public Equipment Equipment => CreateEquipment();
        public IAmmoBackpack AmmoBackpack => CreateAmmoBackpack();
        public UnitModel ModelTemplate => _modelTemplate;
        public DropTable DropTable => _dropTable;

        private Equipment CreateEquipment ()
        {
            Equipment equipment = new Equipment();
            foreach (EquipmentItem item in _equipment.items)
                equipment.Equipe(item);
            return equipment;
        }

        private IAmmoBackpack CreateAmmoBackpack ()
        {
            AmmoBackpack backpack = new AmmoBackpack();
            backpack.InfinityAmmo = _infinityAmmo;
            foreach (AmmoValue ammo in _ammoAmount)
                backpack.Add(ammo.type, ammo.amount);
            return backpack;
        }
    }
}