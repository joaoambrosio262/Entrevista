using FI.AtividadeEntrevista.DML;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FI.AtividadeEntrevista.DAL.Beneficiarios;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Cliente
    /// </summary>
    internal class DaoCliente : AcessoDados
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal long Incluir(Cliente cliente)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter(nameof(Cliente.Nome), cliente.Nome),
                new SqlParameter(nameof(Cliente.Sobrenome), cliente.Sobrenome),
                new SqlParameter(nameof(Cliente.Nacionalidade), cliente.Nacionalidade),
                new SqlParameter(nameof(Cliente.CEP), cliente.CEP),
                new SqlParameter(nameof(Cliente.Estado), cliente.Estado),
                new SqlParameter(nameof(Cliente.Cidade), cliente.Cidade),
                new SqlParameter(nameof(Cliente.Logradouro), cliente.Logradouro),
                new SqlParameter(nameof(Cliente.Email), cliente.Email),
                new SqlParameter(nameof(Cliente.Telefone), cliente.Telefone),
                new SqlParameter(nameof(Cliente.CPF), cliente.CPF)
            };

            DataSet dataSet = base.Consultar("FI_SP_IncClienteV2", parametros);
            
            long ret = 0;
            if (dataSet.Tables[0].Rows.Count > 0)
                long.TryParse(dataSet.Tables[0].Rows[0][0].ToString(), out ret);

            return ret;
        }

        /// <summary>
        /// Consulta um cliente pelo Id
        /// </summary>
        /// <param name="Id">Id do cliente</param>
        internal Cliente Consultar(long Id)
        {
            List<SqlParameter> parametros = new List<SqlParameter> 
            {
                new SqlParameter(nameof(Cliente.Id), Id)
            };

            DataSet dataSet = base.Consultar("FI_SP_ConsCliente", parametros);
            List<Cliente> clientesEncontrados = Converter(dataSet);

            return clientesEncontrados.FirstOrDefault();
        }

        internal bool VerificarExistencia(string CPF, long id = 0)
        {
            List<SqlParameter> parametros = new List<SqlParameter> 
            {
                new SqlParameter(nameof(Cliente.CPF), CPF),
                new SqlParameter(nameof(Cliente.Id), id)
            };

            DataSet dataSet = base.Consultar("FI_SP_VerificaCliente", parametros);

            return dataSet.Tables[0].Rows.Count > 0;
        }

        internal List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            List<SqlParameter> parametros = new List<SqlParameter> 
            {
                new SqlParameter("iniciarEm", iniciarEm),
                new SqlParameter("quantidade", quantidade),
                new SqlParameter("campoOrdenacao", campoOrdenacao),
                new SqlParameter("crescente", crescente)
            };

            DataSet dataSet = base.Consultar("FI_SP_PesqCliente", parametros);
            List<Cliente> clientes = Converter(dataSet);

            int iQtd = 0;

            if (dataSet.Tables.Count > 1 && dataSet.Tables[1].Rows.Count > 0)
                int.TryParse(dataSet.Tables[1].Rows[0][0].ToString(), out iQtd);

            qtd = iQtd;

            return clientes;
        }

        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        internal List<Cliente> Listar()
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter(nameof(Cliente.Id), 0) // se informado 0, a query retornará todos os clientes cadastrados
            };

            DataSet dataSet = base.Consultar("FI_SP_ConsCliente", parametros);
            List<Cliente> clientes = Converter(dataSet);

            return clientes;
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Alterar(Cliente cliente)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter(nameof(Cliente.Id), cliente.Id),
                new SqlParameter(nameof(Cliente.Nome), cliente.Nome),
                new SqlParameter(nameof(Cliente.Sobrenome), cliente.Sobrenome),
                new SqlParameter(nameof(Cliente.Nacionalidade), cliente.Nacionalidade),
                new SqlParameter(nameof(Cliente.CEP), cliente.CEP),
                new SqlParameter(nameof(Cliente.Estado), cliente.Estado),
                new SqlParameter(nameof(Cliente.Cidade), cliente.Cidade),
                new SqlParameter(nameof(Cliente.Logradouro), cliente.Logradouro),
                new SqlParameter(nameof(Cliente.Email), cliente.Email),
                new SqlParameter(nameof(Cliente.Telefone), cliente.Telefone),
                new SqlParameter(nameof(Cliente.CPF), cliente.CPF)
            };

            base.Executar("FI_SP_AltCliente", parametros);
        }

        /// <summary>
        /// Excluir Cliente
        /// </summary>
        /// <param name="Id">Id do cliente</param>
        internal void Excluir(long Id)
        {
            new DaoBeneficiario().ExcluirPorCliente(Id);

            List<SqlParameter> parametros = new List<SqlParameter> 
            {
                new SqlParameter(nameof(Cliente.Id), Id)
            };

            base.Executar("FI_SP_DelCliente", parametros);
        }

        private List<Cliente> Converter(DataSet ds)
        {
            List<Cliente> clientes = new List<Cliente>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Cliente cli = new Cliente();
                    cli.Id = row.Field<long>("Id");
                    cli.CEP = row.Field<string>("CEP");
                    cli.Cidade = row.Field<string>("Cidade");
                    cli.Email = row.Field<string>("Email");
                    cli.Estado = row.Field<string>("Estado");
                    cli.Logradouro = row.Field<string>("Logradouro");
                    cli.Nacionalidade = row.Field<string>("Nacionalidade");
                    cli.Nome = row.Field<string>("Nome");
                    cli.Sobrenome = row.Field<string>("Sobrenome");
                    cli.Telefone = row.Field<string>("Telefone");
                    cli.CPF = row.Field<string>("CPF");

                    clientes.Add(cli);
                }
            }

            return clientes;
        }
    }
}
