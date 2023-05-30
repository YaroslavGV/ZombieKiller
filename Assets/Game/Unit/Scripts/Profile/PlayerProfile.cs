using UnityEngine;
using Zenject;
using StatSystem;
using InventorySystem;
using Weapon;


namespace Unit
{
    using Skin;

    [CreateAssetMenu(fileName = "PlayerProfile", menuName = "Unit/PlayerProfile")]
    public class PlayerProfile : ScriptableObject, IUnitProfile
    {
        [SerializeField] private string _name;
        [SerializeField] private StatValues _baseStats;
        [SerializeField] private UnitSkin _skin;
        [SerializeField] private bool _infinityAmmo;
        [Space]
        [SerializeField] private UnitModel _modelTemplate;

        [Inject] private Inventory _inventory;
        [Inject] private Equipment _equipment;
        [Inject] private ItemsCollection _itemDataBase;

        public string Name => _name;
        public StatValues BaseStats => _baseStats;
        public UnitSkin Skin => _skin;
        public Equipment Equipment => _equipment;
        public IAmmoBackpack AmmoBackpack => CreateAmmoBackpack();
        public UnitModel ModelTemplate => _modelTemplate;

        private IAmmoBackpack CreateAmmoBackpack ()
        {
            InventoryAmmoBackpack backpack = new InventoryAmmoBackpack(_inventory, _itemDataBase);
            backpack.InfinityAmmo = _infinityAmmo;
            return backpack;
        }
    }
}