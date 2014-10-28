using System;
using System.ComponentModel.DataAnnotations;

namespace Internet_Banking.Models
{
    public class SummaryCardsModel
    {
        //	Название карты (например, Visa Classic)
        //	Номер карты
        //	Тип счета, к которому привязана карта
        //	ФИ держателя латиницей 
        //	Статус карты («Активна»/«Неактивна)»
        public Guid CardId { get; set; }

        
        [Required]
        [Display(Name = "Название карты:")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Номер карты:")]
        public string Number { get; set; }
        [Required]
        [Display(Name = "Тип счета:")]
        public string Type { get; set; }
        [Required]
        [Display(Name = "ФИ держателя:")]	
        public string UserSignature { get; set; }
        [Required]
        [Display(Name = "Статус карты:")]
        public int State { get; set; }
    }

    public class CardDetailModel
    {
        //	Название карты (например Visa Classic)
        //	Номер карты
        //	Тип счета, к которому привязана карта 
        //	Номер счета, к которому привязана карта
        //	Дата выпуска карты 
        //	Срок окончания действия карты
        //	Ф.И. держателя латиницей 
        //	Статус карты («Активна» либо, если в предыдущем разделе «Неактивна», расшифровываем: «Истек срок действия»/«Заблокирована»)

        public Guid CardId { get; set; }
        public Guid AccountId { get; set; }

        [Required]
        [Display(Name = "Название карты:")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Номер карты:")]
        public string Number { get; set; }
        [Required]
        [Display(Name = "Тип счета:")]
        public string AccountType { get; set; }
        [Required]
        [Display(Name = "Номер счета:")]
        public string AccountNumber { get; set; }
        [Required]
        [Display(Name = "Дата выпуска:")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "Срок окончания действия:")]
        public DateTime EndDate { get; set; }
        [Required]
        [Display(Name = "ФИ держателя:")]
        public string UserSignature { get; set; }
        [Required]
        [Display(Name = "Статус карты:")]
        public int State { get; set; }
    }
}