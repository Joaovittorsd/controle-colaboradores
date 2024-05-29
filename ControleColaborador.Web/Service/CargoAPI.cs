using ControleColaborador.Web.Response;
using System.Net.Http.Json;

namespace ControleColaborador.Web.Service;

public class CargoAPI
{
    private readonly HttpClient _httpClient;

    public CargoAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<ICollection<CargoResponse>?> GetCargosAsync()
    {
        return await _httpClient.GetFromJsonAsync<ICollection<CargoResponse>>("cargos");
    }
}
