using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace SaveLoad
{
    public abstract class JsonHandler : IJsonHandler
    {
        private List<IJsonHandle> _handles = new List<IJsonHandle>();

        public IEnumerable<string> Keys => _handles.Select(h => h.Key);
        public bool DebugMode { get; set; }
        protected IEnumerable<IJsonHandle> Handles => _handles;

        public void Add (IJsonHandle handle)
        {
            _handles.Add(handle);
            Load(handle);
        }

        public abstract void Save ();

        public abstract bool HasData (string key);

        public abstract void Clear (string key);

        public abstract void ClearAll ();

        protected abstract void Load (IJsonHandle handle);

        public void DebugAll ()
        {
            foreach (IJsonHandle handle in Handles)
            {
                string key = handle.Key;
                string json = handle.GetJson();
                Debug.Log(string.Format("Key: {0}\n{1}", key, json));
            }
        }

        protected void DebugSave (string key, string json)
        {
            if (DebugMode)
                Debug.Log(string.Format("Save key: {0}\n{1}", key, json));
        }

        protected void DebugLoad (string key, string json)
        {
            if (DebugMode)
                Debug.Log(string.Format("Load key: {0}\n{1}", key, json));
        }

        protected void DebugDefaul (string key)
        {
            if (DebugMode)
                Debug.Log(string.Format("Defaul key: {0}", key));
        }

        protected void DebugClear (string key)
        {
            if (DebugMode)
                Debug.Log(string.Format("Clear key: {0}", key));
        }

        protected void DebugClearAll ()
        {
            if (DebugMode)
                Debug.Log("Clear All");
        }
    }
}