using System.Collections.Generic;

namespace Collection
{
    public interface IDropGenerator
    {
        public IEnumerable<CollectableDrop> GetDrop ();
    }
}