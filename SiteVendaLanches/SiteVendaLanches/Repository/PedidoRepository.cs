using SiteVendaLanches.Context;
using SiteVendaLanches.Models;
using SiteVendaLanches.Repository.Interfaces;

namespace SiteVendaLanches.Repository {
    public class PedidoRepository : IPedidoRepository {

        private readonly AppDbContext _appDbContext;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoRepository(AppDbContext appDbContext, CarrinhoCompra carrinhoCompra) {
            _appDbContext = appDbContext;
            _carrinhoCompra = carrinhoCompra;
        }

        public void CriarPedido(Pedido pedido) {
            pedido.PedidoEnviado = DateTime.Now;
            _appDbContext.Pedidos.Add(pedido);
            _appDbContext.SaveChanges();

            var carrinhoCompraItens = _carrinhoCompra.CarrinhoCompraItens;

            foreach (var item in carrinhoCompraItens) {
                var pedidoDetalhe = new PedidoDetalhe() {
                    Quantidade = item.Quantidade,
                    LancheId = item.Lanche.LancheId,
                    PedidoId = pedido.PedidoId,
                    Preco = item.Lanche.Preco
                };
                _appDbContext.PedidoDetalhes.Add(pedidoDetalhe);
            }
            _appDbContext.SaveChanges();
        }
    }
}
