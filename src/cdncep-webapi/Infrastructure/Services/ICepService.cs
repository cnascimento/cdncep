using cdncep_webapi.ViewModels;

namespace cdncep_webapi.Infrastructure.Services
{
    public interface ICepService
    {
        Task<CepResponse> GetCepAsync(string cep);

        Task<PagedResponse<CepPagedResponse>> GetPagedCepAsync(int pageNumber, int pageLimit, string sort, string cep, string cidade, string uf);
    }
}
