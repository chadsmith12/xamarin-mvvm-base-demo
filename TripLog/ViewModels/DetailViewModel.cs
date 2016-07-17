using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripLog.Interfaces;
using TripLog.Models;

namespace TripLog.ViewModels
{
    public class DetailViewModel : BaseViewModel<TripLogEntry>
    {
        #region Private Fields

        private TripLogEntry _entry;

        #endregion

        #region Properties

        public TripLogEntry Entry
        {
            get { return _entry;}
            set
            {
                _entry = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors

        public DetailViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
        #endregion

        public override async Task Init(TripLogEntry logEntry)
        {
            Entry = logEntry;
        }
    }
}
