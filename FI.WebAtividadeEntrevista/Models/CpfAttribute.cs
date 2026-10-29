using System;
using System.ComponentModel.DataAnnotations;
using FI.AtividadeEntrevista.BLL;

namespace WebAtividadeEntrevista.Models
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class CpfAttribute : ValidationAttribute
    {
        public CpfAttribute()
        {
            ErrorMessage = "O CPF informado é inválido.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            string cpf = value.ToString();
            return ValidateCpf.Validador(cpf);
        }
    }
}
