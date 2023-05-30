using Unit;

namespace Collection
{
    public class CollectorHealth : IDropCollector
    {
        private readonly UnitModel _unit;

        public CollectorHealth (UnitModel unit)
        {
            _unit = unit;
        }

        public bool TryCollect (ICollectobleDrop target)
        {
            if (target is CollectableHealth helathTarget)
            {
                _unit.Health.Value += helathTarget.Amount;
                return true;
            }
            return false;
        }
    }
}