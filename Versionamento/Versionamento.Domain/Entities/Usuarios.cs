using System.Text.RegularExpressions;
using Versionamento.Domain.Validation;

namespace Versionamento.Domain.Entities
{
    public class Usuarios
    {
        public Guid Codigo { get; set; }
        public string Nome { get; private set; }
        public string DocumentoFederal { get; private set; }
        public DateTime DtNasc { get; private set; }

        public Usuarios(Guid codigo, string nome, string documentoFederal, DateTime dtNasc)
        {
            ValidarPropriedades(nome, documentoFederal, dtNasc);

            Codigo = codigo;
            Nome = nome;
            DocumentoFederal = documentoFederal;
            DtNasc = dtNasc;
        }

        private void ValidarPropriedades(string nome, string documentoFederal, DateTime dtNasc)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), 
                "Nome é obrigatório.");

            DomainExceptionValidation.When(nome.Length <= 0 || nome.Length > 255, 
                "Nome não pode ser menor ou igual a zero e nem maior que 255 caracteres.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(documentoFederal), 
                "Documento federal é obrigatório.");

            DomainExceptionValidation.When(documentoFederal.Length != 11 && documentoFederal.Length != 14, 
                "Documento federal tem que conter 11 caracteres para cpf ou 14 para cnpj");

            Regex regex = new Regex(@"^\d{4}-\d{2}-\d{2}$");
            DomainExceptionValidation.When(!regex.IsMatch(dtNasc.ToString("yyyy-MM-dd")), 
                "Data inválida");
        }
    }
}
