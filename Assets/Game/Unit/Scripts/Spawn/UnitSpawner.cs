using UnityEngine;
using FractionSystem;

namespace Unit
{
    [RequireComponent(typeof(SpawnUnitLocation))]
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] private UnitProfile _profile;
        [SerializeField] private Fraction _fraction;
        
        public UnitModel Spawn ()
        {
            SpawnUnitLocation location = GetComponent<SpawnUnitLocation>();
            UnitModel unit = location.SpawnUnit(_profile, _fraction);
            return unit;
        }
    }
}