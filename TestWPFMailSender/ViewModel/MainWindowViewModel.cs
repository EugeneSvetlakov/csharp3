using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MailSender.Services;
using MailSender.Linq2Sql;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace MailSender.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IRecipientsDataService _RecipientsDataService;
        private string _Title = "Рассыльщик почты";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        private string _Status = "Готово!";

        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }

        private ObservableCollection<Recipients> _Recipients;


        public ObservableCollection<Recipients> Recipients
        {
            get => _Recipients;
            private set => Set(ref _Recipients, value);
        }

        private Recipients _CurrentRecipient;

        public Recipients CurrentRecipient
        {
            get => _CurrentRecipient;
            set => Set(ref _CurrentRecipient, value);
        }

        public ICommand UpdateDataCommand { get; }

        public ICommand CreateRecipientCommand { get; }

        public ICommand SaveRecipientCommand { get; }

        public MainWindowViewModel(IRecipientsDataService RecipientsDataService)
        {
            _RecipientsDataService = RecipientsDataService;
            UpdateDataCommand = new RelayCommand(OnUpdateDataCommandExecuted, CanUpdateDataCommandExecuted);
            CreateRecipientCommand = new RelayCommand(OnCreateRecipientCommandExecuted, CanCreateRecipientCommandExecuted);
            SaveRecipientCommand = new RelayCommand<Recipients>(OnSaveRecipientCommandExecuted, CanSaveRecipientCommandExecuted);
            //UpdateData();
        }

        private bool CanUpdateDataCommandExecuted() => true;

        private void OnUpdateDataCommandExecuted()
        {
            UpdateData();
        }

        private bool CanCreateRecipientCommandExecuted() => true;

        private void OnCreateRecipientCommandExecuted()
        {
            var new_recipient = new Recipients()
            {
                Name = "New Recipient",
                MailAddr = "newaddres@localhost"
            };
            _RecipientsDataService.Create(new_recipient);
            _Recipients.Add(new_recipient);
            CurrentRecipient = new_recipient;
        }

        private bool CanSaveRecipientCommandExecuted(Recipients recipient)
        {
            //return recipient != null;
            return true;
        }

        private void OnSaveRecipientCommandExecuted(Recipients recipient)
        {
            _RecipientsDataService.Update(recipient);
        }

        public void UpdateData()
        {
            Recipients = new ObservableCollection<Recipients>(_RecipientsDataService.GetAll());

        }
    }
}
