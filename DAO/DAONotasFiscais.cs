using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class DAONotasFiscais
    {
        public string Emp { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string Fantasia { get; set; }
        public string Nf { get; set; }
        public string Seq { get; set; }
        public string Natureza { get; set; }
        public string Cfop { get; set; }
        public string DescNatureza { get; set; }
        public DateTime DataMovto { get; set; }
        public string EntradaSaida { get; set; }
        public string FaturamentoSimNao { get; set; }
        public string ParametroNatFat { get; set; }
        public string TipoTransacao { get; set; }
        public string CodCanc { get; set; }
        public string Produto { get; set; }
        public string DescricaoItem { get; set; }
        public string Um { get; set; }
        public decimal Qtdesaida { get; set; }
        public decimal ValorSaida { get; set; }
        public decimal UnitarioSaida { get; set; }
        public decimal QtdeEntrada { get; set; }
        public decimal ValorEntrada { get; set; }
        public decimal UnitarioEntrada { get; set; }
        public string NfOrigem { get; set; }
        public string Pedido { get; set; }
        public string CnpjTransportadora { get; set; }
        public string NomeTransportadora { get; set; }
        public string Deposito { get; set; }
        public string CentroCusto { get; set; }
        public string Transacao { get; set; }
        public string ClassificFiscal { get; set; }
        public string ClassifContabil { get; set; }
        public string CodigoContabil { get; set; }
        public decimal BaseIpi { get; set; }
        public decimal PercIpi { get; set; }
        public decimal ValorIpi { get; set; }
        public decimal CvfIpi { get; set; }
        public decimal BaseIcms { get; set; }
        public decimal PercIcms { get; set; }
        public decimal ValorIcms { get; set; }
        public decimal CvfIcms { get; set; }
        public decimal Procedencia { get; set; }
        public decimal BaseDiferenca { get; set; }
        public decimal CvfPis { get; set; }
        public decimal CvfCofins { get; set; }
        public decimal PercPis { get; set; }
        public decimal PercCofins { get; set; }
        public decimal BasePisCofins { get; set; }
        public decimal ValorPis { get; set; }
        public decimal ValorCofins { get; set; }
        public decimal PercSubtituicao { get; set; }
        public decimal BaseSubtituicao { get; set; }
        public decimal ValorSubtituicao { get; set; }
        public string Projeto { get; set; } 

    }
}
