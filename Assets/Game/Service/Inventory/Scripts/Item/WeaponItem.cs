using UnityEngine;
using Weapon;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "WeaponItem", menuName = "Inventory/WeaponItem")]
    public class WeaponItem : EquipmentItem
    {
        [SerializeField] private WeaponModel _model;

        public WeaponModel Model => _model;
    }
}