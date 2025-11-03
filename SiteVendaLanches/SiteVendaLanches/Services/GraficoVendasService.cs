using SiteVendaLanches.Context;
using SiteVendaLanches.Models;

namespace SiteVendaLanches.Services {
    public class GraficoVendasService {
        private readonly AppDbContext _context;

        public GraficoVendasService(AppDbContext context) {
            _context = context;
        }

        public List<LancheGrafico> GetVendasLanche(int dias = 360) {
            var data = DateTime.Now.AddDays(-dias);
            var lanches = (
                from pd in _context.PedidoDetalhes
                join lan in _context.Lanches on pd.LancheId equals lan.LancheId
                where pd.Pedido.PedidoEnviado >= data
                group pd by new { pd.LancheId, lan.LancheNome, pd.Quantidade }
                into g
                select new { // gera um tipo anônimo
                    LancheNome = g.Key.LancheNome,
                    LanchesQuantidade = g.Sum( quant => quant.Quantidade ),
                    LanchesValorTotal = g.Sum( quant => quant.Quantidade * quant.Preco)
                });

            var lista = new List<LancheGrafico>();
            foreach (var item in lanches) {
                var lanche = new LancheGrafico();
                lanche.LancheNome = item.LancheNome;
                lanche.LanchesQuantidade = item.LanchesQuantidade;
                lanche.LanchesValorTotal = item.LanchesValorTotal;
                lista.Add( lanche );
            }
            return lista;
        }
    }
}
