namespace cdncep_webapi.ViewModels
{
    /// <summary>
    ///     Modelo para retornar um CEP
    /// </summary>
    public class CepResponse
    {
        /// <summary>
        ///     Identificador único do CEP
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     CEP
        /// </summary>
        public string Cep { get; set; }

        /// <summary>
        ///     Cidade
        /// </summary>
        public string Cidade { get; set; }

        /// <summary>
        ///     Unidade Federativa (Estado)
        /// </summary>
        public string Uf { get; set; }

        /// <summary>
        ///     Bairro
        /// </summary>
        public string Bairro { get; set; }

        /// <summary>
        ///     Logradouro
        /// </summary>
        public string Logradouro { get; set; }

        /// <summary>
        ///     Complemento
        /// </summary>
        public string Complemento { get; set; }
    }
}
