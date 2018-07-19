using System.ComponentModel.DataAnnotations;

namespace Stock.Web.Models
{
    public class GrupoProdutoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Preencha a descrição")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }
    }
}