using System.ComponentModel.DataAnnotations;

namespace MarriageAgency.Models
{
    public class ProvidedServicesViewModel
    {
        public PaginationViewModel Pagination { get; set; }


        [Display(Name = "Сотрудник")]
        public int EmployeeId { get; set; }


        [Display(Name = "Клиент")]
        public string UserName { get; set; }


        [Display(Name = "Оказанная услуга")]
        public int ServiceId { get; set; }
    }
}
