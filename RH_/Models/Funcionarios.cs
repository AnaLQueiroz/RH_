using System;
using System.Collections.Generic;

namespace RH_.Models
{
    public partial class Funcionarios
    {
        public Funcionarios()
        {
            Dependentes = new HashSet<Dependentes>();
        }

        public int Id { get; set; }
        public int MatriculaFuncionario { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Sexo { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public bool? Ativo { get; set; }

        public virtual ICollection<Dependentes> Dependentes { get; set; }
    }
}
