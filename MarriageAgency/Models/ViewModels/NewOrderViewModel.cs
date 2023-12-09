using System.ComponentModel.DataAnnotations;

namespace MarriageAgency.Models
{
    public class NewOrderViewModel
    {

        [Required]
        [Display(Name = "Название услуги")]
        public int Name { get; set; }

        [Required]
        [Display(Name = "Сотрудник")]
        public int Employee { get; set; }

        [Required]
        [Display(Name = "Дата")]
        public DateOnly Date { get; set; }
    }
}
