namespace Model
{
    public class Estoque
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public int IdFilme { get; set; }
        public Filme Filme { get; set; }
    }
}
