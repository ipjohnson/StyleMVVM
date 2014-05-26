using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Grace.DependencyInjection.Attributes;
using StyleMVVM.View;

namespace StyleMVVM.ViewModel
{
	/// <summary>
	/// This is the default implementation for ViewModels associated with a Page
	/// </summary>
	public class PageViewModel : NavigatingViewModel, IViewAware, ILoadedAwareViewModel
	{
		private WeakReference<FrameworkElement> viewReference;
		private INavigationService navigation;
		private Func<Frame, INavigationService> navigationServiceFactory;
		private bool foundFrame = false;


		#region Navigation

		/// <summary>
		/// Imports a navigation service factory
		/// </summary>
		/// <param name="navigationServiceFactory">navigation service factory</param>
		public void ImportNavigationSericeFactory(Func<Frame, INavigationService> navigationServiceFactory)
		{
			this.navigationServiceFactory = navigationServiceFactory;
		}

		/// <summary>
		/// Navigation service for page
		/// </summary>
		public INavigationService Navigation
		{
			get
			{
				if (navigation == null || !navigation.IsValid || !foundFrame)
				{
					Frame frame = GetFrame();

					foundFrame = frame != null;

					navigation = navigationServiceFactory(frame);
				}

				return navigation;
			}
		}

		/// <summary>
		/// View this ViewModel is attached to
		/// </summary>
		public FrameworkElement View
		{
			get
			{
				FrameworkElement view = null;

				if (viewReference != null)
				{
					viewReference.TryGetTarget(out view);
				}

				return view;
			}
			set
			{
				FrameworkElement oldView = null;

				if (viewReference != null)
				{
					viewReference.TryGetTarget(out oldView);
				}

				if (oldView != value)
				{
					if (value != null)
					{
						viewReference = new WeakReference<FrameworkElement>(value);
					}

					else
					{
						viewReference = null;
					}

					navigation = null;
				}
			}
		}

		/// <summary>
		/// Get the frame this page is in
		/// </summary>
		/// <returns></returns>
		protected virtual Frame GetFrame()
		{
			DependencyObject tempFrame = View;

			while (tempFrame != null && !(tempFrame is Frame))
			{
				tempFrame = VisualTreeHelper.GetParent(tempFrame) as FrameworkElement;
			}

			return tempFrame as Frame;
		}

		#endregion

		#region Loaded events

		/// <summary>
		/// Called when loaded
		/// </summary>
		/// <param name="sender">view</param>
		/// <param name="args">loaded args</param>
		public virtual void OnLoaded(object sender, RoutedEventArgs args)
		{
			OnPropertyChanged("Navigation");
		}

		/// <summary>
		/// Called when unloaded
		/// </summary>
		/// <param name="sender">view</param>
		/// <param name="args">args</param>
		public virtual void OnUnloaded(object sender, RoutedEventArgs args)
		{
		}

		#endregion
	}
}
