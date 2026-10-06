using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mf_dev_backend_2026.Models
{
    [Table("Veículos")]
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Obrigatório informar o nome.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Obrigatório informar a placa.")]
        public string Placa { get; set; }
        [Required(ErrorMessage = "Obrigatório informar o ano de fabricação.")]
        [Display(Name = "Ano de Fabricação")]
        public int AnoFabricacao { get; set; }

        [Display(Name = "Ano do Modelo")]
        [Required(ErrorMessage = "Obrigatório informar o ano do modelo.")]
        public int AnoModelo { get; set; }
    }
}
