namespace Model
{
    public class Telefone
    {
        public int Id { get; set; }
        public string Residencial { get; set; }
        public string Celular { get; set; }
        public string Comercial { get; set; }
        public string Recado { get; set; }
        /** Transient **/
        public Cliente Cliente { get; set; }
    }
}
