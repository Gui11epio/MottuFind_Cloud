using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MottuFind_C_.Domain.VO
{
    public class VOPlaca
    {
        public string Numero { get; private set; }

        protected VOPlaca() { }

        public VOPlaca(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
                throw new ArgumentException("Placa não pode ser vazia");

            // Regex: aceita placas antigas (AAA-1234) e Mercosul (AAA1A23)
            var regex = new Regex(@"^[A-Z]{3}-?[0-9][0-9A-Z][0-9]{2}$");

            if (!regex.IsMatch(numero))
                throw new ArgumentException("Placa inválida");

            Numero = numero.ToUpper();
        }

        public override bool Equals(object obj)
        {
            if (obj is not VOPlaca other) return false;
            return Numero == other.Numero;
        }

        public override int GetHashCode() => Numero.GetHashCode();
        public override string ToString() => Numero;
    }
}
