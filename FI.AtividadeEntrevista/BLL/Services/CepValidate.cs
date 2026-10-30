using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FI.AtividadeEntrevista.BLL.Interfaces;
using FI.AtividadeEntrevista.DML;
using Newtonsoft.Json;

namespace FI.AtividadeEntrevista.BLL.Services
{
    public class CepValidate : ICepValidate
    {
        private static readonly HttpClient httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(5)
        };

        private const string ViaCepApi = "https://viacep.com.br/ws/{0}/json/";

        public async Task<EnderecoCompleto> BuscarEnderecoPorCep_Async(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                return new EnderecoCompleto { Erro = true };

            string cepLimpo = Regex.Replace(cep, @"\D", "");
            if (cepLimpo.Length != 8)
                return new EnderecoCompleto { Erro = true };

            try
            {
                string url = string.Format(ViaCepApi, cepLimpo);
                var response = await httpClient.GetAsync(url).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                    return new EnderecoCompleto { Erro = true };

                string jsonContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var endereco = JsonConvert.DeserializeObject<EnderecoCompleto>(jsonContent);

                return endereco ?? new EnderecoCompleto { Erro = true };
            }
            catch (Exception)
            {
                return new EnderecoCompleto { Erro = true };
            }
        }

        public EnderecoCompleto BuscarEnderecoPorCep(string cep)
        {
            try
            {
                return Task.Run(() => BuscarEnderecoPorCep_Async(cep)).GetAwaiter().GetResult();
            }
            catch
            {
                return new EnderecoCompleto { Erro = true };
            }
        }

        public bool ValidarFormatoCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                return false;

            string cepLimpo = Regex.Replace(cep, @"\D", "");

            bool isValid = cepLimpo.Length == 8 && cepLimpo.All(char.IsDigit);
            return isValid;
        }

        public bool ValidarCep(string cep)
        {
            if (!ValidarFormatoCep(cep))
                return false;

            var endereco = BuscarEnderecoPorCep(cep);
            bool isValid = endereco != null && !endereco.Erro;
            
            return isValid;
        }
    }
}
