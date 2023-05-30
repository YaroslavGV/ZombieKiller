using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using FractionSystem;
using Zenject;

namespace Unit
{
    public class SceneUnits : MonoBehaviour
    {
        public Action<UnitModel> OnAdd;
        public Action<UnitModel> OnRemoved;
        [SerializeField] private UnitModel _defaultTemplate;
        private List<UnitModel> _units = new List<UnitModel>();
        private int _counter;
        [Inject] private DiContainer _container;

        public IEnumerable<UnitModel> Units => _units;

        public void AddUnit (UnitModel unit)
        {
            if (_units.Contains(unit))
                return;
            _units.Add(unit);
            unit.OnDead += OnUnitDead;
            OnAdd?.Invoke(unit);
        }

        public void RemoveUnit (UnitModel unit)
        {
            if (_units.Remove(unit))
            {
                unit.OnDead -= OnUnitDead;
                OnRemoved?.Invoke(unit);
            }
        }

        public IEnumerable<UnitModel> GetUnits (Fraction fraction)
        {
            List<UnitModel> units = new List<UnitModel>();
            foreach (UnitModel target in _units)
                if (target.Fraction.IsEquals(fraction))
                    units.Add(target);
            return units;
        }

        public IEnumerable<UnitModel> GetEnemysFor (UnitModel unit)
        {
            List<UnitModel> units = new List<UnitModel>();
            foreach (UnitModel target in _units)
                if (unit.CanAttack(target))
                    units.Add(target);
            return units;
        }

        public UnitModel GetCloseEnemyFor (UnitModel unit)
        {
            UnitModel[] enemys = GetEnemysFor(unit).ToArray();
            if (enemys.Length == 0)
                return null;
            int closeIndex = -1;
            float closeDistance = float.PositiveInfinity;
            for (int i = 0; i < enemys.Length; i++)
            {
                float distance = (enemys[i].transform.position - unit.transform.position).sqrMagnitude;
                if (closeDistance > distance)
                {
                    closeDistance = distance;
                    closeIndex = i;
                }
            }
            return enemys[closeIndex];
        }

        private void OnUnitDead ()
        {
            foreach (UnitModel unit in _units)
                if (unit.IsAlive == false)
                {
                    RemoveUnit(unit);
                    break;
                }
        }

        public UnitModel SpawnUnit (IUnitProfile profile, Fraction fraction, Vector3 position)
        {
            UnitModel template = profile.ModelTemplate != null ? profile.ModelTemplate : _defaultTemplate;
            UnitModel unit = _container.InstantiatePrefabForComponent<UnitModel>(template, position, Quaternion.identity, transform);
            unit.gameObject.name = string.Format("{0} {1}", profile.Name, _counter.ToString("D3"));
            unit.Initialize(profile, fraction);
            AddUnit(unit);
            _counter++;
            return unit;
        }
    }
}