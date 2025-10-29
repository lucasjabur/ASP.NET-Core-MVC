using Microsoft.AspNetCore.Mvc;
using SiteVendaLanches.Repository.Interfaces;
using SiteVendaLanches.ViewModel;

namespace SiteVendaLanches.Controllers {
    public class LancheController : Controller {
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository) {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List() {
            //ViewData["Título"] = "Todos os Lanches";
            //ViewData["Data"] = DateTime.Now;

            //var lanches = _lancheRepository.Lanches;
            //var numLanches = lanches.Count();

            //ViewBag.Total = "Número de Lanches: ";
            //ViewBag.NumLanches = numLanches; 

            //return View(lanches);

            var lanchesListViewModel = new LancheListViewModel();
            lanchesListViewModel.Lanches = _lancheRepository.Lanches;
            lanchesListViewModel.CategoriaAtual = "Categoria Atual";

            return View(lanchesListViewModel);

        }
    }
}
