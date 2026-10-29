using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public interface IBoCliente
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        long Incluir(Cliente cliente);

        /// <summary>
        /// Altera um cliente
        /// </summary>
        void Alterar(Cliente cliente);

        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        Cliente Consultar(long id);

        /// <summary>
        /// Exclui o cliente pelo id
        /// </summary>
        void Excluir(long id);

        /// <summary>
        /// Lista os clientes
        /// </summary>
        List<Cliente> Listar();

        /// <summary>
        /// Lista os clientes com paginação
        /// </summary>
        List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd);

        /// <summary>
        /// Verifica a existência do cliente por CPF
        /// </summary>
        bool VerificarExistencia(string CPF, long id = 0);
    }
}
