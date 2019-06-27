using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MailSender.Services;
using MailSender.Data.Linq2Sql;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using System.ComponentModel;

namespace MailSender.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region MainWindowViewModel
        public MainWindowViewModel(
            IRecipientsDataService RecipientsDataService, 
            ISendersDataService SendersDataService,
            IMailServersDataService MailServersDataService,
            IMailTemplatesDataService MailTemplatesDataService)
        {
            _RecipientsDataService = RecipientsDataService;
            GetRecipientsDataCommand = new RelayCommand(OnGetRecipientsDataCommandExecuted, CanGetRecipientsDataCommandExecuted);
            CreateRecipientCommand = new RelayCommand(OnCreateRecipientCommandExecuted, CanCreateRecipientCommandExecuted);
            SaveRecipientCommand = new RelayCommand<Recipient>(OnSaveRecipientCommandExecuted, CanSaveRecipientCommandExecuted);
            DeleteRecipientCommand = new RelayCommand<Recipient>(OnDeleteRecipientCommandExecuted, CanDeleteRecipientCommandExecuted);
            //UpdateData();

            _SendersDataService = SendersDataService;
            UpdateSendersCommand = new RelayCommand(OnUpdateSendersCommandExecuted, CanUpdateSendersCommandExecuted);
            CreateSenderCommand = new RelayCommand(OnCreateSenderCommandExecuted, CanCreateRecipientCommandExecuted);
            SaveSendersCommand = new RelayCommand<Sender>(OnSaveSendersCommandExecuted, CanSaveSendersCommandExecuted);
            DeleteSenderCommand = new RelayCommand<Sender>(OnDeleteSenderCommandExecuted, CanDeleteSenderCommandExecuted);
            //GetSenders();

            _MailServersDataService = MailServersDataService;
            UpdateMailServersCommand = new RelayCommand(OnUpdateMailServersCommandExecuted, CanUpdateMailServersCommandExecuted);
            CreateMailServersCommand = new RelayCommand(OnCreateMailServersCommandExecuted, CanCreateMailServersCommandExecuted);
            SaveMailServersCommand = new RelayCommand<MailServer>(OnSaveMailServersCommandExecuted, CanSaveMailServersCommandExecuted);
            DeleteMailServerCommand = new RelayCommand<MailServer>(OnDeleteMailServerCommandExecuted, CanDeleteMailServerCommandExecuted);
            //UpdateMailServersData();

            _MailTemplatesDataService = MailTemplatesDataService;
            UpdateMailTemplatesCommand = new RelayCommand(OnUpdateMailTemplatesCommandExecuted, CanUpdateMailTemplatesCommandExecuted);
            CreateMailTemplateCommand = new RelayCommand(OnCreateMailTemplateCommandExecuted, CanCreateMailTemplateCommandExecuted);
            SaveMailTemplatesCommand = new RelayCommand<MailTemplate>(OnSaveMailTemplatesCommandExecuted, CanSaveMailTemplatesCommandExecuted);
            DeleteMailTemplateCommand = new RelayCommand<MailTemplate>(OnDeleteMailTemplateCommandExecuted, CanDeleteMailTemplateCommandExecuted);
            //GetMailTemplates();
        }
        #endregion

        #region MailWindow
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
        #endregion

        #region Recipients
        private readonly IRecipientsDataService _RecipientsDataService;
        private ObservableCollection<Recipient> _Recipients;
        
        public ObservableCollection<Recipient> Recipients
        {
            get => _Recipients;
            private set => Set(ref _Recipients, value);
        }

        private Recipient _CurrentRecipient;

        public Recipient CurrentRecipient
        {
            get => _CurrentRecipient;
            set => Set(ref _CurrentRecipient, value);
        }

        public ICommand GetRecipientsDataCommand { get; }

        private bool CanGetRecipientsDataCommandExecuted() => true;

        private void OnGetRecipientsDataCommandExecuted()
        {
            GetRecipientsData();
        }

        public ICommand CreateRecipientCommand { get; }

        private bool CanCreateRecipientCommandExecuted() => true;

        private void OnCreateRecipientCommandExecuted()
        {
            var new_recipient = new Recipient()
            {
                Name = "New Recipient",
                MailAddr = "newaddres@localhost"
            };
            _RecipientsDataService.Create(new_recipient);
            GetRecipientsData();
            CurrentRecipient = new_recipient;
        }

        public ICommand SaveRecipientCommand { get; }

        private bool CanSaveRecipientCommandExecuted(Recipient item)
        {
            return item != null && item.Name.Length > 3;
        }

        private void OnSaveRecipientCommandExecuted(Recipient item)
        {
            _RecipientsDataService.Update(item);
        }

        public ICommand DeleteRecipientCommand { get; }

        private bool CanDeleteRecipientCommandExecuted(Recipient item)
        {
            return item != null;
        }

        private void OnDeleteRecipientCommandExecuted(Recipient item)
        {
            _RecipientsDataService.Delete(item);
            GetRecipientsData();
        }

        public void GetRecipientsData()
        {
            Recipients = new ObservableCollection<Recipient>(_RecipientsDataService.GetAll());
        }
        #endregion

        #region Senders
        private readonly ISendersDataService _SendersDataService;
        private ObservableCollection<Sender> _Senders;
        public ObservableCollection<Sender> Senders
        {
            get => _Senders;
            private set => Set(ref _Senders, value);
        }
        public void GetSenders()
        {
            Senders = new ObservableCollection<Sender>(_SendersDataService.GetAll());
        }

        private Sender _CurrentSender;

        public Sender CurrentSender
        {
            get => _CurrentSender;
            set => Set(ref _CurrentSender, value);
        }

        public ICommand UpdateSendersCommand { get; }

        private bool CanUpdateSendersCommandExecuted() => true;

        private void OnUpdateSendersCommandExecuted()
        {
            GetSenders();
        }

        public ICommand CreateSenderCommand { get; }

        private bool CanCreateSenderCommandExecuted() => true;

        private void OnCreateSenderCommandExecuted()
        {
            var new_item = new Sender()
            {
                Name = "NewName",
                MailAddr = "NewAddr@localhost"
            };
            _SendersDataService.Create(new_item);
            GetSenders();
            CurrentSender = new_item;
        }

        public ICommand SaveSendersCommand { get; }

        private bool CanSaveSendersCommandExecuted(Sender item)
        {
            return item != null;
        }

        private void OnSaveSendersCommandExecuted(Sender item)
        {
            _SendersDataService.Update(item);
        }

        public ICommand DeleteSenderCommand { get; }

        private bool CanDeleteSenderCommandExecuted(Sender item)
        {
            return item != null;
        }

        private void OnDeleteSenderCommandExecuted(Sender item)
        {
            _SendersDataService.Delete(item);
            GetSenders();
        }

        #endregion

        #region MailTemplates
        private readonly IMailTemplatesDataService _MailTemplatesDataService;
        private ObservableCollection<MailTemplate> _MailTemplates;
        public ObservableCollection<MailTemplate> MailTemplates
        {
            get => _MailTemplates;
            private set => Set(ref _MailTemplates, value);
        }
        public void GetMailTemplates()
        {
            MailTemplates = new ObservableCollection<MailTemplate>(_MailTemplatesDataService.GetAll());
        }

        private MailTemplate _CurrentMailTemplate;

        public MailTemplate CurrentMailTemplate
        {
            get => _CurrentMailTemplate;
            set => Set(ref _CurrentMailTemplate, value);
        }

        public ICommand UpdateMailTemplatesCommand { get; }

        private bool CanUpdateMailTemplatesCommandExecuted() => true;

        private void OnUpdateMailTemplatesCommandExecuted()
        {
            GetMailTemplates();
        }

        public ICommand CreateMailTemplateCommand { get; }

        private bool CanCreateMailTemplateCommandExecuted() => true;

        private void OnCreateMailTemplateCommandExecuted()
        {
            var new_item = new MailTemplate()
            {
                subject = "NewSubject",
                message = "NewMessage"
            };
            _MailTemplatesDataService.Create(new_item);
            GetMailTemplates();
            CurrentMailTemplate = new_item;
        }

        public ICommand SaveMailTemplatesCommand { get; }

        private bool CanSaveMailTemplatesCommandExecuted(MailTemplate item)
        {
            return item != null;
        }

        private void OnSaveMailTemplatesCommandExecuted(MailTemplate item)
        {
            _MailTemplatesDataService.Update(item);
        }

        public ICommand DeleteMailTemplateCommand { get; }

        private bool CanDeleteMailTemplateCommandExecuted(MailTemplate item)
        {
            return item != null;
        }

        private void OnDeleteMailTemplateCommandExecuted(MailTemplate item)
        {
            _MailTemplatesDataService.Delete(item);
            GetMailTemplates();
        }

        #endregion

        #region MailServers
        private readonly IMailServersDataService _MailServersDataService;

        private ObservableCollection<MailServer> _MailServers;

        public ObservableCollection<MailServer> MailServers
        {
            get => _MailServers;
            private set => Set(ref _MailServers, value);
        }

        private MailServer _CurrentMailServer;

        public MailServer CurrentMailServer
        {
            get => _CurrentMailServer;
            set => Set(ref _CurrentMailServer, value);
        }

        public void GetMailServersData()
        {
            MailServers = new ObservableCollection<MailServer>(_MailServersDataService.GetAll());
        }

        public ICommand UpdateMailServersCommand { get; }

        private bool CanUpdateMailServersCommandExecuted() => true;

        private void OnUpdateMailServersCommandExecuted()
        {
            GetMailServersData();
        }

        public ICommand CreateMailServersCommand { get; }

        private bool CanCreateMailServersCommandExecuted() => true;

        private void OnCreateMailServersCommandExecuted()
        {
            var new_item = new MailServer()
            {
                Host = "NewHost",
                Port = 25,
                Ssl = "true"
            };
            _MailServersDataService.Create(new_item);
            GetMailServersData();
            CurrentMailServer = new_item;
        }

        public ICommand SaveMailServersCommand { get; }

        private bool CanSaveMailServersCommandExecuted(MailServer item)
        {
            return item != null;
        }

        private void OnSaveMailServersCommandExecuted(MailServer item)
        {
            _MailServersDataService.Update(item);
        }

        public ICommand DeleteMailServerCommand { get; }

        private bool CanDeleteMailServerCommandExecuted(MailServer item)
        {
            return item != null;
        }

        private void OnDeleteMailServerCommandExecuted(MailServer item)
        {
            _MailServersDataService.Delete(item);
            GetMailServersData();
        }

        #endregion
    }
}
