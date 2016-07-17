using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TripLog.Interfaces;
using TripLog.Models;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Private Fields

        ObservableCollection<TripLogEntry> _logEntries;
        private Command<TripLogEntry> _viewCommand;
        private Command _newCommand;

        private readonly ITripLogApiService _tripApiService;

        #endregion

        #region Properties

        public ObservableCollection<TripLogEntry> LogEntries
        {
            get { return _logEntries; }
            set
            {
                _logEntries = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructor

        public MainViewModel(INavigationService navigationService, ITripLogApiService tripApiService) : base(navigationService)
        {
            _tripApiService = tripApiService;
            LogEntries = new ObservableCollection<TripLogEntry>();
        }

        #endregion

        #region Public Methods
        public override async Task Init()
        {
            await LoadEntries();
        }
        #endregion

        #region Private Methods

        private async Task LoadEntries()
        {
            // we are already busy, don't try to do it again
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var entries = await _tripApiService.GetEntriesAsync();
                LogEntries = new ObservableCollection<TripLogEntry>(entries);
            }
            finally
            {
                IsBusy = false;
            }

            IsBusy = false;
        }
        #endregion

        #region Commands

        public Command<TripLogEntry> ViewCommand
        {
            get
            {
                return _viewCommand ?? (_viewCommand = new Command<TripLogEntry>(async (entry) => await ExecuteViewCommand(entry)));
            }
        }

        public Command NewCommand
        {
            get { return _newCommand ?? (_newCommand = new Command(async () => await ExecuteNewCommand())); }
        }

        private async Task ExecuteNewCommand()
        {
            await NavigationService.NavigateTo<NewEntryViewModel>();
        }

        private async Task ExecuteViewCommand(TripLogEntry entry)
        {
            await NavigationService.NavigateTo<DetailViewModel, TripLogEntry>(entry);
        }

        #endregion
    }
}
