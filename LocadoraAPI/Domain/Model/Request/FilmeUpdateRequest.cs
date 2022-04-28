using System;

namespace Domain.Model.Request
{
    public class FilmeUpdateRequest
    {
        public int idFilme { get; set; }
        public string titulo { get; set; }
        public int classificacaoIndicativa { get; set; }
        public bool lancamento { get; set; }
    }
}
