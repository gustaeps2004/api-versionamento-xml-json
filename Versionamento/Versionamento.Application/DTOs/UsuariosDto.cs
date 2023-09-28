using System.ComponentModel;
using System.Xml.Serialization;

namespace Versionamento.Application.DTOs
{
    public class UsuariosDto
    {        
        public Guid Codigo { get; set; }
        public string Nome { get; set; }
        public string DocumentoFederal { get; set; }
        public DateTime DtNasc { get; set; }

    }
}
