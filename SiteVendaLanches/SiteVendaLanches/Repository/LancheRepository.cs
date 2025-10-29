using Microsoft.EntityFrameworkCore;
using SiteVendaLanches.Context;
using SiteVendaLanches.Models;
using SiteVendaLanches.Repository.Interfaces;


namespace SiteVendaLanches.Repository {
    public class LancheRepository : ILancheRepository {
        private readonly AppDbContext _context;

        public LancheRepository(AppDbContext context) {
            _context = context;
        }

        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(cat => cat.Categoria);
        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.Where(pref => pref.IsLanchePreferido).Include(cat => cat.Categoria);
        public Lanche GetLancheById(int lancheId) => _context.Lanches.FirstOrDefault(lanche => lanche.LancheId == lancheId);
        // uso de Expression Bodied Member (uso de lambda para tornar o código menos verboso)
    }
}
