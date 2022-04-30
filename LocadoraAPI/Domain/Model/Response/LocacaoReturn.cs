using System;

namespace Domain.Model.Dao
{
    public class LocacaoReturn
    {
        public int idLocacao { get; set; }
        public int idCliente { get; set; }
        public int idFilme { get; set; }
        public DateTime? dataLocacao { get; set; }
        public DateTime? dataDevolucao { get; set; }

    }
}
