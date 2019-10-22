namespace Model
{
    public class RelLocacaoFilme
    {
        public int Id { get; set; }
        public int IdLocacao { get; set; }
        public int IdFilme { get; set; }
        public Locacao Locacao { get; set; }
        public Filme Filme { get; set; }
    }
}
