using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAOContasPagas
    {
        public string Fornecedor { get; set; }
        public string Duplicata { get; set; }
        public string TipoTitulo { get; set; }
        public string Portador { get; set; }        
        public string Posicao { get; set; }
        public string CentroCusto { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencto { get; set; }
        public DateTime DataPagto { get; set; }
        public decimal ValorParcela { get; set; }
        public decimal ValorPago { get; set; }
        public decimal ValorJuros { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorAbatido { get; set; }
        public decimal SaldoParcela { get; set; }


    }
}
