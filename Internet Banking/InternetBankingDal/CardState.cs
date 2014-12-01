using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InternetBankingDal
{
    public enum CardState
    {
        [Display(Name = "Активна")]
        Active = 0,
        [Display(Name = "Заблокирована")]
        Locked = 1,
        [Display(Name = "Истек срок действия")]
        Expired = 2
    
    }
    public class EnumHelper
    {
        public SelectList SelectList
        {
            get
            {
                return new SelectList(from InternetBankingDal.CardState s in Enum.GetValues(typeof(InternetBankingDal.CardState))
                                      select new { State = (int)s, Name = GetDisplayName(s) }, "State", "Name");
            }
        }

        public string GetDisplayName(CardState State)
        {
            var type = typeof(CardState);
            var memInfo = type.GetMember(State.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute),
                false);
            return ((DisplayAttribute)attributes[0]).Name;
        }
        
    }

    //public static class EnumHelper
    //{
    //    /// <summary>
    //    /// Gets an attribute on an enum field value
    //    /// </summary>
    //    /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
    //    /// <param name="enumVal">The enum value</param>
    //    /// <returns>The attribute of type T that exists on the enum value</returns>
    //    public static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
    //    {
    //        var type = enumVal.GetType();
    //        var memInfo = type.GetMember(enumVal.ToString());
    //        var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
    //        return (attributes.Length > 0) ? (T)attributes[0] : null;
    //    }
    //}
}
