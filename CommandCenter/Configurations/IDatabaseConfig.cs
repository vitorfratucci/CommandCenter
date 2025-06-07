namespace CommandCenter.Configurations
{
    public interface IDatabaseConfig
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CrisesCollectionName { get; set; }
        string InformativosCollectionName { get; set; } // 🔹 Adicionado para Informativos
    }
}
