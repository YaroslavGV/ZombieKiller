using System.Collections;
using UnityEngine;

namespace Unit
{
    public class UnitTracker : MonoBehaviour
    {
        public virtual void SetTarget (UnitModel unit)
        {
            StartCoroutine(Track(unit));
        }

        public virtual void Stop ()
        {
            StopAllCoroutines();
        }

        private IEnumerator Track (UnitModel target)
        {
            transform.position = target.transform.position;
            while (target.IsAlive)
            {
                yield return null;
                transform.position = target.transform.position;
            }
        }
    }
}