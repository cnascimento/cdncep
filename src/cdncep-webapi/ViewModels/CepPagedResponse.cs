namespace cdncep_webapi.ViewModels
{
    /// <summary>
    ///     Modelo para retornar os CEPs paginados
    /// </summary>
    public class CepPagedResponse
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
        ///     Cidade do CEP
        /// </summary>
        public string Cidade { get; set; }

        /// <summary>
        ///     Estado do CEP
        /// </summary>
        public string Uf { get; set; }
    }
}
