using System.Collections;
using UnityEngine;
using DitzelGames.FastIK;
using Weapon;

namespace Unit.Skin
{
    public class SkinModulHandIK : UnitSkinModul
    {
        [SerializeField] private FastIKFabric _front;
        [SerializeField] private FastIKFabric _back;

        public override void OnSpawnView (Object view)
        {
            StopAllCoroutines();
            if (view is IHandIK hik)
            {
                SetTarget(_front, hik.FrontHandIK);
                SetTarget(_back, hik.BackHandIK);
            }
        }

        public override void OnRemoveView (Object view)
        {
            if (view is IHandIK)
            {
                DelayClearTarget(_front);
                DelayClearTarget(_back);
            }
        }

        private void SetTarget (FastIKFabric fabric, TargetIK ik)
        {
            StartCoroutine(DelaySetIK(fabric, ik));
        }

        private void ImmediatelySetIK (FastIKFabric fabric, TargetIK ik)
        {
            if (ik.target != null)
            {
                fabric.Target = ik.target;
                fabric.Pole = ik.pole;
                fabric.enabled = true;
            }
            else
            {
                ImmediatelyClearTarget(fabric);
            }
        }

        private void ImmediatelyClearTarget (FastIKFabric fabric)
        {
            fabric.Target = null;
            fabric.Pole = null;
            fabric.enabled = false;
        }

        private IEnumerator DelaySetIK (FastIKFabric fabric, TargetIK ik)
        {
            yield return new WaitForEndOfFrame();
            ImmediatelySetIK(fabric, ik);
            yield return new WaitForEndOfFrame();
        }

        private IEnumerator DelayClearTarget (FastIKFabric fabric)
        {
            yield return new WaitForEndOfFrame();
            ImmediatelyClearTarget(fabric);
        }
    }
}