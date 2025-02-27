namespace CommandCenter.Configurations
{
    public class DatabaseConfig : IDatabaseConfig
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CrisesCollectionName { get; set; } // 🔹 Adicione essa linha!
    }
}
