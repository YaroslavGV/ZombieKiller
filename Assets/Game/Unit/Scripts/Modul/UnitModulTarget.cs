using UnityEngine;
using Zenject;
using StatSystem;

namespace Unit
{
    public class UnitModulTarget : UnitModul
    {
        [SerializeField] private bool _lostTargetOutRange = false;
        [Inject] private SceneUnits _units;
        private Stat _viewRadius;

        public override void Tick ()
        {
            if (Unit.Inputs.target != null)
            {
                if (_lostTargetOutRange && ValidRange(Unit.Inputs.target) == false)
                    SetTaret(null);
                else if (Unit.Inputs.target.IsAlive == false)
                    SetTaret(null);
            }
            else
            {
                UnitModel target = _units.GetCloseEnemyFor(Unit);
                if (target != null && ValidRange(target))
                    SetTaret(target);
            }
        }

        protected override void OnInitialize ()
        {
            _viewRadius = Unit.Stats.GetStat(StatType.ViewRadius);
        }

        private bool ValidRange (UnitModel target)
        {
            float distance = (target.transform.position - Unit.transform.position).sqrMagnitude;
            float requareRange = _viewRadius.value * _viewRadius.value;
            return distance < requareRange;
        }

        private void SetTaret (UnitModel target)
        {
            InputValues values = Unit.Inputs;
            values.target = target;
            Unit.Inputs = values;
        }
    }
}