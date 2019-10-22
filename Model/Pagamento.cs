namespace Model
{
    public class Pagamento
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public int IdLocacao { get; set; }
        public Locacao Locacao { get; set; }
    }
}
