namespace CommandCenter.Configurations
{
    public class DatabaseConfig : IDatabaseConfig
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CrisesCollectionName { get; set; }
        public string InformativosCollectionName { get; set; } // 🔹 Adicionado para Informativos
    }
}
