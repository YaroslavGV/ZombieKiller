using UnityEngine;

namespace Weapon
{
    public class MeeleeWeaponModel : BasicWeaponModel
    {
        [SerializeField] private Vector2 _hitBoxSize = new Vector2(2, 2);
        private IAimDirection _aimDirection;
        private IItemViewHandler _viewHandler;

        public override WeaponView View => null;

        public override void Initlialize (IAimDirection aimDirection, IItemViewHandler viewHandler)
        {
            _aimDirection = aimDirection;
            _viewHandler = viewHandler;
        }

        public override void Remove ()
        { 
        }

        public override void DoAttack ()
        {
            Vector3 point = _viewHandler.GetWeaponPoint().position;
            Vector2 direction = _aimDirection.GetAimDirection();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            RaycastHit2D[] hits = Physics2D.BoxCastAll(point, _hitBoxSize, angle, direction);
            foreach (RaycastHit2D hit in hits)
            {
                var target = hit.collider.GetComponentInParent<IDamageable>();
                if (target != null && Sorce.CanAttack(target))
                    Sorce.DoDamage(target, this);
            }
        }
    }
}