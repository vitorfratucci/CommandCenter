using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace CommandCenter.Models
{
    public class InformativosModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [BsonElement("OperacaoAfetada")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string OperacaoAfetada { get; set; }

        [BsonElement("NaturezaIncidente")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string NaturezaIncidente { get; set; }

        [BsonElement("TipoImpacto")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string TipoImpacto { get; set; }

        [BsonElement("DescricaoImpacto")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string DescricaoImpacto { get; set; }

        [BsonElement("HorarioInicio")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public DateTime HorarioInicio { get; set; }

        [BsonElement("PrevisaoTermino")]
        public DateTime? PrevisaoTermino { get; set; } // Exibido apenas na mensagem (Create/Edit)

        [BsonElement("HorarioTermino")]
        public DateTime? HorarioTermino { get; set; } // Usado somente no encerramento

        [BsonElement("Status")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Status { get; set; }
    }
}