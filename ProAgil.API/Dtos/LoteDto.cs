using System;

namespace ProAgil.API.Dtos
{
    public class LoteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public DateTime? DataIncio { get; set; }
        public DateTime? DataFim { get; set; }
        public int Quantidade { get; set; }

    }
}