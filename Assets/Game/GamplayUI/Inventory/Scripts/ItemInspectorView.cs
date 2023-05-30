using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Zenject;
using TMPro;

namespace InventorySystem
{
    public class ItemInspectorView : MonoBehaviour
    {
        [SerializeField] private InventoryView _inventoryView;
        [Space]
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Button _equipeButton;
        [SerializeField] private Button _destroyButton;
        [Space]
        [SerializeField] private AmountChoiseView _amount;

        [Inject] private Equipment _equipment;
        
        private ItemView _item;
        private UnityAction _equipeClick, _destroyClick;

        private void OnEnable ()
        {
            _inventoryView.OnSelectView += OnSelectView;
            OnSelectView(_inventoryView.SelectionItem);

            _equipeClick += EquipmeItem;
            _destroyClick += DestroyItem;
            _equipeButton.onClick.AddListener(_equipeClick);
            _destroyButton.onClick.AddListener(_destroyClick);
        }

        private void OnDisable ()
        {
            _inventoryView.OnSelectView -= OnSelectView;
            _equipeButton.onClick.RemoveListener(_equipeClick);
            _destroyButton.onClick.RemoveListener(_destroyClick);
            _equipeClick -= EquipmeItem;
            _destroyClick -= DestroyItem;
        }

        private void OnSelectView (ItemView item)
        {
            _item = item;
            UpdateView();
        }

        private void UpdateView ()
        {
            SetActive(_item != null);
            if (_item == null)
                return;
            
            Item item = _item.Item;
            _name.text = item.Name;

            EquipmentItem eItem = item as EquipmentItem;
            _equipeButton.gameObject.SetActive(eItem != null);
        }

        private void SetActive (bool value)
        {
            _name.gameObject.SetActive(value);
            _equipeButton.gameObject.SetActive(value);
            _destroyButton.gameObject.SetActive(value);
        }

        private void EquipmeItem ()
        {
            if (_item == null)
                return;
            Item item = _item.Item;
            if (item is EquipmentItem eItem)
            {
                Inventory inventory = _inventoryView.Inventory;
                EquipmentItem removedItem = _equipment.Equipe(eItem);
                inventory.RemoveItem(eItem);
                if (removedItem != null)
                    inventory.AddItem(removedItem);
            }
        }

        private void DestroyItem ()
        {
            if (_item == null)
                return;
            Item item = _item.Item;
            if (item is StackableItem sItem)
            {
                _amount.Choise(item.Name, sItem.Amount, DestroyStack);
            }
            else
            {
                Inventory inventory = _inventoryView.Inventory;
                inventory.RemoveItem(item);
            }
        }

        private void DestroyStack (int amount)
        {
            if (_item == null)
                return;
            Item item = _item.Item;
            if (item is StackableItem sItem)
                sItem.Amount -= amount;
        }
    }
}