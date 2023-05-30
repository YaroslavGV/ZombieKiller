using System;
using UnityEngine;

namespace Weapon
{
    public abstract class WeaponModel : MonoBehaviour, IDamageMediator
    {
        public event Action<string> RequireSkinAnimation;
        [SerializeField] private float _damageFactor = 1;
        [SerializeField] private float _range = 6;
        [SerializeField] private string _skinAnimationTrigger;
        private IDamageSorce _sorce;

        public abstract WeaponView View { get; }
        public virtual Action<WeaponModel> OnAttack { get; set; }
        public abstract bool IsAttacking { get; }
        protected IDamageSorce Sorce => _sorce;
        public float DamageFactor => _damageFactor;
        public float Range => _range;
        public string SkinAnimationTrigger => _skinAnimationTrigger;

        public void SetDamageSorce (IDamageSorce sorce) => _sorce = sorce;

        public abstract void Initlialize (IAimDirection aimDirection, IItemViewHandler viewHandler);
        public abstract void RemoveView ();
        public abstract void SetAttack (bool isAttacking);
        public virtual void StartAttack () => SetAttack(true);
        public virtual void StopAttack () => SetAttack(false);
        public abstract void Update ();
        public abstract void DoAttack ();

        protected void RequireAnimation () => RequireSkinAnimation?.Invoke(_skinAnimationTrigger);
    }
}