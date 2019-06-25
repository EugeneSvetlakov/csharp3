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
using MailSender.Services.Linq2Sql;
using MailSender.Services;
using MailSender.Linq2Sql;

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

            SimpleIoc.Default.Register(() => new MailSenderDbContextDataContext());
            SimpleIoc.Default.Register<IRecipientsDataService, RecipientsDataServicesLinq2Sql>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MainWindowViewModel>();
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