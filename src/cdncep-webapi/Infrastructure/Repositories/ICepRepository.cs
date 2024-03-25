using cdncep_webapi.Models;
using cdncep_webapi.ViewModels;

namespace cdncep_webapi.Infrastructure.Repositories
{
    public interface ICepRepository
    {
        Task<CepModel> GetCepAsync(string cep);

        Task<PagedResponse<CepPagedResponse>> GetPagedCepAsync(int pageNumber, int pageLimit, string sort, string cep, string cidade, string uf);

        Task AddCepAsync(CepModel entity);

        Task UpdateCepAsync(CepModel entity);
    }
}