

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using MailSender.Services;
using MailSender.Services.Linq2Sql;
using MailSender.Services.InMemory;
using MailSender.Data.Linq2Sql;
using MailSender.Data.EF;
using MailSender.Services.EF;

namespace MailSender.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}


            //����������� ����� �������� ������ � ������� ������������: InMemmory/Linq2Sql/EF
            var services = SimpleIoc.Default;

            #region Linq2Sql
            //services.Register(() => new MailSenderDbContext()); //�������������� ����������� � �� ����� Linq2Sql
            //services.Register<IRecipientsDataService, RecipientsDataServicesLinq2Sql>(); 
            //services.Register<ISendersDataService, SendersDataServicesLinq2Sql>(); 
            //services.Register<IServersDataService, MailServersDataServicesLinq2Sql>(); 
            //services.Register<IMailMessageDataService, MailTemplatesDataServicesLinq2Sql>(); 
            #endregion
            
            #region EntityFramework
            services.Register(() => new MailSenderDB()); //�������������� ����������� � �� ����� EF
            services.Register<IRecipientsDataService, RecipientsEFData>();
            services.Register<ISendersDataService, SendersEFData>();
            services.Register<IServersDataService, ServersEFData>();
            services.Register<IMessageDataService, MessagesEFData>();
            services.Register<IRecipientsListsDataService, RecipientsListsEFData>(); 
            //services.Register<ISendTasksDataService, SendTasksEFData>(); 
            #endregion

            #region InMemory
            //services.Register<IRecipientsDataService, RecipientsDataInMemory>();
            //services.Register<ISendersDataService, SendersDataInMemory>();
            //services.Register<IServersDataService, ServersDataInMemory>();
            //services.Register<IMailMessageDataService, MailMessagesDataInMemory>();
            #endregion

            #region �������� ������ ��������
            // ������ ���������� ���������. 
            // � ��� ����� ���������� ��� �������� ��������� SchedulerTasks:
            // 1) ����������� �������� �� �������� ���������
            // 2) ���������� �������(�������) ���������� �������� 
            //services.Register<IMailSenderService, MailSenderService>(); //������ �������� ���������
            #endregion

            services.Register<MainViewModel>();
            services.Register<MainWindowViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        //public MainWindowViewModel MainWindowViewModel => ServiceLocator.Current.GetInstance<MainWindowViewModel>();
        public MainWindowViewModel MainWindowViewModel
        {
            get
            {
                var model = ServiceLocator.Current.GetInstance<MainWindowViewModel>();
                return model;
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}