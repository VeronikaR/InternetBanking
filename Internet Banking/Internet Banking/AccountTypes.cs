using System;
using System.ComponentModel;
using System.Reflection;

namespace Internet_Banking
{
    public class StringValueAttribute : Attribute
    {

        #region Properties

        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string StringValue { get; protected set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }

        #endregion
        public static string GetStringValue(Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            var attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs != null && attribs.Length > 0 ? attribs[0].StringValue : null;
        }
       
    }
    public enum AccountTypes
    {
        [StringValue("Текущий счет")] CurrentAccount,
        [StringValue("Карт-счет")] CardAccount ,
        [StringValue("Депозитный счет")] DepositAccount
    }
    //public sealed class AccountTypes
    //{
    //    private readonly String name;
    //    private readonly int value;

    //    public static readonly AccountTypes CurrentAccount = new AccountTypes(1, "Текущий счет");
    //    public static readonly AccountTypes TransactionDeposit = new AccountTypes(2, "WINDOWS");
    //    public static readonly AccountTypes SavingsAccount = new AccountTypes(3, "Сберегательный счет");
    //    public static readonly AccountTypes DepositAccount = new AccountTypes(4, "Депозит");

    //    private AccountTypes(int value, String name)
    //    {
    //        this.name = name;
    //        this.value = value;
    //    }

    //    public override String ToString()
    //    {
    //        return name;
    //    }
    //}
}