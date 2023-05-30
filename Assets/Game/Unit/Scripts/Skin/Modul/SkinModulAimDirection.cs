using UnityEngine;

namespace Unit.Skin
{
    public class SkinModulAimDirection : UnitSkinModul
    {
        [SerializeField] private Transform _aim;

        public override void LateTick ()
        {
            Vector2 direction = Unit.Inputs.aimDirection;
            if (direction == Vector2.zero)
                return;

            float scaleX = Mathf.Sign(transform.lossyScale.x);
            float angle = Mathf.Atan2(direction.y, direction.x * scaleX) * Mathf.Rad2Deg;
            if (_aim != null)
            {
                Vector3 rot = _aim.transform.localEulerAngles;
                rot.z = angle;
                _aim.transform.localEulerAngles = rot;
            }
        }
    }
}