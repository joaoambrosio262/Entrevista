using Newtonsoft.Json;

namespace FI.AtividadeEntrevista.DML
{
    /// <summary>
    /// DTO que representa os dados de endereço retornados na consulta da API ViaCEP
    /// </summary>
    public class EnderecoCompleto
    {
        [JsonProperty("cep")]
        public string Cep { get; set; }

        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty("complemento")]
        public string Complemento { get; set; }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("localidade")]
        public string Localidade { get; set; }

        [JsonProperty("uf")]
        public string Uf { get; set; }

        [JsonProperty("ibge")]
        public string Ibge { get; set; }

        [JsonProperty("gia")]
        public string Gia { get; set; }

        [JsonProperty("ddd")]
        public string Ddd { get; set; }

        [JsonProperty("siafi")]
        public string Siafi { get; set; }

        public bool Erro { get; set; }

        [JsonProperty("erro")]
        private object ErroRaw
        {
            set
            {
                if (value is bool b)
                    Erro = b;
                else if (value is string s && bool.TryParse(s, out bool parsed))
                    Erro = parsed;
                else if (value != null && value.ToString().ToLower() == "true")
                    Erro = true;
            }
        }
    }
}
