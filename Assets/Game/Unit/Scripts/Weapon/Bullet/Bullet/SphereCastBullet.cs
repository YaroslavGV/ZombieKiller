using UnityEngine;

namespace Weapon
{
    public class SphereCastBullet : CastBullet
    {
        [SerializeField] private float _radius = 0.2f;

        protected override RaycastHit2D[] GetHits (float deltaDistance) => Physics2D.CircleCastAll(Ray.origin, _radius, Ray.direction, deltaDistance, Mask);
        protected override float GetTravaledDistance (Vector2 point, RaycastHit2D hit)
        {
            Vector2 hitSpherePoint = hit.point+hit.normal*_radius;
            return Vector2.Distance(point, hitSpherePoint);
        }

        private void OnDrawGizmosSelected ()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}