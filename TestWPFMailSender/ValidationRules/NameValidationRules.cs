using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MailSender.ValidationRules
{
    public class NameValidationRules
    {
        
        public void OnNameValidationError(object sender, ValidationErrorEventArgs e)
        {
            var error_control = (Control)e.OriginalSource;
            if (e.Action == ValidationErrorEventAction.Added)
            {
                error_control.ToolTip = e.Error.ErrorContent.ToString();
            }
            else
            {
                error_control.ToolTip = DependencyProperty.UnsetValue;
            }
        }
    }
}
