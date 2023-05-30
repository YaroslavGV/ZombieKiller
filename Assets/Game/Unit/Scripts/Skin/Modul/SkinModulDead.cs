using UnityEngine;

namespace Unit.Skin
{
    public class SkinModulDead : UnitSkinModul
    {
        [SerializeField] private string _triggerParameter = "Dead";
        private Animator _animator;

        public override void OnDead ()
        {
            _animator.SetTrigger(_triggerParameter);
        }

        protected override void OnInitialize ()
        {
            _animator = Unit.Skin.Animator;
        }
    }
}