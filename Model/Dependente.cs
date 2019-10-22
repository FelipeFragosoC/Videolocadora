namespace Model
{
    public class Dependente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
    }
}
