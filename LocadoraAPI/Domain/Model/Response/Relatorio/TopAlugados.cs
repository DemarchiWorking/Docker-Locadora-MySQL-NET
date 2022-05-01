using System;

namespace Domain.Model.Dao
{
    public class TopAlugados
    {
        public int idFilme { get; set; }
        public string titulo { get; set; }
        public int classificacaoIndicativa { get; set; }
        public bool lancamento { get; set; }
        public int vendas { get; set; }

    }
}
