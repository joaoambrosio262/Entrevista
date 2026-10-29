using FI.AtividadeEntrevista.DAL;
using FI.AtividadeEntrevista.DAL.Beneficiarios;
using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoCliente : IBoCliente
    {
        private readonly DaoCliente _dao;
        private readonly IBoBeneficiario _boBeneficiario;

        public BoCliente()
        {
            _dao = new DaoCliente();
            _boBeneficiario = new BoBeneficiario();
        }

        public long Incluir(Cliente cliente)
        {
            long id = _dao.Incluir(cliente);
            cliente.Id = id;

            if (cliente.Beneficiarios != null && cliente.Beneficiarios.Count > 0)
            {
                foreach (var item in cliente.Beneficiarios)
                {
                    item.IdCliente = id;
                    _boBeneficiario.Incluir(item);
                }
            }

            return id;
        }

        public void Alterar(Cliente cliente)
        {
            _dao.Alterar(cliente);

            if (cliente.Beneficiarios != null)
            {
                var beneficiariosExistentes = _boBeneficiario.Consultar(cliente.Id);

                // Exclui os que foram removidos
                var idsManter = cliente.Beneficiarios.Where(b => b.Id > 0).Select(b => b.Id).ToList();
                foreach (var existente in beneficiariosExistentes)
                {
                    if (!idsManter.Contains(existente.Id))
                    {
                        _boBeneficiario.Excluir(existente.Id);
                    }
                }

                // Insere novos ou altera existentes
                foreach (var item in cliente.Beneficiarios)
                {
                    item.IdCliente = cliente.Id;
                    if (item.Id > 0 && beneficiariosExistentes.Any(b => b.Id == item.Id))
                    {
                        _boBeneficiario.Alterar(item);
                    }
                    else
                    {
                        _boBeneficiario.Incluir(item);
                    }
                }
            }
        }

        public Cliente Consultar(long id)
        {
            var cliente = _dao.Consultar(id);
            if (cliente != null)
            {
                cliente.Beneficiarios = _boBeneficiario.Consultar(id);
            }
            return cliente;
        }

        public void Excluir(long id)
        {
            _dao.Excluir(id);
        }

        public List<Cliente> Listar()
        {
            return _dao.Listar();
        }

        public List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            return _dao.Pesquisa(iniciarEm, quantidade, campoOrdenacao, crescente, out qtd);
        }

        public bool VerificarExistencia(string CPF, long id = 0)
        {
            return _dao.VerificarExistencia(CPF, id);
        }
    }
}
