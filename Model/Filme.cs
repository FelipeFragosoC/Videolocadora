namespace Model
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Lancamento { get; set; }
        public string Sinopse { get; set; }
        public int IdGeneroCinematografico { get; set; }
        public int IdClassificacaoIndicativa { get; set; }
        public GeneroCinematografico GeneroCinematografico { get; set; }
        public ClassificacaoIndicativa ClassificacaoIndicativa { get; set; }
    }
}
