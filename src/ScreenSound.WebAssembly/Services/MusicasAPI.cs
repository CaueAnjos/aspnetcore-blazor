using ScreenSound.Shared.Modelos.Response;
using System.Net.Http.Json;

namespace ScreenSound.WebAssembly.Services
{
    public class MusicasAPI
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MusicasAPI> _logger;

        public MusicasAPI(IHttpClientFactory factory, ILogger<MusicasAPI> logger)
        {
            _httpClient = factory.CreateClient("API");
            _logger = logger;
        }

        public async Task<ICollection<MusicaResponse>> GetMusicasAsync()
        {
            try
            {
                _logger.LogInformation("Tentando buscar música no endpoint de músicas.");
                var result = await _httpClient.GetFromJsonAsync<ICollection<MusicaResponse>>("musicas");
                _logger.LogInformation("Busca de músicas concluída.");
                return result;
            }
            catch(Exception e)
            {
                string mensagem = "Erro ao buscar músicas da API.";
                _logger.LogError(e, mensagem);
                throw new ApplicationException(mensagem, e);
            }
        }
    }
}
