using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CommandCenter.Models
{
    public class CrisesModel
    {
        private static readonly TimeZoneInfo HorarioBrasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

        // 🔹 Identificação
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [BsonElement("NumeroDoIncidente")]
        public string NumeroDoIncidente { get; set; } = string.Empty;

        [BsonElement("Abrangencia")]
        public string Abrangencia { get; set; } = string.Empty;

        [BsonElement("ACN")]
        [Required(ErrorMessage = "O campo ACN é obrigatório.")]
        public string ACN { get; set; } = string.Empty;

        [BsonElement("CausaDoIncidente")]
        public string CausaDoIncidente { get; set; } = string.Empty;

        [BsonElement("ImpactoNosNegocios")]
        public string ImpactoNosNegocios { get; set; } = string.Empty;

        [BsonElement("EquipesAtuando")]
        public string EquipesAtuando { get; set; } = string.Empty;


        // 🔹 Datas e Horários
        private DateTime _dataHoraIncidente;
        private DateTime _dataHoraAcionamento;
        private DateTime? _dataEncerramento;

        [BsonElement("DataHoraIncidente")]
        public DateTime DataHoraIncidente
        {
            get => _dataHoraIncidente;
            set => _dataHoraIncidente = ConverterParaHorarioBrasilia(value);
        }

        [BsonElement("DataHoraAcionamento")]
        public DateTime DataHoraAcionamento
        {
            get => _dataHoraAcionamento;
            set => _dataHoraAcionamento = ConverterParaHorarioBrasilia(value);
        }

        [BsonElement("DataEncerramento")]
        public DateTime? DataEncerramento
        {
            get => _dataEncerramento;
            set => _dataEncerramento = value.HasValue ? ConverterParaHorarioBrasilia(value.Value) : null;
        }

        private static DateTime ConverterParaHorarioBrasilia(DateTime dataUtc)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dataUtc.ToUniversalTime(), HorarioBrasilia);
        }

        // 🔹 Acompanhamento e Histórico
        [BsonElement("AcoesRealizadas")]
        public List<string> AcoesRealizadas { get; set; } = new List<string>();

        public void adicionarHistoricoDeAcoes(string novoRegistro)
        {
            if (!string.IsNullOrWhiteSpace(novoRegistro))
            {
                AcoesRealizadas.Add(novoRegistro);
            }
        }

        // 🔹 Encerramento
        [BsonElement("solucaoAplicada")]
        public string? SolucaoAplicada { get; set; }

        [BsonElement("causaRaizResolvida")]
        public string? CausaRaizResolvida { get; set; }
    }
}
