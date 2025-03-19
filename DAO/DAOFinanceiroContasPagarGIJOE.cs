using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAOFinanceiroContasPagarGIJOE
    {
        #region ATRIBUTOS | OBJETOS
        public string Empresa { get; set; }
        public int Duplicata { get; set; }
        public string Parcela { get; set; }
        public DateTime DataContrato { get; set; }
        public string TipoTitulo { get; set; }
        public int Documento { get; set; }
        public string Serie { get; set; }
        public string Historico { get; set; }
        public string EmpresaCobranca { get; set; }
        public string CodContabil { get; set; }
        public string CodFornecedor { get; set; }
        public string NomeFornecedor { get; set; }
        public string TipoFornecedor { get; set; }
        public DateTime DataTransacao { get; set; }
        public string Previsao { get; set; }
        public int Portador { get; set; }
        public string VencimentoOrig { get; set; }
        public DateTime Vencimento { get; set; }
        public string Posicao { get; set; }
        public int NumContabil { get; set; }
        public string OrigemDebito { get; set; }
        public string SituacaoTitulo { get; set; }
        public string SituacaoSispag { get; set; }
        public string TipoPagamento { get; set; }
        public string CodigoBarras { get; set; }
        public string Moeda { get; set; }
        public decimal ValorTitulo { get; set; }
        public decimal SaldoTitulo { get; set; }
        public int Tran { get; set; }
        public string Transacao { get; set; }
        public int CCusto { get; set; }
        public string CentroCusto { get; set; }
        public int CentroCustoPai { get; set; }
        public decimal ValorCcusto { get; set; }
        public string CodContabilRateio { get; set; }
        public string MesAnoVencimento { get; set; }
        public string Pd { get; set; }
        public string Previsao2 { get; set; }
        public string CentrosCustoTecelagem { get; set; }
        public string CentrosCustoValorPonto { get; set; }
        public string CentrosCustoBeneficiamento { get; set; }

        #endregion

    }
}
