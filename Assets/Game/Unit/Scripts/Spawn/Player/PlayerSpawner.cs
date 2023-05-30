using UnityEngine;
using Zenject;
using Session;
using FractionSystem;

namespace Unit
{
    [RequireComponent(typeof(SpawnUnitLocation))]
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Fraction _fraction;
        [Space]
        [SerializeField] private PlayerInputHandler _playerInput;
        [SerializeField] private UnitTracker _cameraTemplate;
        [SerializeField] private RadiusView _radiusTemplate;
        [SerializeField] private TargetView _target;
        [SerializeField] private AmmoView _ammo;

        [Inject] GameSession _session;
        [Inject] PlayerProfile _profile;

        private void Awake ()
        {
            _session.OnStart += Spawn;
        }

        public void Spawn ()
        {
            SpawnUnitLocation location = GetComponent<SpawnUnitLocation>();
            UnitModel player = location.SpawnUnit(_profile, _fraction);
            _playerInput.SetPlayer(player);
            Transform parent = transform;
            if (transform.parent != null)
                parent = transform.parent;

            if (_cameraTemplate != null)
            {
                UnitTracker tracker = Instantiate(_cameraTemplate, parent);
                tracker.SetTarget(player);
            }
            if (_radiusTemplate != null)
            {
                RadiusView radius = Instantiate(_radiusTemplate, parent);
                radius.SetTarget(player);
            }
            if (_target != null)
                _target.SetUnit(player);
            if (_ammo != null)
                _ammo.SetUnit(player);
        }
    }
}