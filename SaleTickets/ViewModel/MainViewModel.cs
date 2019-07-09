using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SaleTickets.Data;
using SaleTickets.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SaleTickets.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(ITicketDataService TicketDataService)
        {
            _TicketDataService = TicketDataService;
            GetTicketsDataCommand = new RelayCommand(OnGetTicketsDataCommandExecuted, CanGetTicketsDataCommandExecuted);
        }

        #region Ticket
        private readonly ITicketDataService _TicketDataService;
        private ObservableCollection<Ticket> _Tickets;

        public ObservableCollection<Ticket> Tickets
        {
            get => _Tickets;
            private set => Set(ref _Tickets, value);
        }

        private Ticket _CurrentTicket;

        public Ticket CurrentTicket
        {
            get => _CurrentTicket;
            set => Set(ref _CurrentTicket, value);
        }
        #endregion

        #region Commands
        public ICommand GetTicketsDataCommand { get; }

        private bool CanGetTicketsDataCommandExecuted() => true;

        private void OnGetTicketsDataCommandExecuted()
        {
            GetTicketsData();
        }

        private void GetTicketsData()
        {
            Tickets = new ObservableCollection<Ticket>(_TicketDataService.GetAll());
        }
        #endregion

    }
}