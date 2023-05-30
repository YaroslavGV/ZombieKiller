using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using StatSystem;
using FractionSystem;
using Weapon;
using InventorySystem;

namespace Unit
{
    using Skin;

    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public class UnitModel : MonoBehaviour, IFractionMember, IDamageable, IDamageReportReceiver, IDamageSorce
    {
        public Action<DamageReport> OnTakeDamage;
        public Action<DamageReport> OnDealDamage;
        public Action OnDead;
        [Tooltip("Order of modul initialize")]
        [SerializeField] private List<UnitModul> _moduls;
        private DamageProducer _damageProducer;
        private DamageHandler _damageHandler;

        public bool IsInit { get; private set; }
        public IUnitProfile Profile { get; private set; }
        public IFraction Fraction { get; private set; }
        public StatGroup Stats { get; private set; }
        public UnitSkin Skin { get; private set; }
        public Rigidbody2D Body { get; private set; }
        public CircleCollider2D Collider { get; private set; }
        public FloatingValue Health { get; private set; }
        public InputValues Inputs { get; set; }
        public bool IsAlive => _damageHandler.IsAlive;
        public Equipment Equipment { get; private set; }
        public IAmmoBackpack AmmoBackpack { get; private set; }

        private void Update ()
        {
            if (IsInit && IsAlive)
                foreach (UnitModul modul in _moduls)
                    modul.Tick();
        }

        private void LateUpdate ()
        {
            if (IsInit && IsAlive)
                foreach (UnitModul modul in _moduls)
                    modul.LateTick();
        }

        public override string ToString ()
        {
            return string.Format("Unit {0} {1}", name, Fraction.Name);
        }

        public void Initialize (IUnitProfile profile, Fraction fraction)
        {
            if (IsInit)
                return;

            Profile = profile;
            Fraction = fraction;

            Stats = new StatGroup(Profile.BaseStats);

            Equipment = Profile.Equipment;
            AmmoBackpack = Profile.AmmoBackpack;

            Skin = Instantiate(Profile.Skin, transform);
            _moduls.Insert(0, Skin);
            Body = GetComponent<Rigidbody2D>();
            Collider = GetComponent<CircleCollider2D>();

            _damageProducer = new DamageProducer(this);
            _damageHandler = new DamageHandler(this);

            var health = Stats.GetStat(StatType.Health);
            Health = new FloatingValue(health.value);
            Health.SetupMaxValue();

            foreach (UnitModul modul in _moduls)
                modul.Initialize(this);

            IsInit = true;
        }

        public T GetModul<T> () where T : UnitModul
        {
            Type targetType = typeof(T);
            var modul = _moduls.FirstOrDefault(m => targetType.IsAssignableFrom(m.GetType()));
            if (modul == null)
                Debug.LogWarning(targetType + " modul is missing");
            else if (modul.IsInit == false)
                Debug.LogWarning(targetType + " modul is not init");
            return (T)modul;
        }

        public bool CanAttack (IDamageable target) => _damageProducer.CanAttack(target);

        public void DoDamage (IDamageable target, IDamageMediator mediator) => _damageProducer.DoDamage(target, mediator);

        public void ApplyDamage (Damage damage)
        {
            _damageHandler.ApplyDamage(damage);
            if (Inputs.target == null && damage.sorce is UnitModel um && CanAttack(um))
            {
                InputValues inputs = Inputs;
                inputs.target = um;
                Inputs = inputs;
            }
        }

        public void AcceptDealDamageReport (DamageReport report)
        {
            OnDealDamage?.Invoke(report);
        }

        public void AcceptTakeDamageReport (DamageReport report)
        {
            OnTakeDamage?.Invoke(report);
            if (IsAlive == false)
                Dead();
        }

        private void Dead ()
        {
            foreach (UnitModul modul in _moduls)
                modul.OnDead();
            Body.velocity = Vector2.zero;
            Body.bodyType = RigidbodyType2D.Kinematic;
            Collider.enabled = false;
            OnDead?.Invoke();
        }
    }
}