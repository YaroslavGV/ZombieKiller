using System.Collections.Generic;

namespace SaveLoad
{
    public interface IJsonHandler
    {
        public IEnumerable<string> Keys { get; }
        public bool DebugMode { get; set; }

        public void Add (IJsonHandle handle);
        public void Save ();
        public bool HasData (string key);
        public void Clear (string key);
        public void ClearAll ();
        public void DebugAll ();
    }
}