using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAOContasPagar
    {
        #region ATRIBUTOS | OBJETOS

        public string Fornecedor { get; set; }
        public string Duplicata { get; set; }
        public string TipoTitulo { get; set; }
        public string Portador { get; set; }
        public string Posicao { get; set; }
        public string CentroCusto { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataTransacao { get; set; }
        public DateTime DataVencto { get; set; }
        public decimal ValorParcela { get; set; }
        public decimal SaldoParcela { get; set; }

        #endregion

    }
}
