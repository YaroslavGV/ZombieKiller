using UnityEngine;
using StatSystem;

namespace Unit
{
    [RequireComponent(typeof(LineRenderer))]
    public class RadiusView : UnitTracker
    {
        [SerializeField] private float _segmentLenght = 0.25f;
        private Stat _radius;
        private LineRenderer _line;

        public override void SetTarget (UnitModel unit)
        {
            _line = GetComponent<LineRenderer>();
            _radius = unit.Stats.GetStat(StatType.ViewRadius);
            _radius.Changed += UpdateRadius;
            UpdateRadius();
            base.SetTarget(unit);
        }

        private void UpdateRadius () => UpdateLine(_radius.value);

        private void UpdateLine (float radius)
        {
            float lenght = Mathf.PI * 2f * radius;
            int segment = Mathf.RoundToInt(lenght / _segmentLenght);
            
            Vector3[] points = new Vector3[segment];
            float radianStep = Mathf.PI * 2f / (float)segment;
            for (int i = 0; i < segment; i++)
            {
                float radian = i * radianStep;
                points[i] = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0) * radius;
            }

            _line.positionCount = segment;
            _line.SetPositions(points);
        }
    }
}