using UnityEngine;

namespace Weapon
{
    public class RayCastBullet : CastBullet
    {
        protected override RaycastHit2D[] GetHits (float deltaDistance) => Physics2D.RaycastAll(Ray.origin, Ray.direction, deltaDistance, Mask);
        protected override float GetTravaledDistance (Vector2 point, RaycastHit2D hit) => Vector2.Distance(point, hit.point);
    }
}