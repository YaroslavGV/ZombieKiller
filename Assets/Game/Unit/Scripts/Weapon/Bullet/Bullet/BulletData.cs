using UnityEngine;

namespace Weapon
{
    public struct ShotData
    {
        public Transform point;
        public Vector2 direction;
        public float speed;

        public ShotData (Transform point, Vector2 direction, float speed) : this()
        {
            this.point = point;
            this.direction = direction;
            this.speed = speed;
        }
    }

    public struct HitData
    {
        public Collider2D collider;
        public Vector2 position;
        public bool isFinished;

        public HitData (Collider2D collider, Vector2 position, bool isFinished)
        {
            this.collider = collider;
            this.position = position;
            this.isFinished = isFinished;
        }
    }

    public struct ExhaustData
    {
        public Vector3 position;
        public Vector3 vector;

        public ExhaustData (Vector3 position, Vector3 vector)
        {
            this.position = position;
            this.vector = vector;
        }
    }
}