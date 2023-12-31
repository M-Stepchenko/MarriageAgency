﻿using System.ComponentModel.DataAnnotations;

namespace MarriageAgency.Models
{
    public class InfoEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Место работы")]
        public string Job { get; set; }

        [Required]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Хобби")]
        public string? Hobby { get; set; }

        [Display(Name = "Вредные привычки")]
        public string? BadHabbits { get; set; }
        
        [Display(Name = "Паспортные данные")]
        public string? Passport { get; set; }

        [Required]
        [Display(Name = "Знак зодиака")]
        public int ZodiacSign { get; set; }

        [Required]
        [Display(Name = "Национальность")]
        public int Nationality { get; set; }

        [Required]
        [Display(Name = "Семейное положение")]
        public int FamilyStatus { get; set; }

        [Display(Name = "Адрес")]
        public string? Address { get; set; }
        
        [Display(Name = "Искомый партнер")]
        public string? DesiredPartner { get; set; }

        [Required]
        [Display(Name = "Рост")]
        public int Height { get; set; }

        [Required]
        [Display(Name = "Вес")]
        public int Weight { get; set; }

        [Required]
        [Display(Name = "Количество детей")]
        public int Children { get; set; }

        [Required]
        [Display(Name = "Пол")]
        public string Sex { get; set; }

        [Required]
        [Display(Name = "Дата рождения")]
        public DateOnly Bithdate { get; set; }
    }
}
