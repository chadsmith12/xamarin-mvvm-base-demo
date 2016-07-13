using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripLog.Interfaces;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Private Fields

        ObservableCollection<TripLogEntry> _logEntries;
        private Command<TripLogEntry> _viewCommand;
        private Command _newCommand;

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

        public MainViewModel(INavigationService navigationService) : base(navigationService)
        {
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
            LogEntries.Clear();

            // load up all entries in a background task
            await Task.Factory.StartNew(() =>
            {
                LogEntries.Add(new TripLogEntry("Washington Monument", 38.8895, -77.0352, new DateTime(2015, 2, 5), 3, "Amazing"));
                LogEntries.Add(new TripLogEntry("Statue of Liberty", 40.6892, -74.0444, new DateTime(2015, 4, 13), 4, "Inspiring!!"));
                LogEntries.Add(new TripLogEntry("Golden Gate Bridge", 37.8268, -122.4789, new DateTime(2015, 4, 26), 5, "Foggy, but beautiful!!"));
            });
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
