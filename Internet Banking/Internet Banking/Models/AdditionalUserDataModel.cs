using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Internet_Banking.Models
{
    public class AdditionalUserDataModel
    {
        const string FioTemplateString = "{0} {1} {2}";

        public string Password { get; set; }
        public string FIO { get { return string.Format(FioTemplateString, LastName, FirstName, MiddleName); } }

        public Guid UserId { get; set; }

        [Required]
        [Display(Name = "Логин:")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Фамилия (кириллица):")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Имя (кириллица):")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Отчество (кириллица):")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Дата рождения (формат даты dd.mm.yyyy):")]
        public String BirthDate { get; set; }

        [Required]
        [Display(Name = "Гражданство:")]
        public string Nationality { get; set; }

        [Required]
        [Display(Name = "Идентификационный номер паспорта:")]
        public string IdentificationNumber { get; set; }

        [Required]
        [Display(Name = "Номер паспорта:")]
        public string PassportNumber { get; set; }

        public AdditionalUserDataModel() {
            UserId = Guid.Empty;
        }
    }
}