using UnityEngine;

namespace Weapon
{
    public abstract class CastBullet : Bullet
    {
        [Space]
        [SerializeField] private LayerMask _mask;
        private LifeDistance _life;

        protected LifeDistance Life => _life;
        protected LayerMask Mask => _mask;
        protected Ray2D Ray => new Ray2D(transform.position, Direction);

        private void Awake ()
        {
            _life = new LifeDistance();
            _life.LifeOver += Exhaust;
        }

        private void OnDestroy ()
        {
            _life.LifeOver -= Exhaust;
        }

        private void Update ()
        {
            if (IsRunning)
            {
                float distance = Settings.speed*Time.deltaTime;
                distance = Mathf.Min(distance, _life.Remainder);
                HandleRay(distance);
            }
        }

        public override void Fire (ShotData data, IDamageSorce sorce = null, IDamageMediator mediator = null)
        {
            base.Fire(data, sorce, mediator);
            _life.SetDistance(Settings.maxDistance);
            _life.Reset();
        }

        private void HandleRay (float deltaDistance)
        {
            RaycastHit2D[] hitTargets = GetHits(deltaDistance);
            foreach (RaycastHit2D hitTarget in hitTargets)
            {
                transform.position = hitTarget.point;
                Collider2D collider = hitTarget.collider;
                IDamageable damagebleTarget = GetDamageTarget(collider);
                if (damagebleTarget != null)
                {
                    bool isFinished = HandleTargetCollision(damagebleTarget, collider, hitTarget);
                    if (isFinished)
                    {
                        Exhaust();
                        return;
                    }
                }
                else
                {
                    HitEnvironment(hitTarget, false);
                    return;
                }
            }
            transform.position += (Vector3)Direction * deltaDistance;
            _life.SpendDistance(deltaDistance);
        }

        protected abstract RaycastHit2D[] GetHits (float deltaDistance);
        protected abstract float GetTravaledDistance (Vector2 point, RaycastHit2D hit);
    }
}