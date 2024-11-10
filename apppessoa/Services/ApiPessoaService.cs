using apppessoa.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apppessoa.Services
{
    public class ApiPessoaService
    {
        private readonly HttpClient _httpClient;

        public ApiPessoaService()
        {
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://10.0.2.2:7039/")
            };
        }

        public async Task<List<Pessoa>> GetPessoasAsync()
        {
            var response = await _httpClient.GetAsync("api/pessoa");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Pessoa>>(content);
        }

        public async Task<Pessoa> GetPessoaAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/pessoa/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Pessoa>(content);
        }

        public async Task CreateAsync(Pessoa pessoa)
        {
            var json = JsonConvert.SerializeObject(pessoa);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/pessoa", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(Pessoa pessoa)
        {
            var json = JsonConvert.SerializeObject(pessoa);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/pessoa/{pessoa.Id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/pessoa/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
