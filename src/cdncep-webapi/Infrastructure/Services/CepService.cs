using cdncep_webapi.Infrastructure.Repositories;
using cdncep_webapi.ViewModels;

namespace cdncep_webapi.Infrastructure.Services
{
    public class CepService : ICepService
    {
        private readonly ICepRepository _cepRepository;
        private readonly ILogger<CepService> _logger;

        public CepService(ICepRepository cepRepository, ILogger<CepService> logger)
        {
            _cepRepository = cepRepository;
            _logger = logger;
        }

        public async Task<CepResponse> GetCepAsync(string cep)
        {
            try
            {
                var cepModel = await _cepRepository.GetCepAsync(cep);

                if (cepModel != null)
                {
                    var cepResp = new CepResponse
                    {
                        Id = cepModel.Id,
                        Cep = cepModel.Cep,
                        Bairro = cepModel.Bairro,
                        Cidade = cepModel.Cidade,
                        Complemento = cepModel.Complemento,
                        Logradouro = cepModel.Logradouro,
                        Uf = cepModel.Uf
                    };

                    return cepResp;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
            }

            return null;
        }

        public async Task<PagedResponse<CepPagedResponse>> GetPagedCepAsync(int pageNumber, int pageLimit, string sort, string cep, string cidade, string uf)
        {
            var ret = await _cepRepository.GetPagedCepAsync(pageNumber, pageLimit, sort, cep, cidade, uf);
            return ret;
        }
    }
}
