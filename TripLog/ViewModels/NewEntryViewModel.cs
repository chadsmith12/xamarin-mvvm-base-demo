using System;
using System.Threading.Tasks;
using TripLog.Interfaces;
using TripLog.Models;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
    public class NewEntryViewModel : BaseViewModel
    {
        #region Private Fields

        private string _title;
        private double _latitude;
        private double _longitude;
        private DateTime _date;
        private int _rating;
        private string _notes;
        private Command _saveCommand;
        private readonly ILocationService _locationService;
        private readonly ITripLogApiService _tripLogApiService;


        #endregion

        #region Properties

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        public double Latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        public double Longitude
        {
            get {return _longitude; }
            set
            {
                _longitude = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        public DateTime Date
        {
            get { return _date;}
            set
            {
                _date = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();

            }
        }

        public int Rating
        {
            get { return _rating;}
            set
            {
                _rating = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();

            }
        }

        public string Notes
        {
            get { return _notes;}
            set
            {
                _notes = value; 
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();

            }
        }
        #endregion

        #region Constructors

        public NewEntryViewModel(INavigationService navigationService, ILocationService locationService, ITripLogApiService tripLogApiService) : base(navigationService)
        {
            _locationService = locationService;
            _tripLogApiService = tripLogApiService;
            Date = DateTime.Today;
            Rating = 1;
        }
        #endregion

        #region Public Methods
        public override async Task Init()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var coords = await _locationService.GetGeoCoordinatesAsync();
            Latitude = coords.Latitude;
            Longitude = coords.Longitude;

            IsBusy = false;
        }
        #endregion

        #region Commands

        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(async () =>  await ExecuteSaveCommand(), CanSave));

        private bool CanSave()
        {
            return !String.IsNullOrWhiteSpace(Title);
        }

        private async Task ExecuteSaveCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var newItem = new TripLogEntry(this.Title, this.Latitude, this.Longitude, this.Date, this.Rating, this.Notes);


            try
            {
                await _tripLogApiService.SaveEntryAsync(newItem);
                await NavigationService.GoBack();
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion

    }
}
