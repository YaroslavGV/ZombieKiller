using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventorySystem
{
    public class InventoryView : MonoBehaviour
    {
        public event Action<ItemView> OnSelectView;
        [SerializeField] private ItemView _viewTemplate;
        [SerializeField] private Transform _itemsParent;

        [Inject] private Inventory _inventory;
        private List<ItemView> _views = new List<ItemView>();

        public Inventory Inventory => _inventory;
        public ItemView SelectionItem { get; private set; }

        private void OnEnable ()
        {
            UpdateItems();
            _inventory.OnAdd += AddItem;
            _inventory.OnRemove += RemoveItem;
        }

        private void OnDisable ()
        {
            ClearItems();
            _inventory.OnAdd -= AddItem;
            _inventory.OnRemove -= RemoveItem;
        }

        private void UpdateItems ()
        {
            ClearItems();
            foreach (Item item in _inventory.Items)
                AddItem(item);
        }

        private void ClearItems ()
        {
            for (int i = _views.Count-1; i > -1; i--)
                RemoveItem(_views[i]);
            _views.Clear();
        }

        private void AddItem (Item item)
        {
            ItemView newView = Instantiate(_viewTemplate, _itemsParent);
            newView.SeItem(item);
            newView.OnSelectView += OnItemSelected;
            _views.Add(newView);
        }

        private void OnItemSelected (ItemView view)
        {
            if (SelectionItem != null)
                SelectionItem.DeselectItem();
            SelectionItem = view;
            OnSelectView?.Invoke(view);
        }

        private void RemoveItem (Item item)
        {
            foreach (ItemView view in _views)
                if (view.Item == item)
                {
                    RemoveItem(view);
                    break;
                }
        }

        private void RemoveItem (ItemView view)
        {
            if (SelectionItem == view)
                OnItemSelected(null);
            view.OnSelectView -= OnItemSelected;
            _views.Remove(view);
            Destroy(view.gameObject);
        }
    }
}