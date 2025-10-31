using SiteVendaLanches.Models;
using SiteVendaLanches.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SiteVendaLanches.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository,
            CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout(Pedido pedido) {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;

            //obtem os itens do carrinho de compra do cliente
            List<CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItens = items;

            //verifica se existem itens de pedido
            if (_carrinhoCompra.CarrinhoCompraItens.Count == 0) {
                ModelState.AddModelError("", "Seu carrinho esta vazio, que tal incluir um lanche...");
            }

            //calcula o total de itens e o total do pedido
            foreach (var item in items) {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Lanche.Preco * item.Quantidade);
            }

            //atribui os valores obtidos ao pedido
            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;

            ModelState.Remove("PedidoEnviado");
            ModelState.Remove("PedidoEntregueEm");
            ModelState.Remove("PedidoItens");
            ModelState.Remove("TotalItensPedido");
            ModelState.Remove("PedidoTotal");

            //valida os dados do pedido
            if (ModelState.IsValid) {
                // Define a data do pedido
                pedido.PedidoEnviado = DateTime.Now;

                //cria o pedido e os detalhes
                _pedidoRepository.CriarPedido(pedido);

                // Popula a lista de PedidoItens para exibir na view
                pedido.PedidoItens = new List<PedidoDetalhe>();

                foreach (var item in items) {
                    var pedidoDetalhe = new PedidoDetalhe {
                        Quantidade = item.Quantidade,
                        LancheId = item.Lanche.LancheId,
                        Preco = item.Lanche.Preco,
                        Lanche = item.Lanche
                    };
                    pedido.PedidoItens.Add(pedidoDetalhe);
                }

                //define mensagens ao cliente
                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

                //limpa o carrinho do cliente
                _carrinhoCompra.LimparCarrinho();

                //exibe a view com dados do cliente e do pedido
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }

            foreach (var key in ModelState.Keys) {
                var errors = ModelState[key].Errors;
                if (errors.Count > 0) {
                    foreach (var error in errors) {
                        Console.WriteLine($"Campo: {key} - Erro: {error.ErrorMessage}");
                    }
                }
            }

            return View(pedido);
        }
    }
}
