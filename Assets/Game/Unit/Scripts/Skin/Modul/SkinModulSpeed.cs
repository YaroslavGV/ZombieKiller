using UnityEngine;

namespace Unit.Skin
{
    public class SkinModulSpeed : UnitSkinModul
    {
        [SerializeField] private string _floatParameter = "Speed";
        private Animator _animator;

        public override void Tick ()
        {
            Vector2 velocity = Unit.Inputs.notMovable == false ? Unit.Inputs.move : Vector2.zero;
            _animator.SetFloat(_floatParameter, velocity.magnitude);
        }

        protected override void OnInitialize ()
        {
            _animator = Unit.Skin.Animator;
        }
    }
}