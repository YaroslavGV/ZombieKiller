using UnityEngine;

namespace Weapon
{
    public class FireWeaponView : WeaponView, IHandIK
    {
        [SerializeField] private Transform _shootPoint;
        [Space]
        [SerializeField] private TargetIK _frontHandIK;
        [SerializeField] private TargetIK _backHandIK;
        [Space]
        [SerializeField] private Key _parentSkinPoint;

        public Transform ShootPoint => _shootPoint;
        public TargetIK FrontHandIK => _frontHandIK;
        public TargetIK BackHandIK => _backHandIK;
        public string ParentSkinPoint => _parentSkinPoint.Name;
    }
}