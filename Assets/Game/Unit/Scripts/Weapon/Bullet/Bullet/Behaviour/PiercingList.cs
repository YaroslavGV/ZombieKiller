using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class PiercingList
    {
        private List<Collider2D> _hittedPerRun;

        public void ResetHitList () => _hittedPerRun = new List<Collider2D>();
        public bool IsAlreadyHit (Collider2D target) => _hittedPerRun.Contains(target);
        public void AddHitTarget (Collider2D target) => _hittedPerRun.Add(target);
    }
}