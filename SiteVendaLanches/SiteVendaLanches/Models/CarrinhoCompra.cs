using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteVendaLanches.Context;

namespace SiteVendaLanches.Models {
    public class CarrinhoCompra {

        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context) {
            _context = context;
        }

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services) {

            // define uma sessão
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            // obtem um serviço do tipo do nosso contexto
            var context = services.GetService<AppDbContext>();

            // obtem ou gera o Id do carrinho
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            // atribui o id do carrinho na sessão
            session.SetString("CarrinhoId", carrinhoId);

            return new CarrinhoCompra(context) { CarrinhoCompraId = carrinhoId };
        }

        public void AdicionarAoCarrinho(Lanche lanche) {
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                    temp => temp.Lanche.LancheId == lanche.LancheId &&
                            temp.CarrinhoCompraId == CarrinhoCompraId
                );

            if (carrinhoCompraItem == null) {
                carrinhoCompraItem = new CarrinhoCompraItem {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };
                _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            
            } else {
                carrinhoCompraItem.Quantidade++;
            }

            _context.SaveChanges();
        }

        public int RemoverItemDoCarrinho(Lanche lanche) {
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                    temp => temp.Lanche.LancheId == lanche.LancheId &&
                            temp.CarrinhoCompraId == CarrinhoCompraId
                );

            var quantidadeLocal = 0;

            if (carrinhoCompraItem != null) {
                if (carrinhoCompraItem.Quantidade > 1) {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                } else {
                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }
            _context.SaveChanges();

            return quantidadeLocal;
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens() {
            return CarrinhoCompraItens ?? (CarrinhoCompraItens =
                        _context.CarrinhoCompraItens
                        .Where(temp => temp.CarrinhoCompraId == CarrinhoCompraId)
                        .Include(temp => temp.Lanche).ToList());
        }

        public void LimparCarrinho() {
            var carrinhoItens = _context.CarrinhoCompraItens.Where(car => car.CarrinhoCompraId == CarrinhoCompraId);
            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
            _context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal() {
            var total = _context.CarrinhoCompraItens
                .Where(car => car.CarrinhoCompraId == CarrinhoCompraId)
                .Select(car => car.Lanche.Preco * car.Quantidade)
                .Sum();

            return total;
        }
    }
}
