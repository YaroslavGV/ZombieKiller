using UnityEngine;
using UnityEngine.Events;

namespace Collection
{
    public abstract class CollectableDrop : MonoBehaviour, ICollectobleDrop
    {
        public UnityEvent OnPickUp;

        public void PickUp ()
        {
            OnPickUp?.Invoke();
        }

        public void DestroySelf () => Destroy(gameObject);
    }
}