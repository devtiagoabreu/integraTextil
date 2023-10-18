using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAOPedidoCompra
    {
        public string PcNumPedCompra { get; set; }
        public string PcSeqItemPedido { get; set; }
        public string PcItem100Nivel99 { get; set; }
        public string PcItem100Grupo { get; set; }
        public string PcItem100Subgrupo { get; set; }
        public string PcItem100Item { get; set; }
        public string PcDescricaoItem { get; set; }
        public decimal PcQtdePedidaItem { get; set; }
        public string PcUnidadeMedida { get; set; }
        public decimal PcPrecoItemComp { get; set; }
    }
}
