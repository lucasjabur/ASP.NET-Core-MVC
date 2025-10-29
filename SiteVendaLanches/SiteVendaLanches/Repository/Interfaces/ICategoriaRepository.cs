using SiteVendaLanches.Models;

namespace SiteVendaLanches.Repository.Interfaces {
    public interface ICategoriaRepository {

        IEnumerable<Categoria> Categorias { get; }
    }
}
