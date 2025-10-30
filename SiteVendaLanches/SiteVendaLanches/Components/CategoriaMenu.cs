using Microsoft.AspNetCore.Mvc;
using SiteVendaLanches.Repository.Interfaces;

namespace SiteVendaLanches.Components {
    public class CategoriaMenu :ViewComponent {

        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaMenu(ICategoriaRepository categoriaRepository) {
            _categoriaRepository = categoriaRepository;
        }

        public IViewComponentResult Invoke() {
            var categorias = _categoriaRepository.Categorias.OrderBy(cat => cat.CategoriaNome);

            return View(categorias);
        }
    }
}
