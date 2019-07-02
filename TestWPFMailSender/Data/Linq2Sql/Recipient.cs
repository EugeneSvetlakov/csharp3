using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.Linq2Sql
{
    public partial class Recipient : IDataErrorInfo
    {
        partial void OnNameChanging(string value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value), "Передана пустая строка");

        }

        string IDataErrorInfo.this[string PropertyName]
        {
            get
            {
                switch (PropertyName)
                {
                    case nameof(Name):
                        if (Name is null) return "пустая строка";
                        if (Name.Length < 2) return "Имя меньше 2 символов";
                        if (!char.IsUpper(Name[0])) return "Имя начинается не с Прописной буквы";
                        break;
                }
                return "";
            }
        }

        string IDataErrorInfo.Error => "";
    }
}
