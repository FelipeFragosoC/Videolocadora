using System;

namespace Model
{
    public class Locacao
    {
        public int Id { get; set; }
        public DateTime DataLocacao { get; set; }
        public DateTime DataDevolucao { get; set; }
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }

    }
}
