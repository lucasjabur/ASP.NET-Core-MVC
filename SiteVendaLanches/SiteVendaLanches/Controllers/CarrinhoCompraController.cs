using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteVendaLanches.Models;
using SiteVendaLanches.Repository.Interfaces;
using SiteVendaLanches.ViewModel;
using System.Runtime.CompilerServices;

namespace SiteVendaLanches.Controllers {
    public class CarrinhoCompraController : Controller {

        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra) {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index() {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItens = itens;
            var carrinhoCompraVM = new CarrinhoCompraViewModel {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };

            return View(carrinhoCompraVM);
        }

        [Authorize]
        public RedirectToActionResult AdicionarItemNoCarrinhoCompra(int lancheId) {
            var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(lanche => lanche.LancheId == lancheId);

            if (lancheSelecionado != null) {
                _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public RedirectToActionResult RemoverItemNoCarrinhoCompra(int lancheId) {
            var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(lanche => lanche.LancheId == lancheId);

            if (lancheSelecionado != null) {
                _carrinhoCompra.RemoverItemDoCarrinho(lancheSelecionado);
            }

            return RedirectToAction("Index");
        }
    }
}
