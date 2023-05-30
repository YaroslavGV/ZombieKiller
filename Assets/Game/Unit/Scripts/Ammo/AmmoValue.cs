using System;

namespace Weapon
{
    [Serializable]
    public struct AmmoValue
    {
        public AmmoType type;
        public int amount;
    }
}