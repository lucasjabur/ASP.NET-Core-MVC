using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using SiteVendaLanches.Models;
using SiteVendaLanches.Repository.Interfaces;
using SiteVendaLanches.ViewModel;

namespace SiteVendaLanches.Controllers {
    public class LancheController : Controller {
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository) {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List(string categoria) {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria)) {
                lanches = _lancheRepository.Lanches.OrderBy(lanche => lanche.LancheId);
                categoriaAtual = "todos os lanches";
            
            } else {
            
                //if (string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase)) {
                //    lanches = _lancheRepository.Lanches
                //        .Where(lanche => lanche.Categoria.CategoriaNome.Equals("Normal"))
                //        .OrderBy(lanche => lanche.LancheNome);
                //} else {
                //    lanches = _lancheRepository.Lanches
                //        .Where(lanche => lanche.Categoria.CategoriaNome.Equals("Natural"))
                //        .OrderBy(lanche => lanche.LancheNome);
                //}

                lanches = _lancheRepository.Lanches
                    .Where(lanche => lanche.Categoria.CategoriaNome.Equals(categoria))
                    .OrderBy(lanche => lanche.LancheNome);
                categoriaAtual = categoria;
            }

            var lanchesListViewModel = new LancheListViewModel {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View(lanchesListViewModel);

        }

        public IActionResult Details(int lancheId) {
            var lanche = _lancheRepository.Lanches.FirstOrDefault(lanche => lanche.LancheId == lancheId);
            return View(lanche);
        }

        public ViewResult Search(string searchString) {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(searchString)) {
                lanches = _lancheRepository.Lanches.OrderBy(lanche => lanche.LancheId);
                categoriaAtual = "Todos os lanches";

            } else {
                lanches = _lancheRepository.Lanches.Where(lanche => lanche.LancheNome.ToLower().Contains(searchString.ToLower()));
                if (lanches.Any()) {
                    categoriaAtual = "Lanches";
                
                } else {
                    categoriaAtual = "Nenhum lanche foi encontrado";
                }
            }

            return View("~/Views/Lanche/List.cshtml", new LancheListViewModel {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            });
        }
    }
}
