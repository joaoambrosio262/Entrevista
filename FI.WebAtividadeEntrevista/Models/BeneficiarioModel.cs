using System.ComponentModel.DataAnnotations;

namespace WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo de Beneficiário
    /// </summary>
    public class BeneficiarioModel
    {
        public long Id { get; set; }

        /// <summary>
        /// CPF do Beneficiário
        /// </summary>
        [Required(ErrorMessage = "O CPF do beneficiário é obrigatório")]
        [CpfAttribute(ErrorMessage = "O CPF do beneficiário informado é inválido")]
        public string CPF { get; set; }

        /// <summary>
        /// Nome do Beneficiário
        /// </summary>
        [Required(ErrorMessage = "O Nome do beneficiário é obrigatório")]
        public string Nome { get; set; }

        /// <summary>
        /// Id do Cliente
        /// </summary>
        public long IdCliente { get; set; }
    }
}
