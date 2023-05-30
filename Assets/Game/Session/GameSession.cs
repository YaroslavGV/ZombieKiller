using System;
using System.Linq;
using UnityEngine;
using Zenject;
using FractionSystem;
using Unit;
using Collection;

namespace Session
{
    public class GameSession : MonoBehaviour
    {
        public event Action OnStart;
        public event Action OnCompleat;
        public event Action OnFail;
        [SerializeField] private Fraction _player;
        [SerializeField] private Fraction _enemy;
        [Inject] SceneUnits _units;
        [Inject] DropSpawner _drop;

        public bool Playing { get; private set; }

        private void Start ()
        {
            _units.OnRemoved += OnUnitRemove;
            StartSession();
        }

        public void StartSession ()
        {
            if (Playing)
            {
                Debug.Log("Session already started");
                return;
            }

            Playing = true;
            OnStart?.Invoke();
        }

        private void OnUnitRemove (UnitModel unit)
        {
            if (unit.IsAlive == false && unit.Profile is IDropTableContainer container)
                _drop.SpawnDrop(container, unit.transform.position);

            if (_units.GetUnits(_enemy).Count() == 0)
                LevelCompleat();
            if (_units.GetUnits(_player).Count() == 0)
                LevelFail();
        }

        private void LevelCompleat ()
        {
            if (Playing == false)
                return;

            Playing = false;
            OnCompleat?.Invoke();
        }
        private void LevelFail ()
        {
            if (Playing == false)
                return;

            Playing = false;
            OnFail?.Invoke();
        }
    }
}