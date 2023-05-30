using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class AIInputHandler : MonoBehaviour
    {
        [SerializeField] private SceneUnits _units;
        private List<UnitModel> _unit = new List<UnitModel>();

        private void Update ()
        {
            for (int i = _unit.Count - 1; i > -1; i--)
            {
                if (_unit[i].IsAlive == false)
                    _unit.RemoveAt(i);
                else
                    UpdateUnit(_unit[i]);
            }
        }

        public void AddUnit (UnitModel unit)
        {
            _unit.Add(unit);
        }

        public void UpdateUnit (UnitModel unit)
        {
            InputValues values = unit.Inputs;

            if (values.target == null)
            {
                if (values.isAttacking)
                {
                    values.isAttacking = false;
                    unit.Inputs = values;
                }
                return;
            }

            Vector2 targetVector = values.target.transform.position - unit.transform.position;
            float sqrDistance = targetVector.sqrMagnitude;
            float sqrAttackRange = values.attackRange * values.attackRange;

            bool isAttackRange = sqrDistance < sqrAttackRange;
            values.move = isAttackRange == false ? targetVector.normalized : Vector2.zero;
            values.isAttacking = isAttackRange;

            unit.Inputs = values;
        }
    }
}