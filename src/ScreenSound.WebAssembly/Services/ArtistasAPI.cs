using ScreenSound.Shared.Modelos.Requests;
using ScreenSound.Shared.Modelos.Response;
using System.Net.Http.Json;

namespace ScreenSound.WebAssembly.Services
{
    public class ArtistasAPI
    {
        private readonly HttpClient _httpClient;

        public ArtistasAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        public async Task<ICollection<ArtistaResponse>> GetArtistasAsync()
        {
            return await _httpClient.GetFromJsonAsync<ICollection<ArtistaResponse>>("artistas");
        }

        public async Task AddArtistaAsync(ArtistaRequest artista)
        {
            await _httpClient.PostAsJsonAsync("artistas", artista);
        }
    }
}
