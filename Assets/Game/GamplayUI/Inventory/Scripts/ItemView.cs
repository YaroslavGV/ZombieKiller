using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace InventorySystem
{
    public class ItemView : Selectable
    {
        public event Action<ItemView> OnSelectView;
        [Space]
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _stack;
        [SerializeField] private TextMeshProUGUI _stackAmount;
        [SerializeField] private Graphic[] _colorTargets;
        private Item _item;
        private StackableItem _sItem;

        public Item Item => _item;

        private void OnDestroy ()
        {
            if (_sItem != null)
                _sItem.OnChange -= UpdateAmount;
        }

        public void SeItem (Item item)
        {
            if (_item == item)
                return;

            _item = item;
            _icon.sprite = _item.Icon;

            _sItem = _item is StackableItem sItem ? sItem : null;
            _stack.SetActive(_sItem != null);
            if (_sItem != null)
            {
                _sItem.OnChange += UpdateAmount;
                UpdateAmount(null);
            }
            name = "ItemView +"+item.Name;
        }

        // block auto deselect
        public override void OnDeselect (BaseEventData eventData)
        {
        }

        public override void OnSelect (BaseEventData eventData)
        {
            base.OnSelect(eventData);
            OnSelectView?.Invoke(this);
        }

        public void DeselectItem ()
        {
            base.OnDeselect(null);
        }

        private void UpdateAmount (Item item)
        {
            if (_sItem != null)
            {
                int count = _sItem.Amount;
                _stack.SetActive(count > 1);
                _stackAmount.text = count.ToString();
            }
        }

        protected override void DoStateTransition (SelectionState state, bool instant)
        {
            var targetColor =
                state == SelectionState.Disabled ? colors.disabledColor :
                state == SelectionState.Highlighted ? colors.highlightedColor :
                state == SelectionState.Normal ? colors.normalColor :
                state == SelectionState.Pressed ? colors.pressedColor :
                state == SelectionState.Selected ? colors.selectedColor : Color.white;

            foreach (var graphic in _colorTargets)
                graphic.CrossFadeColor(targetColor, instant ? 0f : colors.fadeDuration, true, true);
        }
    }
}
