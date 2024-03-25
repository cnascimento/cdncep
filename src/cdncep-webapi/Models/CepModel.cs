using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace cdncep_webapi.Models
{
    /// <summary>
    ///     Classe modelo de CEP
    /// </summary>
    public class CepModel
    {
        /// <summary>
        ///     Identificador único do CEP
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        ///     CEP
        /// </summary>
        [BsonElement("cep")]
        public string Cep { get; set; }

        /// <summary>
        ///     Cidade
        /// </summary>
        [BsonElement("cidade")]
        public string Cidade { get; set; }

        /// <summary>
        ///     Unidade Federativa (Estado)
        /// </summary>
        [BsonElement("uf")]
        public string Uf { get; set; }

        /// <summary>
        ///     Bairro
        /// </summary>
        [BsonElement("bairro")]
        public string Bairro { get; set; }

        /// <summary>
        ///     Logradouro
        /// </summary>
        [BsonElement("logradouro")]
        public string Logradouro { get; set; }

        /// <summary>
        ///     Complemento
        /// </summary>
        [BsonElement("complemento")]
        public string Complemento { get; set; }
    }
}