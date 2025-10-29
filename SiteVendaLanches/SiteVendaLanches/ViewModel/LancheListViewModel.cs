using SiteVendaLanches.Models;

namespace SiteVendaLanches.ViewModel {
    public class LancheListViewModel {
        public IEnumerable<Lanche> Lanches { get; set; }
        public string CategoriaAtual {  get; set; }
    }
}
