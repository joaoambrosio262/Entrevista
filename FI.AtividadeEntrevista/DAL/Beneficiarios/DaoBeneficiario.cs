using FI.AtividadeEntrevista.DML;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace FI.AtividadeEntrevista.DAL.Beneficiarios
{
    /// <summary>
    /// Classe de acesso a dados de Beneficiário
    /// </summary>
    internal class DaoBeneficiario : AcessoDados
    {
        /// <summary>
        /// Inclui um novo beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiário</param>
        internal long Incluir(Beneficiario beneficiario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter(nameof(Beneficiario.CPF), beneficiario.CPF),
                new SqlParameter(nameof(Beneficiario.Nome), beneficiario.Nome),
                new SqlParameter(nameof(Beneficiario.IdCliente), beneficiario.IdCliente)
            };

            DataSet dataSet = base.Consultar("FI_SP_IncBenef", parametros);

            long ret = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
                long.TryParse(dataSet.Tables[0].Rows[0][0].ToString(), out ret);

            return ret;
        }

        /// <summary>
        /// Altera um beneficiário existente
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiário</param>
        internal void Alterar(Beneficiario beneficiario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter(nameof(Beneficiario.Nome), beneficiario.Nome),
                new SqlParameter(nameof(Beneficiario.CPF), beneficiario.CPF),
                new SqlParameter(nameof(Beneficiario.Id), beneficiario.Id)
            };

            base.Executar("FI_SP_AltBenef", parametros);
        }

        /// <summary>
        /// Consulta beneficiários por Id do cliente
        /// </summary>
        /// <param name="idCliente">Id do cliente</param>
        internal List<Beneficiario> ConsultarPorCliente(long idCliente)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter(nameof(Beneficiario.IdCliente), idCliente)
            };

            DataSet dataSet = base.Consultar("FI_SP_ConsBenef", parametros);
            return Converter(dataSet);
        }

        /// <summary>
        /// Exclui um beneficiário pelo Id
        /// </summary>
        /// <param name="id">Id do beneficiário</param>
        internal void Excluir(long id)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter(nameof(Beneficiario.Id), id)
            };

            base.Executar("FI_SP_DelBenef", parametros);
        }

        /// <summary>
        /// Exclui todos os beneficiários de um cliente
        /// </summary>
        /// <param name="idCliente">Id do cliente</param>
        internal void ExcluirPorCliente(long idCliente)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter(nameof(Beneficiario.IdCliente), idCliente)
            };

            base.Executar("FI_SP_DelBenefPorCliente", parametros);
        }

        /// <summary>
        /// Verifica se já existe um beneficiário com o mesmo CPF para o mesmo cliente
        /// </summary>
        internal bool VerificarExistencia(string CPF, long idCliente, long id = 0)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter(nameof(Beneficiario.CPF), CPF),
                new SqlParameter(nameof(Beneficiario.IdCliente), idCliente),
                new SqlParameter(nameof(Beneficiario.Id), id)
            };

            DataSet dataSet = base.Consultar("FI_SP_VerificaBeneficiario", parametros);

            return dataSet.Tables[0].Rows.Count > 0;
        }

        private List<Beneficiario> Converter(DataSet ds)
        {
            List<Beneficiario> beneficiarios = new List<Beneficiario>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Beneficiario beneficiario = new Beneficiario();
                    beneficiario.Id = row.Field<long>("Id");
                    beneficiario.CPF = row.Field<string>("CPF");
                    beneficiario.Nome = row.Field<string>("Nome");
                    beneficiario.IdCliente = row.Field<long>("IdCliente");

                    beneficiarios.Add(beneficiario);
                }
            }

            return beneficiarios;
        }
    }
}
