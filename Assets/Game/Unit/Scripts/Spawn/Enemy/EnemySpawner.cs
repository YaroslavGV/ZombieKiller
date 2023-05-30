using UnityEngine;
using Zenject;
using Session;
using FractionSystem;

namespace Unit
{
    [RequireComponent(typeof(SpawnUnitLocation))]
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private UnitProfile[] _profiles;
        [SerializeField] private Fraction _fraction;
        [Space]
        [SerializeField] private float _spawnCount = 3;
        [Space]
        [SerializeField] private AIInputHandler _aiInput;
        private SpawnUnitLocation _location;

        [Inject] GameSession _session;
        
        private void Awake ()
        {
            _session.OnStart += Spawn;
        }

        public void Spawn ()
        {
            _location = GetComponent<SpawnUnitLocation>();
            for (int i = 0; i < _spawnCount; i++)
                SpawnUnit();
        }

        private void SpawnUnit ()
        {
            int profileIndex = Random.Range(0, _profiles.Length);
            UnitModel unit = _location.SpawnUnit(_profiles[profileIndex], _fraction);
            _aiInput.AddUnit(unit);
        }
    }
}