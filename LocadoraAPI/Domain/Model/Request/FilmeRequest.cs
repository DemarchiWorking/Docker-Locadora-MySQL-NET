using System;

namespace Domain.Model.Request
{
    public class FilmeRequest
    {
        public string titulo { get; set; }
        public int? classificacaoIndicativa { get; set; }
        public bool? lancamento { get; set; }
    }
}
