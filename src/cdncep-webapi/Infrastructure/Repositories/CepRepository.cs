using cdncep_webapi.Models;
using cdncep_webapi.ViewModels;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace cdncep_webapi.Infrastructure.Repositories
{
    public class CepRepository : ICepRepository
    {
        private readonly ILogger<CepRepository> _logger;
        private readonly CdnCepContext _context;

        public CepRepository(IOptions<CdnCepSettings> settings, ILogger<CepRepository> logger)
        {
            _context = new CdnCepContext(settings);
            _logger = logger;
        }

        public async Task AddCepAsync(CepModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Obtém dados paginados de CEP's
        /// </summary>
        /// <param name="pageNumber">Número da página</param>
        /// <param name="pageLimit">Limkite de registros por página</param>
        /// <param name="sort">Ordenação</param>
        /// <returns>Objeto contento a listagem de CEP's e os dados de paginação</returns>
        public async Task<PagedResponse<CepPagedResponse>> GetPagedCepAsync(int pageNumber, int pageLimit, string sort, string cep, string cidade, string uf)
        {
            // Paginação
            pageLimit = pageLimit <= 0 ? 20 : pageLimit;
            var skip = pageNumber > 0 ? ((pageNumber - 1) * pageLimit) : 0;

            // Ordenação
            var sortJson = "{";
            if (!string.IsNullOrWhiteSpace(sort))
            {
                foreach (var field in sort.Split(","))
                {
                    var ordering = field.Substring(field.IndexOf(".") + 1);
                    switch (field)
                    {
                        case "cep.asc":
                        case "cep.desc":

                            if (ordering.Equals("asc"))
                                sortJson += "cep: 1,";
                            else if (ordering.Equals("desc"))
                                sortJson += "cep: -1,";

                            break;
                        case "cidade.asc":
                        case "cidade.desc":
                            if (ordering.Equals("asc"))
                                sortJson += "cidade: 1,";
                            else if (ordering.Equals("desc"))
                                sortJson += "cidade: -1,";

                            break;
                        case "uf.asc":
                        case "uf.desc":
                            if (ordering.Equals("asc"))
                                sortJson += "uf: 1,";
                            else if (ordering.Equals("desc"))
                                sortJson += "uf: -1,";

                            break;
                    }
                }
            }
            sortJson += "}";

            // Filtrando
            var filterJson = "{";
            if (cep == cidade && cidade == uf)
            {
                filterJson += $"$or: [{{ cep: /^{cep}/ }}, {{ cidade: /^(?i){cidade}(?-i)/ }}, {{ uf: /^(?i){uf}(?-i)/ }}]";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(cep))
                    filterJson += $"cep: /^{cep}/,";

                if (!string.IsNullOrWhiteSpace(cidade))
                    filterJson += $"cidade: /^(?i){cidade}(?-i)/,";

                if (!string.IsNullOrWhiteSpace(uf))
                    filterJson += $"uf: /^(?i){uf}(?-i)/,";
            }
            filterJson += "}";

            // Total de registros na base de dados
            var totalRecords = await _context.Ceps.CountDocumentsAsync(new BsonDocument());
            // Total de registros filtrados
            var totalFilteredRecords = await _context.Ceps.CountDocumentsAsync(filterJson);

            var entities = await _context.Ceps
                .Find(filterJson)
                .Sort(sortJson)
                .Skip(skip)
                .Limit(pageLimit)
                .ToListAsync();

            return new PagedResponse<CepPagedResponse>
            {
                Records = entities.Select(t => new CepPagedResponse { Id = t.Id, Cep = t.Cep, Cidade = t.Cidade, Uf = t.Uf }),
                TotalRecords = totalRecords,
                totalFilteredRecords = totalFilteredRecords,
                PageLimit = pageLimit,
                CurrentPage = pageNumber > 0 ? pageNumber : 1
            };
        }

        /// <summary>
        ///     Obtém os dados de um CEP
        /// </summary>
        /// <param name="cep">CEP solicitado</param>
        /// <returns>Retorna os dados do CEP solicitado</returns>
        public async Task<CepModel> GetCepAsync(string cep)
        {
            _logger.LogInformation("Executando a busca do CEP {cep} na base de dados.", cep);

            var entity = await _context.Ceps.Find(t => t.Cep.Equals(cep)).SingleOrDefaultAsync();

            _logger.LogInformation("Busca executada.");

            return entity;
        }

        public async Task UpdateCepAsync(CepModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
