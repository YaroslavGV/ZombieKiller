using UnityEngine;

namespace SaveLoad
{
    public class PlayerPrefsHandler : JsonHandler
    {
        public override void Save ()
        {
            foreach (IJsonHandle handle in Handles)
            {
                string key = handle.Key;
                string json = handle.GetJson();
                DebugSave(key, json);
                PlayerPrefs.SetString(key, json);
            }
        }

        public override bool HasData (string key) => PlayerPrefs.HasKey(key);

        public override void Clear (string key) 
        {
            DebugClear(key);
            PlayerPrefs.DeleteKey(key); 
        }

        public override void ClearAll ()
        {
            DebugClearAll();
            PlayerPrefs.DeleteAll();
        }

        protected override void Load (IJsonHandle handle)
        {
            string key = handle.Key;
            if (PlayerPrefs.HasKey(key))
            {
                string json = PlayerPrefs.GetString(key);
                DebugLoad(key, json);
                handle.SetJson(json);
            }
            else
            {
                DebugDefaul(key);
                handle.SetDefaul();
            }
        }
    }
}