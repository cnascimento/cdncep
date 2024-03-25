using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cdncep_webapi.ViewModels
{
    /// <summary>
    ///     Modelo para requisições paginadas
    /// </summary>
    public class PagedResponse<T> where T : class
    {
        /// <summary>
        ///     Registros
        /// </summary>
        public IEnumerable<T> Records { get; set; }

        /// <summary>
        ///     Total de registros
        /// </summary>
        public long TotalRecords { get; set; }

        /// <summary>
        ///     Total de registros filtrados
        /// </summary>
        public long totalFilteredRecords { get; set; }

        /// <summary>
        ///     Registros por página
        /// </summary>
        public int PageLimit { get; set; }

        /// <summary>
        ///     Página corrente
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        ///     Total de páginas
        /// </summary>
        public int TotalPages { get { return (int)Math.Ceiling(totalFilteredRecords / (double)PageLimit); }}

        /// <summary>
        ///     Página anterior existente
        /// </summary>
        public bool PreviousPage { get { return CurrentPage > 1; }}

        /// <summary>
        ///     Próxima página existente
        /// </summary>
        public bool NextPage { get { return CurrentPage < TotalPages; }}
    }
}
