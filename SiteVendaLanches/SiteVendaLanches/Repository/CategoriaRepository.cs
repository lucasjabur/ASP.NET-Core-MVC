using SiteVendaLanches.Context;
using SiteVendaLanches.Models;
using SiteVendaLanches.Repository.Interfaces;

namespace SiteVendaLanches.Repository {
    public class CategoriaRepository : ICategoriaRepository {

        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context) {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
