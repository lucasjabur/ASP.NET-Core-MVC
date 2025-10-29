using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace SiteVendaLanches.Models
{
    [Table("Lanche")]
    public class Lanche
    {
        [Key]
        public int LancheId { get; set; }

        [StringLength(80, MinimumLength = 10, ErrorMessage = " O {0} deve ter no mínimo {1} e no máximo {2} caracteres!")]
        [Required(ErrorMessage = "Informe o nome do lanche!")]
        [Display(Name = "Nome do Lanche")]
        public string LancheNome { get; set; }

        [Required(ErrorMessage = "Informe a descrição do lanche!")]
        [Display(Name = "Descrição do Lanche")]
        [MinLength(20, ErrorMessage = "A descrição do lanche deve ter no mínimo {1} caracteres!")]
        [MaxLength(200, ErrorMessage = "A descrição do lanche não pode exceder {1} caracteres!")]
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = "Informe a descrição detalhada do lanche!")]
        [Display(Name = "Descrição do Lanche")]
        [MinLength(20, ErrorMessage = "A descrição detalhada do lanche deve ter no mínimo {1} caracteres!")]
        [MaxLength(200, ErrorMessage = "A descrição detalhada  do lanche não pode exceder {1} caracteres!")]
        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "Informe ao preço do lanche!")]
        [Display(Name = "Preço")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1, 999.99, ErrorMessage = "O preço deve estar entre R$ 1,00 e R$ 999,99!")]
        public decimal Preco { get; set; }

        [Display(Name = "Caminho da Imagem")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = " O {0} deve ter no máximo {1} caracteres!")]
        public string ImagemUrl { get; set; }

        [Display(Name = "Caminho da Imagem Miniatura")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = " O {0} deve ter no máximo {1} caracteres!")]
        public string ImagemThumbnailUrl { get; set; }

        [Display(Name = "Preferido")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }

        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
