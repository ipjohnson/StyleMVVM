using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grace.Data;
using Grace.DependencyInjection;

#if NETFX_CORE
using Windows.UI.Xaml.Controls;
#else
using System.Windows.Controls;
#endif

namespace StyleMVVM.View.Impl
{
	public class NavigationService : NotifyObject, INavigationService
	{
		private readonly WeakReference<Frame> frameReference;
		private readonly IInjectionScope injectionScope;

		public NavigationService(Frame frame, IInjectionScope injectionScope)
		{
			this.injectionScope = injectionScope;

			frameReference = frame != null ?
									new WeakReference<Frame>(frame) :
									new WeakReference<Frame>(ViewHelper.MainContent as Frame);
		}

		public Frame Frame
		{
			get
			{
				Frame frame;

				if (frameReference.TryGetTarget(out frame))
				{
					return frame;
				}

				return null;
			}
		}

		public bool IsValid
		{
			get
			{
				Frame frame;

				return frameReference.TryGetTarget(out frame);
			}
		}

		public bool CanGoBack
		{
			get
			{
				Frame frame;

				if (frameReference.TryGetTarget(out frame))
				{
					return frame.CanGoBack;
				}

				return false;
			}
		}

		public bool CanGoForward
		{
			get
			{
				Frame frame;

				if (frameReference.TryGetTarget(out frame))
				{
					return frame.CanGoForward;
				}

				return false;
			}
		}

		public void GoHome()
		{
			Frame frame;

			if (frameReference.TryGetTarget(out frame))
			{
				while (frame.CanGoBack)
				{
					frame.GoBack();
				}
			}
		}

		public void GoBack()
		{
			Frame frame;

			if (frameReference.TryGetTarget(out frame))
			{
				frame.GoBack();
			}
		}

		public void GoForward()
		{
			Frame frame;

			if (frameReference.TryGetTarget(out frame))
			{
				frame.GoForward();
			}
		}

		public bool Navigate(string pageName, object navigationParam)
		{
			Frame frame;

			if (frameReference.TryGetTarget(out frame))
			{
#if NETFX_CORE
				IExportStrategy exportStrategy = injectionScope.GetStrategy(pageName);

				if (exportStrategy == null)
				{
					throw new Exception("Could not find Page: " + pageName);
				}

				if (navigationParam != null)
				{
					return frame.Navigate(exportStrategy.ActivationType, navigationParam);
				}

				return frame.Navigate(exportStrategy.ActivationType);
#else

#endif
			}

			return false;
		}

		public bool Navigate(Type pageType, object navigationParam = null)
		{
			Frame frame;

			if (frameReference.TryGetTarget(out frame))
			{
				if (navigationParam != null)
				{
					return frame.Navigate(pageType, navigationParam);
				}

				return frame.Navigate(pageType);
			}

			return false;
		}

	}
}
