using UnityEngine;
using TMPro;
using Weapon;

namespace Unit
{
    public class AmmoView : MonoBehaviour
    {
        [SerializeField] private GameObject _view;
        [SerializeField] private TextMeshProUGUI _type;
        [SerializeField] private TextMeshProUGUI _count;

        private UnitModel _unit;
        private UnitModulWeapon _weaponModul;
        private IAmmoBackpack _backpack;
        private IAmmoUser _weapon;

        public void SetUnit (UnitModel unit)
        {
            if (_unit != null)
            {
                if (_weaponModul != null)
                    _weaponModul.OnWeaponChange -= OnChangeWapon;
                _backpack.Changed -= OnChangeAmmo;
                _weaponModul = null;
                _backpack = null;
                _weapon = null;
            }

            _unit = unit;
            _backpack = _unit.AmmoBackpack;
            _backpack.Changed += OnChangeAmmo;
            _weaponModul = _unit.GetModul<UnitModulWeapon>();
            if (_weaponModul != null)
            {
                _weaponModul.OnWeaponChange += OnChangeWapon;
                OnChangeWapon();
            }
            else
            {
                Debug.LogWarning("UnitModulWeapon is missing");
            }
        }

        private void OnChangeAmmo (AmmoType type, int count)
        {
            if (_weapon != null && _weapon.UsesAmmo == type)
                UpdatCount();
        }

        private void OnChangeWapon ()
        {
            _weapon = null;
            WeaponModel weapon = _weaponModul.EquipedWeapon;
            if (weapon != null)
            {
                if (weapon is IAmmoUser au)
                    _weapon = au;
                else
                    Debug.LogWarning(string.Format("{0} not implementation {1}", weapon, nameof(IAmmoUser)));
            }
            UpdateView();
        }

        private void UpdateView ()
        {
            if (_weapon != null)
            {
                if (_weapon.UsesAmmo != AmmoType.None)
                {
                    _type.text = _weapon.UsesAmmo.ToString();
                    UpdatCount();
                }
                else
                {
                    Debug.LogWarning(string.Format("{0} not use ammo", _weapon));
                    _view.SetActive(false);
                }
            }
            else
                _view.SetActive(false);
        }

        private void UpdatCount ()
        {
            if (_backpack != null)
                _count.text = _backpack.GetAmount(_weapon.UsesAmmo).ToString();
            else
                _count.text = "infinity";
        }
    }
}