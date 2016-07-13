using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TripLog.Interfaces;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Forms;

[assembly: Dependency(typeof(NavigationService))]
namespace TripLog.Services
{
    public class NavigationService : INavigationService
    {
        #region Private Fields
        private IDictionary<Type, Type> _map = new Dictionary<Type, Type>();

        #endregion

        #region Public Methods

        public INavigation Navigation { get; set; }

        public bool CanGoBack => Navigation.NavigationStack != null;

        public async Task GoBack()
        {
            // check to see if we can go back then pop off the stack
            if (CanGoBack)
                await Navigation.PopAsync(true);

            OnCanGoBackChanged();
        }

        public async Task NavigateTo<T>() where T : BaseViewModel
        {
            await NavigateToView(typeof (T));

            if (Navigation.NavigationStack.Last().BindingContext is BaseViewModel)
            {
                await ((BaseViewModel) (Navigation.NavigationStack.Last().BindingContext)).Init();
            }
        }

        public async Task NavigateTo<T, TParam>(TParam param) where T : BaseViewModel
        {
            await NavigateToView(typeof (T));

            if (Navigation.NavigationStack.Last().BindingContext is BaseViewModel<TParam>)
                await ((BaseViewModel<TParam>) (Navigation.NavigationStack.Last().BindingContext)).Init(param);
        }

        public async Task RemoveLastView()
        {
            if (Navigation.NavigationStack.Any())
            {
                var lastView = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(lastView);
            }
        }

        public async Task ClearBackStack()
        {
            if (Navigation.NavigationStack.Count <= 1)
            {
                return;
            }

            for(var i = 0; i < Navigation.NavigationStack.Count - 1; i++)
            {
                Navigation.RemovePage(Navigation.NavigationStack[i]);
            }
        }

        public async Task NavigateToUri(Uri uri)
        {
            if(uri == null)
                throw new ArgumentNullException(nameof(uri));

            Device.OpenUri(uri);
        }

        public event PropertyChangedEventHandler CanGoBackChanged;

        public void RegisterViewMapping(Type viewModel, Type view)
        {
            _map.Add(viewModel, view);
        }
        #endregion

        #region Private Methods
        private async Task NavigateToView(Type viewModelType)
        {
            Type viewType;

            // try to find this view mapping for this view
            if (!_map.TryGetValue(viewModelType, out viewType))
            {
                throw new ArgumentException("No view found in View Mapping for " + viewModelType.FullName + ".");
            }

            // get the empty constructor for this view
            var constructor = viewType.GetTypeInfo().DeclaredConstructors.FirstOrDefault(c => c.GetParameters().Length <= 0);
            var view = constructor.Invoke(null) as Page;

            // push this view onto the navigation stack
            await Navigation.PushAsync(view, true);
        }

        private void OnCanGoBackChanged()
        {
            var handler = CanGoBackChanged;

            handler?.Invoke(this, new PropertyChangedEventArgs("CanGoBack"));
        }
        #endregion
    }
}
