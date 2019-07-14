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
            , ISendersDataService SendersDataService
            , IServersDataService MailServersDataService
            , IMessageDataService MailTemplatesDataService
            , IRecipientsListsDataService RecipientsListsDataService
            , IMailTasksDataService MailTasksDataService
            , IMailTasksSender MailTasksSender
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
            SaveMailTemplatesCommand = new RelayCommand<Message>(OnSaveMailTemplatesCommandExecuted, CanSaveMailTemplatesCommandExecuted);
            DeleteMailTemplateCommand = new RelayCommand<Message>(OnDeleteMailTemplateCommandExecuted, CanDeleteMailTemplateCommandExecuted);
            //GetMailTemplates();

            _MailTasksDataService = MailTasksDataService;
            UpdateMailTasksCommand = new RelayCommand(OnUpdateMailTasksCommandExecuted, CanUpdateMailTasksCommandExecuted);
            CreateMailTasksCommand = new RelayCommand(OnCreateMailTasksCommandExecuted, CanCreateMailTasksCommandExecuted);
            DeleteMailTasksCommand = new RelayCommand<MailTask>(OnDeleteMailTasksCommandExecuted, CanDeleteMailTasksCommandExecuted);
            SaveMailTasksCommand = new RelayCommand<MailTask>(OnSaveMailTasksCommandExecuted, CanSaveMailTasksCommandExecuted);

            _MailTasksSender = MailTasksSender;
            SendMailTaskNowCommand = new RelayCommand<MailTask>(OnSendMailTaskNowCommandExecuted, CanSendMailTaskNowCommandExecuted);
            //todo Commands for MailSenderService

            //RecipientsLists
            _RecipientsListsDataService = RecipientsListsDataService;
            UpdateRecipientsListsCommand = new RelayCommand(OnUpdateRecipientsListsCommandExecuted, CanUpdateRecipientsListsCommandExecuted);
            CreateRecipientsListsCommand = new RelayCommand(OnCreateRecipientsListsCommandExecuted, CanCreateRecipientsListsCommandExecuted);
            DeleteRecipientsListsCommand = new RelayCommand<RecipientsList>(OnDeleteRecipientsListsCommandExecuted, CanDeleteRecipientsListsCommandExecuted);
            SaveRecipientsListsCommand = new RelayCommand<RecipientsList>(OnSaveRecipientsListsCommandExecuted, CanSaveRecipientsListsCommandExecuted);
            AddRecipientToListCommand = new RelayCommand<Recipient>(OnAddRecipientToListCommandExecuted, CanAddRecipientToListCommandExecuted);
            RemoveRecipientFromListCommand = new RelayCommand<Recipient>(OnRemoveRecipientFromListCommandExecuted, CanRemoveRecipientFromListCommandExecuted);
            ClearRecipientsFromListCommand = new RelayCommand<RecipientsList>(OnClearRecipientsFromListCommandExecuted, CanClearRecipientsFromListCommandExecuted);

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

        #region MailTask
        private readonly IMailTasksDataService _MailTasksDataService;

        private ObservableCollection<MailTask> _MailTasks;
        public ObservableCollection<MailTask> MailTasks
        {
            get => _MailTasks;
            set => Set(ref _MailTasks, value);
        }

        private MailTask _CurrentMailTask;
        public MailTask CurrentMailTask
        {
            get => _CurrentMailTask;
            set => Set(ref _CurrentMailTask, value);
        }

        //GetAll()
        private void GetMailTasks() => _MailTasksDataService.GetAll();

        public ICommand UpdateMailTasksCommand { get; }
        private bool CanUpdateMailTasksCommandExecuted() => true;
        private void OnUpdateMailTasksCommandExecuted()
        {
            GetMailTasks();
        }

        public ICommand CreateMailTasksCommand { get; }
        private bool CanCreateMailTasksCommandExecuted() => true;
        private void OnCreateMailTasksCommandExecuted()
        {
            throw new NotImplementedException();
        }

        public ICommand DeleteMailTasksCommand { get; }
        private bool CanDeleteMailTasksCommandExecuted(MailTask item) => !(item is null);
        private void OnDeleteMailTasksCommandExecuted(MailTask item)
        {
            throw new NotImplementedException();
        }

        public ICommand SaveMailTasksCommand { get; }
        private bool CanSaveMailTasksCommandExecuted(MailTask item) => !(item is null);
        private void OnSaveMailTasksCommandExecuted(MailTask item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region MailTasksSender
        private readonly IMailTasksSender _MailTasksSender;

        public ICommand SendMailTaskNowCommand { get; }
        private bool CanSendMailTaskNowCommandExecuted(MailTask item) => !(item is null);
        private void OnSendMailTaskNowCommandExecuted(MailTask item)
        {
            throw new NotImplementedException();
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

        #region RecipientsLists
        private readonly IRecipientsListsDataService _RecipientsListsDataService;

        private ObservableCollection<RecipientsList> _RecipientsLists;
        public ObservableCollection<RecipientsList> RecipientsLists
        {
            get => _RecipientsLists;
            set => Set(ref _RecipientsLists, value);
        }

        private RecipientsList _CurrentRecipientsList;
        public RecipientsList CurrentRecipientsList
        {
            get => _CurrentRecipientsList;
            set => Set(ref _CurrentRecipientsList, value);
        }

        private Recipient _ListRecipient;
        public Recipient ListRecipient
        {
            get => _ListRecipient;
            set => Set(ref _ListRecipient, value);
        }

        public void GetRecipientsLists()
        {
            RecipientsLists = new ObservableCollection<RecipientsList>(_RecipientsListsDataService.GetAll());
        }

        public ICommand UpdateRecipientsListsCommand { get; }
        private bool CanUpdateRecipientsListsCommandExecuted() => true;
        private void OnUpdateRecipientsListsCommandExecuted()
        {
            GetRecipientsLists();
        }

        public ICommand CreateRecipientsListsCommand { get; }

        private bool CanCreateRecipientsListsCommandExecuted() => true;

        private void OnCreateRecipientsListsCommandExecuted()
        {
            var new_RecipientsList = new Collection<Recipient>();
            var new_item = new RecipientsList()
            {
                Name = "NewSendList",
                Recipients = new_RecipientsList,
                Comment = ""
            };
            var result = _RecipientsListsDataService.Add(new_item);
            GetRecipientsLists();
            CurrentRecipientsList = new_item;
        }

        public ICommand DeleteRecipientsListsCommand { get; }

        private bool CanDeleteRecipientsListsCommandExecuted(RecipientsList item)
        {
            return item != null;
        }

        private void OnDeleteRecipientsListsCommandExecuted(RecipientsList item)
        {
            _RecipientsListsDataService.Delete(item.id);
            GetRecipientsLists();
        }

        public ICommand SaveRecipientsListsCommand { get; }

        private bool CanSaveRecipientsListsCommandExecuted(RecipientsList item)
        {
            return item != null && item.Name.Length > 3;
        }

        private void OnSaveRecipientsListsCommandExecuted(RecipientsList item)
        {
            _RecipientsListsDataService.Edit(item.id, item);
            GetRecipientsLists();
        }

        #region Work with CurrentRecipientsList
        public ICommand AddRecipientToListCommand { get; }
        private bool CanAddRecipientToListCommandExecuted(Recipient item)
        {
            return (item != null && CurrentRecipientsList != null);
        }
        private void OnAddRecipientToListCommandExecuted(Recipient item)
        {
            _RecipientsListsDataService.AddRecipientToList(CurrentRecipientsList.id, item);
            CurrentRecipientsList =
                _RecipientsListsDataService.GetById(CurrentRecipientsList.id);
        }

        public ICommand RemoveRecipientFromListCommand { get; }
        private bool CanRemoveRecipientFromListCommandExecuted(Recipient item)
        {
            return (item != null && CurrentRecipientsList != null);
        }
        private void OnRemoveRecipientFromListCommandExecuted(Recipient item)
        {
            _RecipientsListsDataService.RemoveRecipientFromList(CurrentRecipientsList.id, item);
            CurrentRecipientsList =
                _RecipientsListsDataService.GetById(CurrentRecipientsList.id);
        }

        public ICommand ClearRecipientsFromListCommand { get; }
        private bool CanClearRecipientsFromListCommandExecuted(RecipientsList item)
        {
            return (item != null);
        }
        private void OnClearRecipientsFromListCommandExecuted(RecipientsList item)
        {
            _RecipientsListsDataService.ClearRecipientsFromList(item.id);
            CurrentRecipientsList =
                _RecipientsListsDataService.GetById(CurrentRecipientsList.id);
        }
        #endregion

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
        private readonly IMessageDataService _MailTemplatesDataService;
        private ObservableCollection<Message> _MailTemplates;
        public ObservableCollection<Message> MailTemplates
        {
            get => _MailTemplates;
            private set => Set(ref _MailTemplates, value);
        }
        public void GetMailTemplates()
        {
            MailTemplates = new ObservableCollection<Message>(_MailTemplatesDataService.GetAll());
        }

        private Message _CurrentMailTemplate;

        public Message CurrentMailTemplate
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
            var new_item = new Message()
            {
                Subject = "NewSubject",
                Body = "NewMessage"
            };
            _MailTemplatesDataService.Add(new_item);
            GetMailTemplates();
            CurrentMailTemplate = new_item;
        }

        public ICommand SaveMailTemplatesCommand { get; }

        private bool CanSaveMailTemplatesCommandExecuted(Message item)
        {
            return item != null;
        }

        private void OnSaveMailTemplatesCommandExecuted(Message item)
        {
            _MailTemplatesDataService.Edit(item.id, item);
        }

        public ICommand DeleteMailTemplateCommand { get; }

        private bool CanDeleteMailTemplateCommandExecuted(Message item)
        {
            return item != null;
        }

        private void OnDeleteMailTemplateCommandExecuted(Message item)
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

        public IMailTasksSender MailTasksSender => mailTasksSender;

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
