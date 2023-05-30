using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(ObjectTrackerUI))]
    public class TargetView : MonoBehaviour
    {
        [SerializeField] private Key _skinPoint;
        [SerializeField] private GameObject _view;
        private UnitModel _unit;
        private UnitModel _currentTarget;
        private ObjectTrackerUI _track;

        private void Awake ()
        {
            _view.SetActive(false);
        }

        public void SetUnit (UnitModel unit)
        {
            _track = GetComponent<ObjectTrackerUI>();
            _unit = unit;
        }

        private void Update ()
        {
            if (_unit == null)
                return;

            UnitModel target = _unit.Inputs.target;
            if (target != null)
            {
                if (_currentTarget != target)
                {
                    _currentTarget = target;
                    Transform followTarget = _currentTarget.Skin.GetPoint(_skinPoint.Name);
                    _track.SetTarget(followTarget);
                    _view.SetActive(true);
                }
            }
            else if (_currentTarget != null)
            {
                _currentTarget = null;
                _track.Stop();
                _view.SetActive(false);
            }
        }
    }
}