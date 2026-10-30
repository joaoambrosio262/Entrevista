using System.Threading.Tasks;
using FI.AtividadeEntrevista.DML;

namespace FI.AtividadeEntrevista.BLL.Interfaces
{
    public interface ICepValidate
    {
        /// <summary>
        /// Busca as informações de endereço de forma assíncrona a partir do CEP
        /// </summary>
        /// <param name="cep">CEP com ou sem máscara</param>
        /// <returns>Dados do endereço ou null/Erro caso não encontrado</returns>
        Task<EnderecoCompleto> BuscarEnderecoPorCep_Async(string cep);

        /// <summary>
        /// Busca as informações de endereço de forma síncrona a partir do CEP
        /// </summary>
        EnderecoCompleto BuscarEnderecoPorCep(string cep);

        /// <summary>
        /// Valida se o formato do CEP é válido (8 dígitos numéricos)
        /// </summary>
        bool ValidarFormatoCep(string cep);

        /// <summary>
        /// Valida se o CEP é válido no formato e existe na base dos Correios
        /// </summary>
        bool ValidarCep(string cep);
    }
}
