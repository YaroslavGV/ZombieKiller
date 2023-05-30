using System;
using System.Linq;
using System.Collections.Generic;

namespace Weapon
{
    public class AmmoBackpack : IAmmoBackpack
    {
        public event Action<AmmoType, int> Changed;
        private Dictionary<AmmoType, int> _ammo = new Dictionary<AmmoType, int>();

        public bool InfinityAmmo { get; set; }

        public override string ToString () => string.Join(", ", _ammo.Select(a => string.Format("{0} {1}", a.Key.ToString(), a.Value.ToString())));

        public int GetAmount (AmmoType type)
        {
            if (_ammo.ContainsKey(type))
                return _ammo[type];
            return 0;
        }

        public void Spend (AmmoType type, int amount = 1)
        {
            if (amount < 0)
                AmountIsNegative();

            _ammo[type] -= amount;
            Changed?.Invoke(type, _ammo[type]);
        }

        public void Add (AmmoType type, int amount)
        {
            if (amount < 0)
                AmountIsNegative();

            if (_ammo.ContainsKey(type))
                _ammo[type] += amount;
            else
                _ammo.Add(type, amount);
            Changed?.Invoke(type, _ammo[type]);
        }

        private void AmountIsNegative () => throw new ArgumentException(string.Format("Amount must be positive value"));
    }
}