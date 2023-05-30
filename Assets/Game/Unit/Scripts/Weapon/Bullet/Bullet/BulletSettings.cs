using System;

namespace Weapon
{
    [Serializable]
    public struct BulletSettings
    {
        public float speed;
        public float maxDistance;
        public bool piercing;
        
        public BulletSettings (float speed, float maxDistance, bool piercing = false)
        {
            this.speed = speed;
            this.maxDistance = maxDistance;
            this.piercing = piercing;
        }

        public static BulletSettings Default => new BulletSettings(16, 32);
    }
}