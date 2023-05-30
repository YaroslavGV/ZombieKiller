using UnityEngine;

namespace Weapon
{
    public abstract class BasicWeaponModel : WeaponModel
    {
        [SerializeField] private float _attackSpeed = 1;
        private bool _isAttacking;
        private float _attackCooldown;

        public override bool IsAttacking => _isAttacking;

        public override void SetAttack (bool isAttacking) => _isAttacking = isAttacking;

        public override void Update ()
        {
            if (_isAttacking || _attackCooldown > 0)
            {
                _attackCooldown -= Time.deltaTime;
                if (_attackCooldown <= 0)
                {
                    if (_isAttacking)
                    {
                        _attackCooldown += _attackSpeed;
                        ReadyToAttack();
                    }
                    else
                    {
                        _attackCooldown = 0;
                    }
                }
            }
        }

        private void ReadyToAttack ()
        {
            if (string.IsNullOrEmpty(SkinAnimationTrigger))
                DoAttack();
            else
                RequireAnimation();
        }
    }
}