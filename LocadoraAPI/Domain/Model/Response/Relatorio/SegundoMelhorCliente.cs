using System;

namespace Domain.Model.Dao
{
    public class SegundoMelhorCliente
    {
        public int idCliente { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public DateTime? dataNascimento { get; set; }
        public long vendas { get; set; }


    }
}
