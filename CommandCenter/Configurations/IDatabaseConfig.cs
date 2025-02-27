namespace CommandCenter.Configurations
{
    public interface IDatabaseConfig
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CrisesCollectionName { get; set; } // 🔹 Adicione essa linha!
    }
}
