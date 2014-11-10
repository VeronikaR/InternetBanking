using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InternetBankingDal;

namespace Internet_Banking.Models
{
    public class SummaryAccountsModel
    {
        //	Название счета (тип счета)
        //	Номер счета
        //	Признак наличия выпущенной и активной карты к счету //TODO

        public Guid AccountId { get; set; }

        [Required]
        [Display(Name = "Название счета:")]
        public string Type { get; set; }
        [Required]
        [Display(Name = "Номер счета:")]
        public string Number { get; set; }
        [Required]
        [Display(Name = "Активная карта:")]
        public bool ActiveCard { get; set; }
    }

    public class AccountDetailModel
    {
        //	Название счета (тип счета)
        //	Номер счета
        //	Доступный остаток на счете 
        //	Валюта счета
        //	Признак наличия выпущенной и активной карты к счету
        //	Дата открытия
        //	Установленный лимит овердрафта

        public Guid AccountId { get; set; }

        [Required]
        [Display(Name = "Название счета:")]
        public string Type { get; set; }
        [Required]
        [Display(Name = "Номер счета:")]
        public string Number { get; set; }
        [Required]
        [Display(Name = "Доступный остаток на счете:")]
        public decimal Ammount { get; set; }
        [Required]
        [Display(Name = "Валюта:")]
        public int Currency { get; set; }
        [Required]
        [Display(Name = "Дата открытия:")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "Установленный лимит овердрафта:")]
        public decimal? OverdraftLimit { get; set; }

        [Required]
        [Display(Name = "Карты, привязанные к этому счету:")]
        public virtual ICollection<Cards> Cards { get; set; }
    }
}