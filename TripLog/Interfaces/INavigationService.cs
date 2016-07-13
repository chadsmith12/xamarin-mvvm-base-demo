using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripLog.ViewModels;

namespace TripLog.Interfaces
{
    public interface INavigationService
    {
        /// <summary>
        /// Gets a value indicating whether this instance can go back.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can go back; otherwise, <c>false</c>.
        /// </value>
        bool CanGoBack { get; }

        /// <summary>
        /// Goes back.
        /// </summary>
        /// <returns></returns>
        Task GoBack();

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <typeparam name="T">The View model</typeparam>
        /// <returns></returns>
        Task NavigateTo<T>() where T : BaseViewModel;

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <typeparam name="T">the view model</typeparam>
        /// <typeparam name="TParam">The type of the parameter.</typeparam>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        Task NavigateTo<T, TParam>(TParam param) where T : BaseViewModel;

        /// <summary>
        /// Removes the last view.
        /// </summary>
        /// <returns></returns>
        Task RemoveLastView();

        /// <summary>
        /// Clears the back stack.
        /// </summary>
        /// <returns></returns>
        Task ClearBackStack();

        /// <summary>
        /// Navigates to URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        Task NavigateToUri(Uri uri);

        /// <summary>
        /// Occurs when [can go back changed].
        /// </summary>
        event PropertyChangedEventHandler CanGoBackChanged;
    }
}
