using System;

namespace Domain.Model.Request
{
    public class ClienteRequest
    {
        public string nome { get; set; }
        public string cpf { get; set; }
        public DateTime? dataNascimento { get; set; }
    }
}
