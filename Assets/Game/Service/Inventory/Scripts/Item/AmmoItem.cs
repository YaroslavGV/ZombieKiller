using UnityEngine;
using Weapon;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/AmmoItem")]
    public class AmmoItem : StackableItem
    {
        [SerializeField] private AmmoType _type;

        public AmmoType Type => _type;
    }
}