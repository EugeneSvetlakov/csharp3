

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using MailSender.Services;
using MailSender.Services.Linq2Sql;
using MailSender.Services.InMemory;
using MailSender.Data.Linq2Sql;

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


            //Определение каким сервисом работы с данными пользоваться: InMemmory/Linq2Sql/EF
            var services = SimpleIoc.Default;
            
            services.Register(() => new MailSenderDbContext()); //Инициализирует подключение к БД через Linq2Sql
            services.Register<IRecipientsDataService, RecipientsDataInMemory>(); //Данные в памяти
            //services.Register<IRecipientsDataService, RecipientsDataServicesLinq2Sql>(); //Данные получателей в БД через Linq2Sql
            services.Register<IServersDataService, ServersDataInMemory>(); //Данные серверов в памяти
            //services.Register<IServersDataService, MailServersDataServicesLinq2Sql>(); //Данные серверов в БД через Linq2Sql
            services.Register<ISendersDataService, SendersDataInMemory>(); //Данные отправителей в памяти
            //services.Register<ISendersDataService, SendersDataServicesLinq2Sql>(); //Данные в БД через Linq2Sql
            services.Register<IMailMessageDataService, MailMessagesDataInMemory>(); //Данные в памяти
            //services.Register<IMailMessageDataService, MailTemplatesDataServicesLinq2Sql>(); //Данные в БД через Linq2Sql
            services.Register<IMailSenderService, SmtpMailSenderService>(); //Сервис рассылки сообщений

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