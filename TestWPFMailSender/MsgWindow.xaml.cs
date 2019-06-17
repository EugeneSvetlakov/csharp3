﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFTestMailSender
{
    /// <summary>
    /// Логика взаимодействия для MsgWindow.xaml
    /// </summary>
    public partial class MsgWindow : Window
    {
        public MsgWindow(EmailSendServiceClass o)
        {
            InitializeComponent();
            this.tbMsg.Text = $"SendStatus is {o.result}. Status message: {o.errMsg}";
            if (o.result == sendStatus.Error)
            {
                this.tbMsg.Foreground = Brushes.Red;
            }
        }
        public MsgWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
