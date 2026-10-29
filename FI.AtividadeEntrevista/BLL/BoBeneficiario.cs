using System.Collections.Generic;
using FI.AtividadeEntrevista.DAL.Beneficiarios;
using FI.AtividadeEntrevista.DML;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario : IBoBeneficiario
    {
        private readonly DaoBeneficiario _dao;

        public BoBeneficiario()
        {
            _dao = new DaoBeneficiario();
        }

        public long Incluir(Beneficiario beneficiario)
        {
            return _dao.Incluir(beneficiario);
        }

        public void Alterar(Beneficiario beneficiario)
        {
            _dao.Alterar(beneficiario);
        }

        public List<Beneficiario> Consultar(long idCliente)
        {
            return _dao.ConsultarPorCliente(idCliente);
        }

        public void Excluir(long id)
        {
            _dao.Excluir(id);
        }

        public void ExcluirPorCliente(long idCliente)
        {
            _dao.ExcluirPorCliente(idCliente);
        }

        public bool VerificarExistencia(string CPF, long idCliente, long id = 0)
        {
            return _dao.VerificarExistencia(CPF, idCliente, id);
        }
    }
}
