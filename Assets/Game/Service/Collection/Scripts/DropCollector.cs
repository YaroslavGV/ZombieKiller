using System;
using UnityEngine;
using Zenject;
using Unit;
using StatSystem;
using InventorySystem;

namespace Collection
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class DropCollector : MonoBehaviour, IDropCollector
    {
        [SerializeField] private UnitModel _unit;
        [Inject] private Inventory _inventory;
        private IDropCollector[] _collectors;
        private Stat _radius;
        private CircleCollider2D _collider;

        private void Start ()
        {
            _collider = GetComponent<CircleCollider2D>();
            _radius = _unit.Stats.GetStat(StatType.CollectRadius);
            _radius.Changed += UpdateRadius;
            UpdateRadius();
            
            _collectors = new IDropCollector[]
            {
                new CollectorHealth(_unit),
                new CollectorItem(_inventory)
            };
        }

        private void UpdateRadius ()
        {
            if (_radius.value != 0)
                _collider.radius = _radius.value;
        }

        private void OnTriggerEnter2D (Collider2D collision)
        {
            if (collision.TryGetComponent(out ICollectobleDrop target) && TryCollect(target))
                target.PickUp();
        }

        public bool TryCollect (ICollectobleDrop target)
        {
            foreach (IDropCollector collector in _collectors)
                if (collector.TryCollect(target))
                    return true;
            Debug.LogWarning("Unidentified item type " + target.GetType());
            return false;
        }
    }
}