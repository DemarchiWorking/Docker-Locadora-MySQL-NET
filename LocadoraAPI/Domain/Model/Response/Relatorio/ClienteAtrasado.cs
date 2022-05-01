using System;

namespace Domain.Model.Dao
{
    public class ClienteAtrasadoReturn
    {
        public int idLocacao { get; set; }
        public int idFilme { get; set; }
        public string titulo { get; set; }
        public int idCliente { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public int tempoAtraso { get; set; }
        public DateTime? dataNascimento { get; set; }
        public DateTime? dataLocacao { get; set; }
        public DateTime? dataDevolucao { get; set; }

    }
}
