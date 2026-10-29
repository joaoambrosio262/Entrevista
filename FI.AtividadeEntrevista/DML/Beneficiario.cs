using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.DML
{
    /// <summary>
    /// Classe de Beneficiário que representa o registro na tabela BENEFICIARIOS
    /// </summary>
    public class Beneficiario
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// CPF do Beneficiário
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// Nome do Beneficiário
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// ID do Cliente associado
        /// </summary>
        public long IdCliente { get; set; }
    }
}
