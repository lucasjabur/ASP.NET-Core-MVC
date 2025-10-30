using SiteVendaLanches.Models;

namespace SiteVendaLanches.Repository.Interfaces {
    public interface IPedidoRepository {
        void CriarPedido(Pedido pedido);
    }
}
