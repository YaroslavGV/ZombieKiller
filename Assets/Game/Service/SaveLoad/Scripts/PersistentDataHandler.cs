using System.IO;
using System.Text;
using Defective.JSON;
using UnityEngine;
using UnityEngine.Android;

namespace SaveLoad
{
    public class PersistentDataHandler : JsonObjectHandler
    {
        public const string FileName = "SaveData.txt";

        protected override void LoadJsonObject ()
        {
            RequestPermission();

            string json = "";
            string path = DataPath;

            if (File.Exists(path))
            {
                FileStream fs = File.Open(path, FileMode.Open);
                byte[] b = new byte[fs.Length];
                UTF8Encoding temp = new UTF8Encoding(true);
                int readLen;
                while ((readLen = fs.Read(b, 0, b.Length)) > 0)
                    json = temp.GetString(b, 0, readLen);
                fs.Close();
            }
            else
            {
                if (DebugMode)
                    Debug.Log("Load Json: File not exist");
            }
            if (DebugMode)
                Debug.Log("Load Json: " + json);
            jObject = new JSONObject(json);
        }

        protected override void SaveJsonObject ()
        {
            if (jObject == null)
            {
                Debug.LogError("Json Object is empty");
                return;
            }
            string json = jObject.ToString();
            string path = DataPath;
            
            if (File.Exists(path))
                File.Delete(path);
            
            FileStream fs = File.Create(DataPath);
            byte[] info = new UTF8Encoding(true).GetBytes(json);
            fs.Write(info, 0, info.Length);
            fs.Close();
            if (DebugMode)
                Debug.Log("Save Json: byte: {0}" + info.Length);
        }

        private void RequestPermission ()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                string read = Permission.ExternalStorageRead;
                string write = Permission.ExternalStorageWrite;
                if (Permission.HasUserAuthorizedPermission(read) == false || Permission.HasUserAuthorizedPermission(write))
                    Permission.RequestUserPermissions(new[] { read, write });
            }
        }

        private string DataPath => Path.Combine(Application.persistentDataPath, FileName);
    }
}