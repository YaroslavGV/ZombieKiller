using System;
using UnityEngine;
using Zenject;
using InventorySystem;
using Weapon;

namespace Unit
{
    public class UnitModulWeapon : UnitModul, IAimDirection
    {
        public event Action OnWeaponChange;
        [Inject] private DiContainer _container;
        private WeaponItem _weaponItem;
        private WeaponModel _weaponModel;
        private bool _hasRequireParameter;
        
        public WeaponModel EquipedWeapon => _weaponModel;
        
        public override void Tick ()
        {
            if (_weaponModel != null)
            {
                bool attacking = Unit.Inputs.isAttacking;
                if (_weaponModel.IsAttacking != attacking)
                    _weaponModel.SetAttack(attacking);
                _weaponModel.Update();
            }
        }

        public override void OnDead ()
        {
            if (_weaponModel != null)
                _weaponModel.SetAttack(false);
        }

        public void Equipe (WeaponItem item)
        {
            _weaponModel = _container.InstantiatePrefabForComponent<WeaponModel>(item.Model, transform.position, transform.rotation, transform);
            _weaponModel.Initlialize(this, Unit.Skin);
            _weaponModel.SetDamageSorce(Unit);
            UpdateAnimationTrigger();
            _weaponItem = item;

            if (_weaponModel is IAmmoUser au)
                au.SetAmmoBackpack(Unit.AmmoBackpack);

            InputValues values = Unit.Inputs;
            values.attackRange = _weaponModel.Range;
            Unit.Inputs = values;

            OnWeaponChange?.Invoke();
        }

        public float GetRange () => _weaponModel != null ? _weaponModel.Range : 0;

        public Vector2 GetAimDirection () => Unit.Inputs.aimDirection;

        protected override void OnInitialize ()
        {
            Unit.Equipment.OnAdd += OnEquipe;
            Unit.Equipment.OnRemove += OnRemove;
            Unit.Skin.AttackHitCallback += OnSkinAttackHit;

            foreach (Item item in Unit.Equipment.Items)
                OnEquipe(item as EquipmentItem);
        }

        private void OnEquipe (EquipmentItem item)
        {
            if (item is WeaponItem wi)
                Equipe(wi);
        }

        private void OnRemove (EquipmentItem item)
        {
            if (item == _weaponItem)
            {
                _weaponModel.RemoveView();
                _weaponModel.RequireSkinAnimation -= PlayAnimation;
                Destroy(_weaponModel);
                _weaponModel = null;
                OnWeaponChange?.Invoke();
            }
        }

        private void PlayAnimation (string name)
        {
            if (_hasRequireParameter)
                Unit.Skin.Animator.SetTrigger(name);
            else
                _weaponModel.DoAttack();
        }

        private void OnSkinAttackHit () => _weaponModel.DoAttack();

        private void UpdateAnimationTrigger ()
        {
            string triggerName = _weaponModel.SkinAnimationTrigger;
            if (string.IsNullOrEmpty(triggerName) == false)
            {
                _weaponModel.RequireSkinAnimation += PlayAnimation;
                Animator animator = Unit.Skin.Animator;

                _hasRequireParameter = HasTriggerParametr(animator, triggerName);
                if (_hasRequireParameter == false)
                    Debug.LogWarning(string.Format("Miss animation trigger \"{0}\" in {1}", name, animator.runtimeAnimatorController.name));
            }
        }

        private bool HasTriggerParametr (Animator animator, string parameterName)
        {
            foreach (AnimatorControllerParameter parameter in animator.parameters)
                if (parameter.name == parameterName)
                    return parameter.type == AnimatorControllerParameterType.Trigger;
            return false;
        }
    }
}