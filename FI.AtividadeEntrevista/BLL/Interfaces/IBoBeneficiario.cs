using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public interface IBoBeneficiario
    {
        /// <summary>
        /// Inclui um novo beneficiário
        /// </summary>
        long Incluir(Beneficiario beneficiario);

        /// <summary>
        /// Altera um beneficiário existente
        /// </summary>
        void Alterar(Beneficiario beneficiario);

        /// <summary>
        /// Consulta os beneficiários de um cliente
        /// </summary>
        List<Beneficiario> Consultar(long idCliente);

        /// <summary>
        /// Exclui um beneficiário pelo Id
        /// </summary>
        void Excluir(long id);

        /// <summary>
        /// Exclui todos os beneficiários de um cliente
        /// </summary>
        void ExcluirPorCliente(long idCliente);

        /// <summary>
        /// Verifica se já existe um beneficiário com o mesmo CPF para o mesmo cliente
        /// </summary>
        bool VerificarExistencia(string CPF, long idCliente, long id = 0);
    }
}
