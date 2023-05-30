using UnityEngine;

namespace Unit
{
    public struct InputValues
    {
        public bool notMovable;
        public Vector2 move;
        public Vector2 aimDirection;
        public UnitModel target;
        public float attackRange;
        public bool isAttacking;

        public InputValues (Vector2 move, Vector2 direction) : this()
        {
            this.move = move;
            this.aimDirection = direction;
        }
    }
}