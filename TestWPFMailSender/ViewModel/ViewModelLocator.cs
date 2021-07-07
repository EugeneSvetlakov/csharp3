/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:TestWPFMailSender"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

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

            var services = SimpleIoc.Default;
            services.Register(() => new MailSenderDbContext());
            services.Register<IRecipientsDataService, RecipientsDataInMemory>();
            //services.Register<IRecipientsDataService, RecipientsDataServicesLinq2Sql>();
            services.Register<IServersDataService, ServersDataInMemory>();
            //services.Register<IServersDataService, MailServersDataServicesLinq2Sql>();
            services.Register<ISendersDataService, SendersDataServicesLinq2Sql>();
            services.Register<IMailMessageDataService, MailTemplatesDataServicesLinq2Sql>();
            services.Register<IMailSenderService, SmtpMailSenderService>();

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