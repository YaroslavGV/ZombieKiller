using UnityEngine;
using Zenject;

namespace Weapon
{
    public class FireWeaponModel : BasicWeaponModel, IBulletAssetUser, IAmmoUser
    {
        [SerializeField] private float _bulletSpeed = 16;
        [SerializeField] private float _scatterAngle = 5;
        [SerializeField] private FireWeaponView _viewTemplate;
        [SerializeField] private BulletTemplateAsset _bulletAsset;
        [SerializeField] private AmmoType _usesAmmo;
        [Inject] private SceneBullets _sceneBullets;
        private FireWeaponView _view;
        private BulletHandler _bullets;
        private IAimDirection _aimDirection;
        private IItemViewHandler _viewHandler;
        private IAmmoBackpack _ammoBackpack;

        public override WeaponView View => _view;
        public BulletTemplateAsset Asset => _bulletAsset;
        public AmmoType UsesAmmo => _usesAmmo;

        private void OnDestroy ()
        {
            Remove();
        }

        public override void Initlialize (IAimDirection aimDirection, IItemViewHandler viewHandler)
        {
            _aimDirection = aimDirection;
            _viewHandler = viewHandler;
            if (_viewTemplate != null)
                _view = _viewHandler.SpawnView(_viewTemplate, _viewTemplate.ParentSkinPoint);
            _bullets = _sceneBullets.AddUser(this);
        }

        public void SetAmmoBackpack (IAmmoBackpack backpack) => _ammoBackpack = backpack;

        public override void Remove ()
        {
            if (_view != null)
                _viewHandler.RemoveView(_view);
        }

        public override void DoAttack ()
        {
            if (TrySpendAmmo() == false)
                return;

            Vector2 vector = _aimDirection.GetAimDirection();
            Vector2 direction = GetDirection(vector, _scatterAngle);
            Transform shootPoint = _view != null ? _view.ShootPoint : _viewHandler.GetWeaponPoint();
            ShotData data = new ShotData(shootPoint, direction, _bulletSpeed);
            
            _bullets.Shoot(data, Sorce, this);
            OnAttack?.Invoke(this);
        }

        private bool TrySpendAmmo (int count = 1)
        {
            if (_ammoBackpack != null)
            {
                if (_ammoBackpack.InfinityAmmo)
                    return true;

                bool haveAmmo = _ammoBackpack.GetAmount(_usesAmmo) > count;
                if (haveAmmo)
                    _ammoBackpack.Spend(_usesAmmo);
                return haveAmmo;
            }
            else
                return false;
        }

        private Vector2 GetDirection (Vector2 direction, float scatterAngle)
        {
            float angle = Mathf.Atan2(direction.y, direction.x);
            float radScatter = scatterAngle * Mathf.Deg2Rad;
            float radAngle = angle + Random.Range(0, radScatter) - radScatter * 0.5f;
            return new Vector2(Mathf.Cos(radAngle), Mathf.Sin(radAngle));
        }
    }
}