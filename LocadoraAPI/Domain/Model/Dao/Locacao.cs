using System;

namespace Domain.Model.Dao
{
    public class Locacao
    {
        public int idCliente { get; set; }
        public int idFilme { get; set; }
        public DateTime? dataLocacao { get; set; }
        public DateTime? dataDevolucao { get; set; }

    }
}
