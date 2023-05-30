using UnityEngine;

namespace Unit.Skin
{
    public class SkinModulFlipX : UnitSkinModul
    {
        private UnitSkin _skin;
        
        protected override void OnInitialize ()
        {
            _skin = Unit.Skin;
        }

        public override void Tick ()
        {
            InputValues values = Unit.Inputs;
            float side = 0;
            if (values.target != null)
                side = Mathf.Sign(values.target.transform.position.x - transform.position.x);
            else if (values.move.x != 0)
                side = Mathf.Sign(values.move.x);
            if (side == 0)
                return;

            if (side != _skin.transform.localScale.x)
            {
                Vector3 scale = _skin.transform.localScale;
                scale.x = Mathf.Abs(scale.x) * side;
                _skin.transform.localScale = scale;
            }
        }
    }
}