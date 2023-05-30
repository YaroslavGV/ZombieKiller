using System;
using System.Collections;
using UnityEngine;

namespace Weapon
{
    public abstract class Bullet : MonoBehaviour, IConsumable<Bullet>
    {
        public Action<ShotData> OnShot;
        public Action<HitData> OnHitEnvironment;
        public Action<HitData> OnHitTarget;
        public Action<ExhaustData> OnExhaust;

        [SerializeField] private BulletSettings _settings = BulletSettings.Default;
        [Space]
        [SerializeField] private float _conusmeAfterFinishDelay = 1;
        private Action<Bullet> _consumeCallback;
        private PiercingList _piercing;
        
        public bool IsRunning { get; private set; }
        protected BulletSettings Settings => _settings;
        protected PiercingList Piercing => _piercing;
        protected IDamageSorce Sorce { get; private set; }
        protected IDamageMediator Mediator { get; private set; }
        protected Vector2 Direction { get; private set; }

        public void SetConsumeCallback (Action<Bullet> callback) => _consumeCallback = callback;

        public void SetSettings (BulletSettings settings)
        {
            _settings = settings;
        }

        public virtual void Fire (ShotData data, IDamageSorce sorce = null, IDamageMediator mediator = null)
        {
            Sorce = sorce;
            Mediator = mediator;
            Direction = data.direction;
            transform.position = data.point.position;
            IsRunning = true;
            _settings.speed = data.speed;
            if (_settings.piercing)
            {
                if (_piercing == null)
                    _piercing = new PiercingList();
                _piercing.ResetHitList();
            }
            OnShot?.Invoke(data);
        }

        public void Exhaust ()
        {
            IsRunning = false;
            OnExhaust?.Invoke(new ExhaustData(transform.position, transform.right));
            StopAllCoroutines();
            StartCoroutine(ConsumeDelay());
        }

        private IEnumerator ConsumeDelay ()
        {
            yield return new WaitForSeconds(_conusmeAfterFinishDelay);
            _consumeCallback?.Invoke(this);
        }

        /// <summary> Invoke hit event and finished if flag true </summary>
        protected void HitEnvironment (RaycastHit2D hit, bool isFinished)
        {
            if (isFinished)
                Exhaust();
            OnHitEnvironment?.Invoke(new HitData(hit.collider, hit.point, isFinished));
        }

        /// <summary> Return true if the bullet has finished or false if it has not interacted or pierced the target </summary>
        protected bool HandleTargetCollision (IDamageable target, Collider2D collider, RaycastHit2D hit)
        {
            if (Sorce == null)
                Debug.LogWarning("Bullet not have damage sorce");

            if (Sorce != null && Sorce.CanAttack(target) == false)
                return false;
            if (target.IsAlive == false)
                return false;
            
            if (_settings.piercing)
            {
                bool isPiercing = Piercing.IsAlreadyHit(collider) == false;
                if (isPiercing)
                {
                    Sorce?.DoDamage(target, Mediator);
                    Piercing.AddHitTarget(collider);
                }
            }
            else
            {
                Sorce?.DoDamage(target, Mediator);
            }
            bool isFinished = _settings.piercing == false;
            OnHitTarget?.Invoke(new HitData(hit.collider, hit.point, isFinished));
            return isFinished;
        }

        protected IDamageable GetDamageTarget (Collider2D collider) => collider.GetComponentInParent<IDamageable>();
    }
}