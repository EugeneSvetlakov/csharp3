using System;
using System.Windows;

using System.Net;
using System.Net.Mail;
using System.Windows.Media;

namespace WPFTestMailSender
{
    public partial class WpfMailSender
    {
        EmailSendServiceClass emailSend = new EmailSendServiceClass();

        public WpfMailSender()
        {
            InitializeComponent();
            DataContext = emailSend;
        }
        private void ButtonSendMailOnClick(object Sender, RoutedEventArgs E)
        {
            emailSend.client.Credentials = new NetworkCredential(UserNameEdit.Text, PasswordEdit.SecurePassword);
            emailSend.Send();
            MsgWindow msgWin = new MsgWindow(emailSend);
            msgWin.ShowDialog();
        }
    }
}
