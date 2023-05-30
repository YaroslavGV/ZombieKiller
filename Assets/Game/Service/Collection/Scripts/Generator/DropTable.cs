using System;
using System.Collections.Generic;

namespace Collection
{

    [Serializable]
    public struct DropTable : IDropGenerator
    {
        public DropBlock[] blocks;

        public IEnumerable<CollectableDrop> GetDrop ()
        {
            List<CollectableDrop> drop = new List<CollectableDrop>();
            foreach (DropBlock block in blocks)
                drop.AddRange(block.GetDrop());
            return drop;
        }
    }
}