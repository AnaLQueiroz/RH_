using System;
using System.Collections.Generic;

namespace RH_.Models
{
    public partial class Dependentes
    {
        public int Id { get; set; }
        public int MatriculaFuncionario { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
        public string Sexo { get; set; }

        public virtual Funcionarios MatriculaFuncionarioNavigation { get; set; }
    }
}
