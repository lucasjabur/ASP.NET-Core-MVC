using Microsoft.AspNetCore.Mvc;
using SiteVendaLanches.Services;

namespace SiteVendaLanches.Areas.Admin.Controllers {

    [Area("Admin")]
    public class AdminGraficoController : Controller {

        private readonly GraficoVendasService _graficoVendas;

        public AdminGraficoController(GraficoVendasService graficoVendas) {
            _graficoVendas = graficoVendas;
        }

        public JsonResult VendasLanches(int dias) {
            var lanchesVendasTotais = _graficoVendas.GetVendasLanche(dias);
            return Json(lanchesVendasTotais);
        }

        public IActionResult Index(int dias) {
            return View();
        }

        public IActionResult VendasMensais(int dias) {
            return View();
        }

        public IActionResult VendasSemanais(int dias) {
            return View();
        }
    }
}
