using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSharp.model
{
    public class CPF
    {
        public string? Email { get; private set; }

        // [Required]
        public string? Password { get; private set; }

        // [Required]
        public string? Cpf { get; private set; }

        // [Required]
        public string? Phone { get; private set; }
    }
}