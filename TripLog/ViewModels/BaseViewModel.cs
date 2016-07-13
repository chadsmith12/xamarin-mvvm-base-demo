using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TripLog.Annotations;
using TripLog.Interfaces;

namespace TripLog.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected INavigationService NavigationService { get; private set; }
        protected BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Provides a way to initialize a view model without the constructor.
        /// Useful when you need to refresh a view model
        /// </summary>
        /// <returns></returns>
        public abstract Task Init();
    }

    /// <summary>
    /// A generic-typed BaseViewModel abstract class
    /// </summary>
    /// <typeparam name="TParam">The type of the parameter.</typeparam>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public abstract class BaseViewModel<TParam> : BaseViewModel
    {
        protected BaseViewModel(INavigationService navigationService) : base(navigationService)
        { }

        public override async Task Init()
        {
            await Init(default(TParam));
        }

        public abstract Task Init(TParam param);
    }
}
