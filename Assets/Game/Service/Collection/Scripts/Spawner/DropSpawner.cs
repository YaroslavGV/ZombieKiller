using System.Collections.Generic;
using UnityEngine;

namespace Collection
{
    public class DropSpawner : MonoBehaviour
    {
        [SerializeField] private CollectableDrop[] _drop;
        [SerializeField] private float _range = 1;

        public void SpawnDrop (CollectableDrop dropTemplate, Vector2 position)
        {
            Vector2 offset = Random.insideUnitCircle.normalized * _range;
            Instantiate(dropTemplate, position + offset, Quaternion.identity, transform);
        }

        public void SpawnDrop (IEnumerable<CollectableDrop> dropTemplates, Vector2 position)
        {
            foreach (CollectableDrop drop in dropTemplates)
                SpawnDrop(drop, position);
        }

        public void SpawnDrop (DropTable table, Vector2 position) => SpawnDrop(table.GetDrop(), position);

        public void SpawnDrop (IDropTableContainer container, Vector2 position) => SpawnDrop(container.DropTable, position);
    }
}
