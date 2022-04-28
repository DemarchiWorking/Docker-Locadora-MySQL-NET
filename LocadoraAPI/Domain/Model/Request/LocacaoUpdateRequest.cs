using System;

namespace Domain.Model.Request
{
    public class LocacaoUpdateRequest
    {
            public int idLocacao { get; set; }
            public int? idCliente { get; set; }
            public int? idFilme { get; set; }
            public DateTime? dataLocacao { get; set; }
            public DateTime? dataDevolucao { get; set; }
        }
}
