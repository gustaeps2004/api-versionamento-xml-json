﻿using System.ComponentModel;

namespace Versionamento.Application.DTOs
{
    [DataObject]
    public class UsuariosDto
    {
        public Guid Codigo { get; set; }
        public string Nome { get; set; }
        public string DocumentoFederal { get; set; }
        public DateTime DtNasc { get; set; }
    }
}
