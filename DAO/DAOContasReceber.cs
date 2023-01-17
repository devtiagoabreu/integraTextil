namespace DAO
{
    public class DAOContasReceber
    {
        public string Cliente { get; set; }
        public string Representante { get; set; }
        public string Duplicata { get; set; }
        public string TipoTitulo { get; set; }
        public string Portador { get; set; }
        public string Posicao { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencto { get; set; }
        public DateTime DataProrrogacao { get; set; }
        public decimal ValorDuplicata { get; set; }
        public decimal SaldoDuplicata { get; set; }
        public int Atraso { get; set; }
    }
}
