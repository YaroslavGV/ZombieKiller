﻿using UnityEngine;

namespace Unit
{
    public abstract class UnitModul : MonoBehaviour
    {
        public bool IsInit { get; private set; }
        protected UnitModel Unit { get; private set; }

        public void Initialize (UnitModel unit)
        {
            Unit = unit;
            OnInitialize();
            IsInit = true;
        }

        public virtual void Tick () { }

        public virtual void LateTick () { }

        public virtual void OnDead () { }

        protected virtual void OnInitialize () {}
    }
}