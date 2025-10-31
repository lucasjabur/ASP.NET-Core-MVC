using Microsoft.AspNetCore.Mvc;
using SiteVendaLanches.Areas.Admin.Services;
using System.Runtime.CompilerServices;

namespace SiteVendaLanches.Areas.Admin.Controllers {
    [Area("Admin")]
    public class AdminRelatorioVendasController : Controller {

        private readonly RelatorioVendasService _relatorioVendasService;

        public AdminRelatorioVendasController(RelatorioVendasService relatorioVendasService) {
            _relatorioVendasService = relatorioVendasService;
        }

        public IActionResult Index() {
            return View();
        }

        public async Task<IActionResult> RelatorioVendasSimples(DateTime? minDate, DateTime? maxDate) {
            if (!minDate.HasValue) {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!maxDate.HasValue) {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var resultado = await _relatorioVendasService.FindByDateAsync(minDate, maxDate);

            return View(resultado);
        }
    }
}
