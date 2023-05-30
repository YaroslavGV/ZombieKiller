using UnityEngine;
using FractionSystem;
using PointGenerator;

namespace Unit
{
    public class SpawnUnitLocation : MonoBehaviour
    {
        [Tooltip("Use self position if location is null")]
        [SerializeField] private PointLocation _location;
        [SerializeField] private SceneUnits _units;

        public UnitModel SpawnUnit (IUnitProfile profile, Fraction fraction) 
        {
            UnitModel unit = _units.SpawnUnit(profile, fraction, GetPoint());
            return unit;
        }

        private Vector3 GetPoint () => _location != null ? _location.GetPoint() : transform.position;
    }
}