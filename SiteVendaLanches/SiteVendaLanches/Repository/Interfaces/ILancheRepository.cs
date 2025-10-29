using SiteVendaLanches.Models;

namespace SiteVendaLanches.Repository.Interfaces {
    public interface ILancheRepository {

        IEnumerable<Lanche> Lanches { get; }
        IEnumerable<Lanche> LanchesPreferidos { get; }
        Lanche GetLancheById(int lancheId);
    }
}
