using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MailSender.Services;
using MailSender.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using System.ComponentModel;
using System.IO;

namespace MailSender.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region MainWindowViewModel
        public MainWindowViewModel(
            IRecipientsDataService RecipientsDataService
            ,ISendersDataService SendersDataService
            ,IServersDataService MailServersDataService
            ,IMailMessageDataService MailTemplatesDataService
            //,IMailSenderService MailSenderService
            )
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
            SaveMailServersCommand = new RelayCommand<Server>(OnSaveMailServersCommandExecuted, CanSaveMailServersCommandExecuted);
            DeleteMailServerCommand = new RelayCommand<Server>(OnDeleteMailServerCommandExecuted, CanDeleteMailServerCommandExecuted);
            //UpdateMailServersData();

            _MailTemplatesDataService = MailTemplatesDataService;
            UpdateMailTemplatesCommand = new RelayCommand(OnUpdateMailTemplatesCommandExecuted, CanUpdateMailTemplatesCommandExecuted);
            CreateMailTemplateCommand = new RelayCommand(OnCreateMailTemplateCommandExecuted, CanCreateMailTemplateCommandExecuted);
            SaveMailTemplatesCommand = new RelayCommand<MailMessage>(OnSaveMailTemplatesCommandExecuted, CanSaveMailTemplatesCommandExecuted);
            DeleteMailTemplateCommand = new RelayCommand<MailMessage>(OnDeleteMailTemplateCommandExecuted, CanDeleteMailTemplateCommandExecuted);
            //GetMailTemplates();

            //_MailSenderService = MailSenderService;
            //todo Commands for MailSenderService

            //Отчеты
            CreateReportRecipientsCommand = new RelayCommand(OnCreateReportRecipientsCommand, CanCreateReportRecipientsCommand);
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

        #region Отчеты

        public ICommand CreateReportRecipientsCommand { get; }

        private bool CanCreateReportRecipientsCommand() => !(Recipients is null);

        private void OnCreateReportRecipientsCommand()
        {
            //todo
            string str = "Список получателей писем: ";
            int counter = 0;
            foreach (var item in Recipients)
            {
                str += $"{counter++}) ID: {item.id} Name: {item.Name} Address: {item.Address}; ";
            }

            var report = new Reports.Report
            {
                Data1 = DateTime.Now.ToLongDateString(),
                Data2 = str
            };

            string Path = @"..\..\ReportFiles";
            string date1 = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
            report.CreatePackage($"{Path}\\Report_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.docx");
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
                Address = "newaddres@localhost",
                Comment = ""
            };
            _RecipientsDataService.Add(new_recipient);
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
            _RecipientsDataService.Edit(item.id, item);
        }

        public ICommand DeleteRecipientCommand { get; }

        private bool CanDeleteRecipientCommandExecuted(Recipient item)
        {
            return item != null;
        }

        private void OnDeleteRecipientCommandExecuted(Recipient item)
        {
            _RecipientsDataService.Delete(item.id);
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
                Address = "NewAddr@localhost",
                Comment = ""
            };
            _SendersDataService.Add(new_item);
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
            _SendersDataService.Edit(item.id, item);
        }

        public ICommand DeleteSenderCommand { get; }

        private bool CanDeleteSenderCommandExecuted(Sender item)
        {
            return item != null;
        }

        private void OnDeleteSenderCommandExecuted(Sender item)
        {
            _SendersDataService.Delete(item.id);
            GetSenders();
        }

        #endregion

        #region MailTemplates
        private readonly IMailMessageDataService _MailTemplatesDataService;
        private ObservableCollection<MailMessage> _MailTemplates;
        public ObservableCollection<MailMessage> MailTemplates
        {
            get => _MailTemplates;
            private set => Set(ref _MailTemplates, value);
        }
        public void GetMailTemplates()
        {
            MailTemplates = new ObservableCollection<MailMessage>(_MailTemplatesDataService.GetAll());
        }

        private MailMessage _CurrentMailTemplate;

        public MailMessage CurrentMailTemplate
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
            var new_item = new MailMessage()
            {
                Subject = "NewSubject",
                Body = "NewMessage"
            };
            _MailTemplatesDataService.Add(new_item);
            GetMailTemplates();
            CurrentMailTemplate = new_item;
        }

        public ICommand SaveMailTemplatesCommand { get; }

        private bool CanSaveMailTemplatesCommandExecuted(MailMessage item)
        {
            return item != null;
        }

        private void OnSaveMailTemplatesCommandExecuted(MailMessage item)
        {
            _MailTemplatesDataService.Edit(item.id, item);
        }

        public ICommand DeleteMailTemplateCommand { get; }

        private bool CanDeleteMailTemplateCommandExecuted(MailMessage item)
        {
            return item != null;
        }

        private void OnDeleteMailTemplateCommandExecuted(MailMessage item)
        {
            _MailTemplatesDataService.Delete(item.id);
            GetMailTemplates();
        }

        #endregion

        #region MailServers
        private readonly IServersDataService _MailServersDataService;

        private ObservableCollection<Server> _MailServers;

        public ObservableCollection<Server> MailServers
        {
            get => _MailServers;
            private set => Set(ref _MailServers, value);
        }

        private Server _CurrentMailServer;

        public Server CurrentMailServer
        {
            get => _CurrentMailServer;
            set => Set(ref _CurrentMailServer, value);
        }

        public void GetMailServersData()
        {
            MailServers = new ObservableCollection<Server>(_MailServersDataService.GetAll());
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
            var new_item = new Server()
            {
                id = -1,
                Name = "HostName",
                Address = "NewHost",
                Port = 25,
                Ssl = true,
                Login = "user_name",
                Pwd = "user_password",
                Comment = ""
            };
            _MailServersDataService.Add(new_item);
            GetMailServersData();
            CurrentMailServer = new_item;
        }

        public ICommand SaveMailServersCommand { get; }

        private bool CanSaveMailServersCommandExecuted(Server item)
        {
            return item != null;
        }

        private void OnSaveMailServersCommandExecuted(Server item)
        {
            _MailServersDataService.Edit(item.id, item);
        }

        public ICommand DeleteMailServerCommand { get; }

        private bool CanDeleteMailServerCommandExecuted(Server item)
        {
            return item != null;
        }

        private void OnDeleteMailServerCommandExecuted(Server item)
        {
            _MailServersDataService.Delete(item.id);
            GetMailServersData();
        }

        #endregion

        #region MailSenderService
        //private readonly IMailSenderService _MailSenderService;

        #endregion
    }
}
