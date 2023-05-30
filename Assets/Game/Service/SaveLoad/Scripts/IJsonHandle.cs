namespace SaveLoad
{
    public interface IJsonHandle
    {
        public string Key { get; }
        public void SetJson (string json);
        public string GetJson ();
        public void SetDefaul ();
    }
}