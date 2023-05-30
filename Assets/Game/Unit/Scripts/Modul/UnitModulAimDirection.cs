using UnityEngine;

namespace Unit
{
    public class UnitModulAimDirection : UnitModul
    {
        [SerializeField] private Key _aimFrom;
        [SerializeField] private Key _aimTo;
        private Transform _from;
        private Transform _to;
        private UnitModel _currentTarget;

        public override void Tick ()
        {
            InputValues values = Unit.Inputs;
            Vector2 direction = Vector2.zero;
            if (values.target != null)
            {
                if (_currentTarget != values.target)
                {
                    _currentTarget = values.target;
                    _to = _currentTarget.Skin.GetPoint(_aimTo.Name);
                }
                direction = (_to.position - _from.position).normalized;
            } 
            else if (values.move != Vector2.zero)
            {
                direction = values.move.normalized;
            }
            if (direction == Vector2.zero)
                return;

            values.aimDirection = direction;
            Unit.Inputs = values;
        }

        protected override void OnInitialize ()
        {
            _from = Unit.Skin.GetPoint(_aimFrom.Name);
        }
    }
}