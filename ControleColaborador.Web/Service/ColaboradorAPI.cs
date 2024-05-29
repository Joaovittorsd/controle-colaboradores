using ControleColaborador.Web.Requests;
using ControleColaborador.Web.Response;
using System.Net.Http.Json;

namespace ControleColaborador.Web.Service;

public class ColaboradorAPI
{
    private readonly HttpClient _httpClient;

    public ColaboradorAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<ICollection<ColaboradorResponse>?> GetColaboradoresAsync()
    {
        return await _httpClient.GetFromJsonAsync<ICollection<ColaboradorResponse>>("colaboradores");
    }

    public async Task<ColaboradorResponse?> GetColaboradorPorIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<ColaboradorResponse>($"colaboradores/{id}");
    }

    public async Task AddColaboradorAsync(ColaboradorRequest colaborador)
    {
        await _httpClient.PostAsJsonAsync("colaboradores", colaborador);
    }

    public async Task UpdateColaboradorAsync(ColaboradorRequestEdit colaborador)
    {
        await _httpClient.PutAsJsonAsync($"colaboradores", colaborador);
    }
}
        