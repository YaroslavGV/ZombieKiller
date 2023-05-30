using UnityEngine;

namespace Unit.Skin
{
    public abstract class UnitSkinModul : UnitModul
    {
        public virtual void OnSpawnView (Object view) { }
        public virtual void OnRemoveView (Object view) { }
    }
}