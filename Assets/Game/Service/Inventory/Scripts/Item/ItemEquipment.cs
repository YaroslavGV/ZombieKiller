using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/EquipmentItem")]
    public class EquipmentItem : Item
    {
        [SerializeField] private Key _slot;

        public virtual Key Slot => _slot;
    }
}