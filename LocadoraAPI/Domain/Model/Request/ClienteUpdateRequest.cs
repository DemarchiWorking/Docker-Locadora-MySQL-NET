using System;

namespace Domain.Model.Request
{
    public class ClienteUpdateRequest
    {
        public int idCliente { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public DateTime? dataNascimento { get; set; }
    }
}
