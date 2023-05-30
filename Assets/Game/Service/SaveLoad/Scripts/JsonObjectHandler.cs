using Defective.JSON;
using UnityEngine;

namespace SaveLoad
{
    public abstract class JsonObjectHandler : JsonHandler
    {
        protected JSONObject jObject;

        public JsonObjectHandler () => LoadJsonObject();

        public override void Save ()
        {
            foreach (IJsonHandle handle in Handles)
            {
                string key = handle.Key;
                string json = handle.GetJson();
                DebugSave(key, json);
                jObject.SetField(key, new JSONObject(json));
            }
            SaveJsonObject();
        }

        public override bool HasData (string key) => jObject.HasField(key);

        public override void Clear (string key)
        {
            DebugClear(key);
            jObject.RemoveField(key);
            SaveJsonObject();
        }

        public override void ClearAll ()
        {
            DebugClearAll();
            jObject = new JSONObject();
            SaveJsonObject();
        }

        protected override void Load (IJsonHandle handle)
        {
            string key = handle.Key;
            if (jObject.HasField(key))
            {
                string json = jObject.GetField(key).ToString();
                DebugLoad(key, json);
                handle.SetJson(json);
            }
            else
            {
                DebugDefaul(key);
                handle.SetDefaul();
            }
        }

        protected abstract void SaveJsonObject ();
        protected abstract void LoadJsonObject ();
    }
}