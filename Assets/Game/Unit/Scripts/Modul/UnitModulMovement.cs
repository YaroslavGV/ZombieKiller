using UnityEngine;
using StatSystem;

namespace Unit
{
    public class UnitModulMovement : UnitModul
    {
        private Rigidbody2D _body;
        private Stat _moveSpeed;
        
        public override void Tick ()
        {
            Vector2 move = Unit.Inputs.move;
            if (Unit.Inputs.notMovable == false)
                _body.velocity = move * _moveSpeed.value;
            else
                _body.velocity = Vector2.zero;
        }

        protected override void OnInitialize ()
        {
            _body = Unit.Body;
            _moveSpeed = Unit.Stats.GetStat(StatType.MovementSpeed);
        }
    }
}