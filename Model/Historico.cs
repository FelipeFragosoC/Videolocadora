using System;

namespace Model
{
    public class Historico
    {
        public int Id { get; set; }
        public DateTime DataInclusao { get; set; }
        public int IdPagamento { get; set; }
        public int IdCliente { get; set; }
        public int IdLocacao { get; set; }
        public Pagamento Pagamento { get; set; }
        public Cliente Cliente { get; set; }
        public Locacao Locacao { get; set; }
    }
}
