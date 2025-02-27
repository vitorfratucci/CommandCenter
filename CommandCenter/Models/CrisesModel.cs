using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CommandCenter.Models
{
    public class CrisesModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [BsonElement("TipoDeCrise")]
        public string TipoDeCrise { get; set; }

        [BsonElement("NumeroDoIncidente")]
        public string NumeroDoIncidente { get; set; }

        [BsonElement("Abrangencia")]
        public string Abrangencia { get; set; }

        [BsonElement("ACN")]
        [Required(ErrorMessage = "O campo ACN é obrigatório.")]
        public string ACN { get; set; }
      
        [BsonElement("CausaDoIncidente")]
        public string CausaDoIncidente { get; set; }

        [BsonElement("ImpactoNosNegocios")]
        public string ImpactoNosNegocios { get; set; }

        [BsonElement("DataHoraIncidente")]
        public DateTime DataHoraIncidente { get; set; }

        [BsonElement("DataHoraAcionamento")]
        public DateTime DataHoraAcionamento { get; set; }

        [BsonElement("DataEncerramento")]
        public DateTime? DataEncerramento { get; set; }

        [BsonElement("EquipesAtuando")]
        public string EquipesAtuando { get; set; }

        [BsonElement("AcoesRealizadas")]
        public List<string> AcoesRealizadas { get; set; } = new List<string>();

        public void adicionarHistoricoDeAcoes(string novoRegistro)
        {
            if (!string.IsNullOrWhiteSpace(novoRegistro)){
                AcoesRealizadas.Add(novoRegistro);
            }
        }
    }
}