using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteVendaLanches.Models
{

    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [StringLength(100, ErrorMessage = "{0} não pode exceder 100 caracteres!")]
        [Required(ErrorMessage = "Informe o nome da categoria!")]
        [Display(Name = "Nome da Categoria")]
        public string CategoriaNome { get; set; }

        [StringLength(200, ErrorMessage = "{0} não pode exceder 200 caracteres!")]
        [Required(ErrorMessage = "Informe a descrição da categoria!")]
        [Display(Name = "Descrição da Categoria")]
        public string Descricao { get; set; }

        public List<Lanche> Lanches { get; set; }
    }
}
